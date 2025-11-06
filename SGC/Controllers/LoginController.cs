using Microsoft.AspNetCore.Mvc;
using SGC.Business.Services;

namespace SGC.Web.Controllers
{
    public class LoginController : Controller
    {
        private readonly LoginService _loginService;

        public LoginController(LoginService loginService)
        {
            _loginService = loginService;
        }

        public IActionResult Index() => View();

        [HttpPost]
        public async Task<IActionResult> Index(string username, string password)
        {
            var userDto = await _loginService.ValidarUsuarioAsync(username, password);

            if (userDto != null)
            {
                TempData["Usuario"] = userDto.Username;
                TempData["Rol"] = userDto.Rol;
                return RedirectToAction("Menu", "Home");
            }

            ViewBag.Error = "Usuario o contraseña incorrectos";
            return View();
        }
    }
}
