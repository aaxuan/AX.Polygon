using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;
using System.Web;

namespace AX.Polygon.Filter
{
    public class GlobalExceptionFilter : IExceptionFilter, IAsyncExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            //判断是否浏览器Json请求
            bool isJsonRequest = false;
            if (context.HttpContext.Request.Headers != null)
            { isJsonRequest = context.HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest"; }

            if (isJsonRequest)
            {
                var result = new Util.JsonResultMessage();
                //统一处理警告消息
                if (context.Exception.GetType().FullName == typeof(AX.Polygon.Util.WarningMessageException).FullName)
                {
                    result.Code = 0;
                    result.Message = context.Exception.Message;
                }
                else
                {
                    result.Code = -999;
                    result.Message = context.Exception.Message;
                }
                context.Result = new JsonResult(result);
                context.ExceptionHandled = true;
            }
            else
            {
                string errorMessage = context.Exception.Message;
                context.Result = new RedirectResult("~/Home/Error?message=" + HttpUtility.UrlEncode(errorMessage));
                context.ExceptionHandled = true;
            }
        }

        public Task OnExceptionAsync(ExceptionContext context)
        {
            OnException(context);
            return Task.CompletedTask;
        }
    }
}