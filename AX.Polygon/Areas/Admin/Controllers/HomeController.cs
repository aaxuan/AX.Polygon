using Microsoft.AspNetCore.Mvc;

namespace AX.Polygon.Areas.Admin.Controllers
{
    [Area("admin")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        #region api

        public IActionResult GetMenu()
        {
            throw new System.Exception();
        }

        #endregion api
    }
}