using AX.Polygon.Admin.DataModel;
using AX.Polygon.Admin.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AX.Polygon.Controllers
{
    public class AuthController : Controller
    {
        private const string TokenName = "AX.Polygon.Token";

        #region 接口

        [HttpPost]
        public async Task<IActionResult> Login(string loginname, string password)
        {
            var user = await new UserService().LoginCheck(loginname, password);
            //签发 cookie
            Util.CookieHelper.Write(TokenName, user.Id);
            return new JsonResult(new Util.JsonResultMessage<User>() { Data = user, Code = 1 });
        }

        [HttpGet]
        public void LogOut()
        {
            Util.CookieHelper.Remove(TokenName);
        }

        #endregion 接口

        public static async Task<User> CurrentUser()
        {
            var token = Util.CookieHelper.GetValue(TokenName);
            if (string.IsNullOrWhiteSpace(token)) { return null; }

            //TODO
            //构造 带权限的 user
            var user = await new UserService().DefualtGetById(token);
            return user;
        }
    }
}