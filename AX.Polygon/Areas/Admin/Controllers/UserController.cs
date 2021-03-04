using AX.Polygon.WebCore;
using Microsoft.AspNetCore.Mvc;

namespace AX.Polygon.Areas.Admin.Controllers
{
    [Area("admin")]
    [Filter.AuthorizeFilter()]
    public class UserController : BaseController
    {
        public IActionResult List()
        {
            return View();
        }
    }
}