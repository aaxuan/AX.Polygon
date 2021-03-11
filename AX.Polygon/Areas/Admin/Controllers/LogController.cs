using AX.DataRepository.Models;
using AX.Polygon.WebCore;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AX.Polygon.Areas.Admin.Controllers
{
    [Area("admin")]
    [Filter.AuthorizeFilter()]
    public class LogController : BaseController
    {
        public IActionResult List()
        {
            return View();
        }

        #region 接口

        [HttpPost]
        public async Task<IActionResult> GetList([FromBody] FetchParameter searchArguments)
        {
            return SuccessResultMessage(await new Polygon.Admin.Services.SystemLogService().DefualtGetList(searchArguments));
        }

        #endregion 接口
    }
}