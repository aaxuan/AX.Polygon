﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AX.Polygon.Admin.Services
{
    class NewsService
    {
        //private AreaBLL areaBLL = new AreaBLL();
        //private NewsService newsService = new NewsService();

        //#region 获取数据
        //public async Task<TData<List<NewsEntity>>> GetList(NewsListParam param)
        //{
        //    TData<List<NewsEntity>> obj = new TData<List<NewsEntity>>();
        //    areaBLL.SetAreaParam(param);
        //    obj.Data = await newsService.GetList(param);
        //    obj.Total = obj.Data.Count;
        //    obj.Tag = 1;
        //    return obj;
        //}

        //public async Task<TData<List<NewsEntity>>> GetPageList(NewsListParam param, Pagination pagination)
        //{
        //    TData<List<NewsEntity>> obj = new TData<List<NewsEntity>>();
        //    areaBLL.SetAreaParam(param);
        //    obj.Data = await newsService.GetPageList(param, pagination);
        //    obj.Total = pagination.TotalCount;
        //    obj.Tag = 1;
        //    return obj;
        //}

        //public async Task<TData<List<NewsEntity>>> GetPageContentList(NewsListParam param, Pagination pagination)
        //{
        //    TData<List<NewsEntity>> obj = new TData<List<NewsEntity>>();
        //    obj.Data = await newsService.GetPageContentList(param, pagination);
        //    obj.Total = pagination.TotalCount;
        //    obj.Tag = 1;
        //    return obj;
        //}

        //public async Task<TData<NewsEntity>> GetEntity(long id)
        //{
        //    TData<NewsEntity> obj = new TData<NewsEntity>();
        //    obj.Data = await newsService.GetEntity(id);
        //    areaBLL.SetAreaId(obj.Data);
        //    obj.Tag = 1;
        //    return obj;
        //}

        //public async Task<TData<int>> GetMaxSort()
        //{
        //    TData<int> obj = new TData<int>();
        //    obj.Data = await newsService.GetMaxSort();
        //    obj.Tag = 1;
        //    return obj;
        //}
        //#endregion

        //#region 提交数据
        //public async Task<TData<string>> SaveForm(NewsEntity entity)
        //{
        //    TData<string> obj = new TData<string>();
        //    areaBLL.SetAreaEntity(entity);
        //    await newsService.SaveForm(entity);
        //    obj.Data = entity.Id.ParseToString();
        //    obj.Tag = 1;
        //    return obj;
        //}

        //public async Task<TData> DeleteForm(string ids)
        //{
        //    TData obj = new TData();
        //    await newsService.DeleteForm(ids);
        //    obj.Tag = 1;
        //    return obj;
        //}
        //#endregion

        //#region 私有方法
        //#endregion
    }
}
