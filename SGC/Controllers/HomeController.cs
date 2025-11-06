using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SGC.Models;

using Microsoft.AspNetCore.Mvc;

namespace SGC.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Menu()
        {
            ViewBag.Usuario = TempData["Usuario"];
            ViewBag.Rol = TempData["Rol"];
            return View();
        }
    }
}

