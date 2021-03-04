using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;

namespace AX.Polygon.Controllers
{
    public class HomeController : Controller
    {
        #region 视图

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]

        public IActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Error(string message)
        {
            ViewBag.Message = message;
            return View();
        }

        #endregion 视图

        [HttpGet]
        public IActionResult Test()
        {
            var result = new Util.JsonResultMessage<List<KeyValuePair<string, object>>>();
            result.Data = new Admin.Services.ServerInformationService().GetServerInfo();
            result.Code = 1;
            result.Message = "请求成功";
            return new JsonResult(result);
        }

        [HttpGet]
        public IActionResult GetCreateSystemSQL()
        {
            var sql = Util.IOCManager.GetScopeService<DataRepository.IRepository>().GetCreateTableSql();
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(sql);
            writer.Flush();
            stream.Position = 0;

            var actionresult = new FileStreamResult(stream, new Microsoft.Net.Http.Headers.MediaTypeHeaderValue("text/txt"));
            actionresult.FileDownloadName = "CreateSystemTableSql.txt";
            return actionresult;
        }
    }
}