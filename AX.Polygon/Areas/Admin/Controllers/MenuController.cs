using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AX.Polygon.Areas.Admin.Controllers
{
    [Area("admin")]
    [Filter.AuthorizeFilter()]
    public class MenuController : WebCore.BaseController
    {
        #region 接口

        [HttpGet]
        public async Task<IActionResult> GetUserMenu()
        {
            return SuccessResultMessage(await new Polygon.Admin.Services.SystemMenuService().GetUserMenu());
        }

        #endregion 接口
    }
}