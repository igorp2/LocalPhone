using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using LocalPhoneAdmin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using LocalPhoneDomain.Areas.Identity.Data;
using LocalPhoneAdmin.Data;

namespace LocalPhoneAdmin.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
