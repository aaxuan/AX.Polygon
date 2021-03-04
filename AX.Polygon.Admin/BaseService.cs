using AX.Polygon.DataRepository.Model;
using System.Threading.Tasks;

namespace AX.Polygon.Admin
{
    public class BaseService<T> where T : class, new()
    {
        internal DataRepository.IRepository DB { get { return Util.IOCManager.GetScopeService<DataRepository.IRepository>(); } }

        internal async Task<T> InitCreate(T model)
        {
            if (model is BaseModel)
            { (model as BaseModel).InitId(); }
            if (model is BaseCreateModel)
            { await (model as BaseCreateModel).InitCreate(); }
            return model;
        }

        internal async Task<T> InitModify(T model)
        {
            if (model is BaseModifyModel)
            { await (model as BaseModifyModel).InitModify(); }
            return model;
        }

        public async Task<PageData<T>> DefualtGetList(SearchArguments searchArguments)
        {
            return await DB.QueryPageListAsync<T>(searchArguments);
        }

        public async Task<T> DefualtInsert(T entity)
        {
            entity = await InitCreate(entity);
            await DB.InsertAsync<T>(entity);
            return entity;
        }

        public async Task<T> DefualtGetById(string id)
        {
            return await DB.GetByIdAsync<T>(id);
        }
    }
}