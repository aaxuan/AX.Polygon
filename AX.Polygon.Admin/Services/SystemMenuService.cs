using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AX.Polygon.Admin.Services
{
    class SystemMenuService
    {
        //public class MenuAuthorizeBLL
        //{
        //    private MenuAuthorizeCache menuAuthorizeCache = new MenuAuthorizeCache();
        //    private MenuCache menuCache = new MenuCache();

        //    #region 获取数据
        //    public async Task<TData<List<MenuAuthorizeInfo>>> GetAuthorizeList(OperatorInfo user)
        //    {
        //        TData<List<MenuAuthorizeInfo>> obj = new TData<List<MenuAuthorizeInfo>>();
        //        obj.Data = new List<MenuAuthorizeInfo>();

        //        List<MenuAuthorizeEntity> authorizeList = new List<MenuAuthorizeEntity>();
        //        List<MenuAuthorizeEntity> userAuthorizeList = null;
        //        List<MenuAuthorizeEntity> roleAuthorizeList = null;

        //        var menuAuthorizeCacheList = await menuAuthorizeCache.GetList();
        //        var menuList = await menuCache.GetList();
        //        var enableMenuIdList = menuList.Where(p => p.MenuStatus == (int)StatusEnum.Yes).Select(p => p.Id).ToList();

        //        menuAuthorizeCacheList = menuAuthorizeCacheList.Where(p => enableMenuIdList.Contains(p.MenuId)).ToList();

        //        // 用户
        //        userAuthorizeList = menuAuthorizeCacheList.Where(p => p.AuthorizeId == user.UserId && p.AuthorizeType == AuthorizeTypeEnum.User.ParseToInt()).ToList();

        //        // 角色
        //        if (!string.IsNullOrEmpty(user.RoleIds))
        //        {
        //            List<long> roleIdList = user.RoleIds.Split(',').Select(p => long.Parse(p)).ToList();
        //            roleAuthorizeList = menuAuthorizeCacheList.Where(p => roleIdList.Contains(p.AuthorizeId.Value) && p.AuthorizeType == AuthorizeTypeEnum.Role.ParseToInt()).ToList();
        //        }

        //        // 排除重复的记录
        //        if (userAuthorizeList.Count > 0)
        //        {
        //            authorizeList.AddRange(userAuthorizeList);
        //            roleAuthorizeList = roleAuthorizeList.Where(p => !userAuthorizeList.Select(u => u.AuthorizeId).Contains(p.AuthorizeId)).ToList();
        //        }
        //        if (roleAuthorizeList != null && roleAuthorizeList.Count > 0)
        //        {
        //            authorizeList.AddRange(roleAuthorizeList);
        //        }

        //        foreach (MenuAuthorizeEntity authorize in authorizeList)
        //        {
        //            obj.Data.Add(new MenuAuthorizeInfo
        //            {
        //                MenuId = authorize.MenuId,
        //                AuthorizeId = authorize.AuthorizeId,
        //                AuthorizeType = authorize.AuthorizeType,
        //                Authorize = menuList.Where(t => t.Id == authorize.MenuId).Select(t => t.Authorize).FirstOrDefault()
        //            });
        //        }
        //        obj.Tag = 1;
        //        return obj;
        //    }
        //    #endregion
        //}











        //private MenuService menuService = new MenuService();

        //private MenuCache menuCache = new MenuCache();

        //#region 获取数据
        //public async Task<TData<List<MenuEntity>>> GetList(MenuListParam param)
        //{
        //    var obj = new TData<List<MenuEntity>>();

        //    List<MenuEntity> list = await menuCache.GetList();
        //    list = ListFilter(param, list);

        //    obj.Data = list;
        //    obj.Tag = 1;
        //    return obj;
        //}

        //public async Task<TData<List<ZtreeInfo>>> GetZtreeList(MenuListParam param)
        //{
        //    var obj = new TData<List<ZtreeInfo>>();
        //    obj.Data = new List<ZtreeInfo>();

        //    List<MenuEntity> list = await menuCache.GetList();
        //    list = ListFilter(param, list);

        //    foreach (MenuEntity menu in list)
        //    {
        //        obj.Data.Add(new ZtreeInfo
        //        {
        //            id = menu.Id,
        //            pId = menu.ParentId,
        //            name = menu.MenuName
        //        });
        //    }

        //    obj.Tag = 1;
        //    return obj;
        //}

        //public async Task<TData<MenuEntity>> GetEntity(long id)
        //{
        //    TData<MenuEntity> obj = new TData<MenuEntity>();
        //    obj.Data = await menuService.GetEntity(id);
        //    if (obj.Data != null)
        //    {
        //        long parentId = obj.Data.ParentId.Value;
        //        if (parentId > 0)
        //        {
        //            MenuEntity parentMenu = await menuService.GetEntity(parentId);
        //            if (parentMenu != null)
        //            {
        //                obj.Data.ParentName = parentMenu.MenuName;
        //            }
        //        }
        //        else
        //        {
        //            obj.Data.ParentName = "主目录";
        //        }
        //    }
        //    obj.Tag = 1;
        //    return obj;
        //}

        //public async Task<TData<int>> GetMaxSort(long parentId)
        //{
        //    TData<int> obj = new TData<int>();
        //    obj.Data = await menuService.GetMaxSort(parentId);
        //    obj.Tag = 1;
        //    return obj;
        //}
        //#endregion

        //#region 提交数据
        //public async Task<TData<string>> SaveForm(MenuEntity entity)
        //{
        //    TData<string> obj = new TData<string>();
        //    if (menuService.ExistMenuName(entity))
        //    {
        //        obj.Message = "菜单名称已经存在！";
        //        return obj;
        //    }
        //    await menuService.SaveForm(entity);

        //    menuCache.Remove();

        //    obj.Data = entity.Id.ParseToString();
        //    obj.Tag = 1;
        //    return obj;
        //}

        //public async Task<TData> DeleteForm(string ids)
        //{
        //    TData obj = new TData();
        //    await menuService.DeleteForm(ids);

        //    menuCache.Remove();
        //    obj.Tag = 1;
        //    return obj;
        //}
        //#endregion

        //#region 私有方法
        //private List<MenuEntity> ListFilter(MenuListParam param, List<MenuEntity> list)
        //{
        //    if (param != null)
        //    {
        //        if (!string.IsNullOrEmpty(param.MenuName))
        //        {
        //            list = list.Where(p => p.MenuName.Contains(param.MenuName)).ToList();
        //        }
        //        if (param.MenuStatus > 0)
        //        {
        //            list = list.Where(p => p.MenuStatus == param.MenuStatus).ToList();
        //        }
        //    }
        //    return list;
        //}
        //#endregion
    }



}
