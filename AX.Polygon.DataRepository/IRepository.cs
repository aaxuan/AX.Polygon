using AX.Polygon.DataRepository.Model;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;

namespace AX.Polygon.DataRepository
{
    public interface IRepository
    {
        DbConnection DBConnection { get; set; }
        DbTransaction DBTransaction { get; set; }

        Task<Repository> BeginTransactionAsync();

        Task CommitTransactionAsync();

        Task<bool> DeleteAllAsync<T>() where T : class;

        Task<bool> DeleteAsync<T>(T entity) where T : class;

        Task<bool> DeleteByIdAsync<T>(dynamic id) where T : class;

        Task<int> ExecuteAsync(string sql, object param = null);

        Task<T> ExecuteScalarAsync<T>(string sql, object param = null);

        Task<IEnumerable<T>> GetAllAsync<T>() where T : class, new();

        Task<T> GetByIdAsync<T>(dynamic id) where T : class;

        Task<int> GetCountAsync<T>();

        DataTable GetTable(string sql, object param = null);

        Task<List<T>> InsertAsync<T>(List<T> entities) where T : class;

        Task<int> InsertAsync<T>(T entity) where T : class;

        Task<IEnumerable<T>> QueryAsync<T>(string sql, object param = null) where T : class;

        Task<PageData<T>> QueryPageListAsync<T>(SearchArguments searchArguments) where T : class, new();

        Task<T> QuerySingleAsync<T>(string sql, object param = null) where T : class;

        Task RollbackTransAsync();

        Task<bool> Update<T>(T entity) where T : class;

        string GetCreateTableSql();
    }
}