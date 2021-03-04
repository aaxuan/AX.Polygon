using AX.Polygon.DataRepository.Model;
using Dapper;
using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AX.Polygon.DataRepository
{
    public class Repository : IRepository
    {
        #region 内部方法

        private static PropertyInfo GetSingleKey<T>()
        {
            return GetSingleKey(typeof(T));
        }

        private static PropertyInfo GetSingleKey(Type type)
        {
            var sqlMapperExtensionsType = typeof(SqlMapperExtensions);
            var methodInfo = sqlMapperExtensionsType.GetMethod("GetSingleKey", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
            //获取泛型方法
            var dynamicMethodInfo = methodInfo.MakeGenericMethod(type);
            var result = dynamicMethodInfo.Invoke(null, new object[] { "DeleteByIdAsync" }) as PropertyInfo;
            return result;
        }

        private static string GetTableName(Type type)
        {
            var sqlMapperExtensionsType = typeof(SqlMapperExtensions);
            var methodInfo = sqlMapperExtensionsType.GetMethod("GetTableName", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
            var result = methodInfo.Invoke(null, new object[] { type }) as string;
            return result;
        }

        private static string GetTableName<T>()
        {
            return GetTableName(typeof(T));
        }

        private static List<PropertyInfo> GetTypeProperties<T>()
        {
            var sqlMapperExtensionsType = typeof(SqlMapperExtensions);
            var methodInfo = sqlMapperExtensionsType.GetMethod("TypePropertiesCache", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
            var result = methodInfo.Invoke(null, new object[] { typeof(T) }) as List<PropertyInfo>;
            return result;
        }

        #endregion 内部方法

        #region 属性

        public DbConnection DBConnection { get; set; }
        public DbTransaction DBTransaction { get; set; }

        #endregion 属性

        public Repository(DbConnection dbConnection) => this.DBConnection = dbConnection;

        #region 事务

        public async Task<Repository> BeginTransactionAsync()
        {
            if (DBConnection.State == ConnectionState.Closed)
            { await DBConnection.OpenAsync(); }
            DBTransaction = await DBConnection.BeginTransactionAsync();
            return this;
        }

        public async Task CommitTransactionAsync()
        {
            try
            {
                if (DBTransaction != null)
                {
                    await DBTransaction.CommitAsync();
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                await DBConnection.CloseAsync();
            }
        }

        public async Task RollbackTransAsync()
        {
            await this.DBTransaction.RollbackAsync();
            await this.DBTransaction.DisposeAsync();
            await DBConnection.CloseAsync();
        }

        #endregion 事务

        #region 执行 SQL 语句

        public async Task<int> ExecuteAsync(string sql, object param = null)
        {
            return await DBConnection.ExecuteAsync(sql, param, DBTransaction);
        }

        public async Task<T> ExecuteScalarAsync<T>(string sql, object param = null)
        {
            return await DBConnection.ExecuteScalarAsync<T>(sql, param, DBTransaction);
        }

        #endregion 执行 SQL 语句

        #region 对象实体 添加、修改、删除

        public async Task<int> InsertAsync<T>(T entity) where T : class
        {
            return await DBConnection.InsertAsync<T>(entity, DBTransaction);
        }

        public async Task<List<T>> InsertAsync<T>(List<T> entities) where T : class
        {
            foreach (var item in entities)
            {
                await DBConnection.InsertAsync<T>(item, DBTransaction);
            }
            return entities;
        }

        public async Task<bool> DeleteAllAsync<T>() where T : class
        {
            return await DBConnection.DeleteAllAsync<T>(DBTransaction);
        }

        public async Task<bool> DeleteAsync<T>(T entity) where T : class
        {
            return await DBConnection.DeleteAsync<T>(entity, DBTransaction);
        }

        public async Task<bool> DeleteByIdAsync<T>(dynamic id) where T : class
        {
            var key = GetSingleKey<T>();
            var tableName = GetTableName<T>();
            var sql = $"delete from {tableName} where {key.Name} = @id";
            var dynParams = new DynamicParameters();
            dynParams.Add("@id", id);
            var deleted = await DBConnection.ExecuteAsync(sql, dynParams, DBTransaction).ConfigureAwait(false);
            return deleted > 0;
        }

        public async Task<bool> Update<T>(T entity) where T : class
        {
            return await DBConnection.UpdateAsync<T>(entity);
        }

        #endregion 对象实体 添加、修改、删除

        #region 对象实体 查询

        public async Task<int> GetCountAsync<T>()
        {
            var tableName = GetTableName<T>();
            var sql = $"select count(*) from {tableName}";
            var result = await DBConnection.ExecuteScalarAsync<int>(sql, null, DBTransaction).ConfigureAwait(false);
            return result;
        }

        public async Task<T> GetByIdAsync<T>(dynamic id) where T : class
        {
            var key = GetSingleKey<T>();
            var tableName = GetTableName<T>();
            var sql = $"select * from {tableName} where {key.Name} = @id";
            var dynParams = new DynamicParameters();
            dynParams.Add("@id", id);
            return await DBConnection.QuerySingleOrDefaultAsync<T>(sql, dynParams, DBTransaction);
        }

        public async Task<T> QuerySingleAsync<T>(string sql, object param = null) where T : class
        {
            return await DBConnection.QuerySingleAsync<T>(sql, param, DBTransaction);
        }

        public async Task<IEnumerable<T>> GetAllAsync<T>() where T : class, new()
        {
            return await DBConnection.GetAllAsync<T>(DBTransaction);
        }

        public async Task<IEnumerable<T>> QueryAsync<T>(string sql, object param = null) where T : class
        {
            if (sql.ToLower().StartsWith("where"))
            { sql = $"select * from {GetTableName<T>()} " + sql; }

            return await DBConnection.QueryAsync<T>(sql, param, DBTransaction);
        }

        public async Task<PageData<T>> QueryPageListAsync<T>(SearchArguments searchArguments) where T : class, new()
        {
            //构造两个sql
            var result = new PageData<T>();
            var tableName = GetTableName<T>();
            var wheresql = new StringBuilder();
            var dyparms = new DynamicParameters();

            searchArguments.SearchFilter = searchArguments.SearchFilter.Where(p => p.IsValid).ToList();

            //过滤条件
            foreach (var filterItem in searchArguments.SearchFilter)
            {
                if (wheresql.Length == 0) { wheresql.Append($"where "); }
                wheresql.Append($"{filterItem.FilterName} {filterItem.FilterType} @{filterItem.FilterName}");
                dyparms.Add($"@{filterItem.FilterName}", filterItem.FilterValue);
            }
            //搜索字段
            if (string.IsNullOrWhiteSpace(searchArguments.SelectField))
            { searchArguments.SelectField = "*"; }

            //排序
            if (string.IsNullOrWhiteSpace(searchArguments.Order) == false)
            { wheresql.Append($" order by {searchArguments.Order}"); }

            result.TotalCount = await DBConnection.ExecuteScalarAsync<int>($"select count(*) from {tableName} {wheresql}", dyparms, DBTransaction).ConfigureAwait(false);

            //分页
            if (searchArguments.UsePage)
            {
                result.BeginIndex = searchArguments.PageIndex * searchArguments.PageItemCount;
                result.EndIndex = result.BeginIndex + searchArguments.PageItemCount;
                wheresql.Append($" limit {result.BeginIndex},{result.EndIndex}");
            }

            var data = await DBConnection.QueryAsync<T>($"select {searchArguments.SelectField} from {tableName} {wheresql}", dyparms).ConfigureAwait(false);
            result.Items = data.ToList();
            result.PageIndex = searchArguments.PageIndex;
            result.TotalPageCount = result.TotalCount / searchArguments.PageItemCount;

            return result;
        }

        #endregion 对象实体 查询

        #region 数据源 查询

        public DataTable GetTable(string sql, object param = null)
        {
            DataTable dt = new DataTable();
            var reader = DBConnection.ExecuteReaderAsync(sql, param).Result;
            dt.Load(reader);
            return dt;
        }

        #endregion 数据源 查询

        #region 数据库结构

        public string GetCreateTableSql()
        {
            //构建创建表sql
            var allType = Assembly.Load("AX.Polygon.Admin").GetTypes();
            var dataModelType = allType.Where(p => p.Namespace == "AX.Polygon.Admin.DataModel").ToList();

            var sb = new StringBuilder();

            foreach (var type in dataModelType)
            {
                var tableName = GetTableName(type);
                var PrimaryKey = GetSingleKey(type);

                sb.AppendLine($"DROP TABLE IF EXISTS `{tableName}`;");
                sb.AppendLine($"CREATE TABLE IF NOT EXISTS `{tableName}` (");

                foreach (var propertyInfo in type.GetProperties())
                {
                    sb.AppendLine($"`{propertyInfo.Name}`    {GetType(propertyInfo)}    {GetCanNull(propertyInfo, PrimaryKey)}    COMMENT '{Util.Reflect.GetPropertiesDisplayNameValue(propertyInfo)}',");
                }

                sb.Remove(sb.Length - 1, 1);
                sb.AppendLine($"PRIMARY KEY(`{PrimaryKey.Name}`)");
                sb.AppendLine($") ENGINE = InnoDB COMMENT '{Util.Reflect.GetClassDisplayNameValue(type)}';");
                sb.AppendLine();
                sb.AppendLine();
            }

            return sb.ToString();
        }

        private object GetCanNull(PropertyInfo propertyInfo, PropertyInfo primaryKey)
        {
            if (propertyInfo.Name == primaryKey.Name)
                return "NOT NULL";
            else
                return "NULL";
        }

        private static string GetType(PropertyInfo item)
        {
            var lowerName = item.PropertyType.FullName.ToLower();

            if (lowerName.Contains("boolean"))
            { return "bit(1)"; }
            if (lowerName.Contains("datetime"))
            { return "datetime"; }
            if (lowerName.Contains("decimal"))
            { return "decimal(10, 2)"; }
            if (lowerName.Contains("double"))
            { return "double"; }
            if (lowerName.Contains("int"))
            { return "int(11)"; }
            if (lowerName.Contains("string"))
            {
                //var length = Reflection.PropertyInfoManage.GetMaxStringLength(item);
                //if (length <= 0)
                { return "varchar(50)"; }
                //else
                //{ return $"varchar({length})"; }
            }

            throw new System.NotSupportedException($"未匹配字段对应数据库类型 {item.PropertyType.FullName}");
        }

        #endregion 数据库结构
    }
}