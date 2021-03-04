using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;
using System.Web;

namespace AX.Polygon.Filter
{
    public class AuthorizeFilterAttribute : ActionFilterAttribute
    {
        public AuthorizeFilterAttribute()
        {
        }

        public AuthorizeFilterAttribute(string authCode) => this.AuthCode = authCode;

        public string AuthCode { get; set; }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            bool hasPermission = false;

            //判断是否浏览器Json请求
            bool isJsonRequest = false;
            if (context.HttpContext.Request.Headers != null)
            { isJsonRequest = context.HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest"; }

            var user = await Controllers.AuthController.CurrentUser();

            //没有登录或超时
            if (user == null)
            {
                if (isJsonRequest)
                {
                    context.Result = new JsonResult(new Util.JsonResultMessage() { Code = -999, Message = "您没有登录或身份验证已超时，请重新登录" });
                    return;
                }
                else
                {
                    context.Result = new RedirectResult("~/Home/Login");
                    return;
                }
            }

            //验证用户权限
            //admin 全部权限
            if (user.IsAdmin)
            { hasPermission = true; }

            // 权限判断
            if (!string.IsNullOrEmpty(AuthCode))
            {
                //string[] authorizeList = AuthCode.Split(',');
                //TData<List<MenuAuthorizeInfo>> objMenuAuthorize = await new MenuAuthorizeBLL().GetAuthorizeList(user);
                //List<MenuAuthorizeInfo> authorizeInfoList = objMenuAuthorize.Data.Where(p => authorizeList.Contains(p.Authorize)).ToList();
                //if (authorizeInfoList.Any())
                //{
                //    hasPermission = true;

                //    #region 新增和修改判断

                //    if (context.RouteData.Values["Action"].ToString() == "SaveFormJson")
                //    {
                //        var id = context.HttpContext.Request.Form["Id"];
                //        if (id.ParseToLong() > 0)
                //        {
                //            if (!authorizeInfoList.Where(p => p.Authorize.Contains("edit")).Any())
                //            {
                //                hasPermission = false;
                //            }
                //        }
                //        else
                //        {
                //            if (!authorizeInfoList.Where(p => p.Authorize.Contains("add")).Any())
                //            {
                //                hasPermission = false;
                //            }
                //        }
                //    }

                //    #endregion 新增和修改判断
                //}
            }

            if (hasPermission)
            {
                var resultContext = await next();
            }
            else
            {
                if (isJsonRequest)
                {
                    context.Result = new JsonResult(new Util.JsonResultMessage() { Code = -999, Message = "您没有该操作权限" });
                    return;
                }
                else
                {
                    context.Result = new RedirectResult("~/Home/Error?message=" + HttpUtility.UrlEncode("您没有该操作权限"));
                }
            }
        }
    }
}