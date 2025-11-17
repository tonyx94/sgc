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
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                ViewBag.Error = "Debe ingresar el usuario y la contraseña.";
                return View();
            }

            var user = await _loginService.ValidarUsuarioAsync(username, password);

            if (user != null)
            {
                HttpContext.Session.SetString("Usuario", user.Username);
                HttpContext.Session.SetString("Rol", user.Rol);
                return RedirectToAction("Menu", "Home");
            }

            ViewBag.Error = "Usuario o contraseña incorrectos. Intente nuevamente.";
            return View();
        }

    }
}
