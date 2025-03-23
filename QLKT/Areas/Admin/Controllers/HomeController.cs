using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace QLKT.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize] // Bắt buộc phải đăng nhập
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
