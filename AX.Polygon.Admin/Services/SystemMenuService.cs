using AX.Polygon.Admin.DataModel;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AX.Polygon.Admin.Services
{
    public class SystemMenuService : BaseService<SystemMenu>
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

        public async Task<List<DataModel.DTO.MenuTreeResult>> GetUserMenu()
        {
            var menus = await DB.GetAllAsync<SystemMenu>();
            var rootMenu = menus.ToList().Where(p => p.ParentId == "0").ToList();

            var result = new List<DataModel.DTO.MenuTreeResult>();
            foreach (var item in rootMenu)
            {
                var firstResultItem = new DataModel.DTO.MenuTreeResult();
                firstResultItem.Menu = item;
                firstResultItem.Child = new List<DataModel.DTO.MenuTreeResult>();
                var firstResultItemChild = menus.Where(p => p.ParentId == item.Id);
                foreach (var item1 in firstResultItemChild)
                {
                    var scendResultItem = new DataModel.DTO.MenuTreeResult();
                    scendResultItem.Menu = item1;
                    scendResultItem.Child = new List<DataModel.DTO.MenuTreeResult>();
                    var scendResultItemChild = menus.Where(p => p.ParentId == item1.Id);
                    foreach (var item2 in scendResultItemChild)
                    {
                        var thirdResultItem = new DataModel.DTO.MenuTreeResult();
                        thirdResultItem.Menu = item2;
                        scendResultItem.Child.Add(thirdResultItem);
                    }
                    firstResultItem.Child.Add(scendResultItem);
                }
                result.Add(firstResultItem);
            }

            return result;
        }

        //        {
        //	"0": {
        //		"title": "常规管理",
        //		"icon": "fa fa-address-book",
        //		"href": "",
        //		"target": "_self",
        //		"child": [
        //			{
        //				"title": "主页模板",
        //				"href": "",
        //				"icon": "fa fa-home",
        //				"target": "_self",
        //				"child": [
        //					{
        //						"title": "主页一",
        //						"href": "page/welcome-1.html",
        //						"icon": "fa fa-tachometer",
        //						"target": "_self"
        //					},
        //					{
        //						"title": "主页二",
        //						"href": "page/welcome-2.html",
        //						"icon": "fa fa-tachometer",
        //						"target": "_self"
        //					},
        //					{
        //	"title": "主页三",
        //						"href": "page/welcome-3.html",
        //						"icon": "fa fa-tachometer",
        //						"target": "_self"
        //					}
        //				]
        //			},
        //			{
        //	"title": "菜单管理",
        //				"href": "page/menu.html",
        //				"icon": "fa fa-window-maximize",
        //				"target": "_self"
        //			},
        //			{
        //	"title": "系统设置",
        //				"href": "page/setting.html",
        //				"icon": "fa fa-gears",
        //				"target": "_self"
        //			},
        //			{
        //	"title": "表格示例",
        //				"href": "page/table.html",
        //				"icon": "fa fa-file-text",
        //				"target": "_self"
        //			},
        //			{
        //	"title": "表单示例",
        //				"href": "",
        //				"icon": "fa fa-calendar",
        //				"target": "_self",
        //				"child": [
        //					{
        //		"title": "普通表单",
        //						"href": "page/form.html",
        //						"icon": "fa fa-list-alt",
        //						"target": "_self"
        //					},
        //					{
        //		"title": "分步表单",
        //						"href": "page/form-step.html",
        //						"icon": "fa fa-navicon",
        //						"target": "_self"
        //					}
        //				]
        //			},
        //			{
        //	"title": "登录模板",
        //				"href": "",
        //				"icon": "fa fa-flag-o",
        //				"target": "_self",
        //				"child": [
        //					{
        //		"title": "登录-1",
        //						"href": "page/login-1.html",
        //						"icon": "fa fa-stumbleupon-circle",
        //						"target": "_blank"
        //					},
        //					{
        //		"title": "登录-2",
        //						"href": "page/login-2.html",
        //						"icon": "fa fa-viacoin",
        //						"target": "_blank"
        //					},
        //					{
        //		"title": "登录-3",
        //						"href": "page/login-3.html",
        //						"icon": "fa fa-tags",
        //						"target": "_blank"
        //					}
        //				]
        //			},
        //			{
        //	"title": "异常页面",
        //				"href": "",
        //				"icon": "fa fa-home",
        //				"target": "_self",
        //				"child": [
        //					{
        //		"title": "404页面",
        //						"href": "page/404.html",
        //						"icon": "fa fa-hourglass-end",
        //						"target": "_self"
        //					}
        //				]
        //			},
        //			{
        //	"title": "其它界面",
        //				"href": "",
        //				"icon": "fa fa-snowflake-o",
        //				"target": "",
        //				"child": [
        //					{
        //		"title": "按钮示例",
        //						"href": "page/button.html",
        //						"icon": "fa fa-snowflake-o",
        //						"target": "_self"
        //					},
        //					{
        //		"title": "弹出层",
        //						"href": "page/layer.html",
        //						"icon": "fa fa-shield",
        //						"target": "_self"
        //					}
        //				]
        //			}
        //		]
        //	}
        //}
    }
}