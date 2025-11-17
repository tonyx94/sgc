using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SGC.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;


namespace SGC.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Menu()
        {
            ViewBag.Usuario = HttpContext.Session.GetString("Usuario");
            ViewBag.Rol = HttpContext.Session.GetString("Rol");
            return View();
        }

    }
}

