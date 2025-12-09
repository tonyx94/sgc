using Microsoft.AspNetCore.Mvc;
using SGC.Business.Dtos;
using SGC.Business.Services;
using Microsoft.AspNetCore.Http;


namespace SGC.Web.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IUsuarioService _service;

        public UsuarioController(IUsuarioService service)
        {
            _service = service;
        }
        private bool EsAdmin()
        {
            return HttpContext.Session.GetString("Rol") == "Administrador";
        
        }

        // GET: /Usuario
        public async Task<IActionResult> Index()
        {
            if (!EsAdmin()) return RedirectToAction("Index", "Home");

            var usuarios = await _service.GetUsuariosAsync();
            return View(usuarios);
        }

        // GET: /Usuario/Crear
        public IActionResult Crear()
        {
            if (!EsAdmin()) return RedirectToAction("Index", "Home");

            return View(new UsuarioDTO());
        }

        // POST: /Usuario/Crear
        [HttpPost]
        public async Task<IActionResult> Crear(UsuarioDTO dto)
        {
            if (!EsAdmin()) return RedirectToAction("Index", "Home");

            if (!ModelState.IsValid)
                return View(dto);

            bool creado = await _service.CrearUsuarioAsync(dto);

            if (!creado)
            {
                ViewBag.Error = "El nombre de usuario ya existe.";
                return View(dto);
            }

            return RedirectToAction("Index");
        }

        // GET: /Usuario/Editar/5
        public async Task<IActionResult> Editar(int id)
        {
            if (!EsAdmin()) return RedirectToAction("Index", "Home");

            var usuario = await _service.GetByIdAsync(id);
            if (usuario == null)
                return NotFound();

            return View(usuario);
        }

        // POST: /Usuario/Editar
        [HttpPost]
        public async Task<IActionResult> Editar(UsuarioDTO dto)
        {
            if (!EsAdmin()) return RedirectToAction("Index", "Home");

            if (!ModelState.IsValid)
                return View(dto);

            await _service.ActualizarUsuarioAsync(dto);
            return RedirectToAction("Index");
        }

        // GET: /Usuario/Borrar/5
        public async Task<IActionResult> Borrar(int id)
        {
            if (!EsAdmin()) return RedirectToAction("Index", "Home");

            await _service.BorrarUsuarioAsync(id);
            return RedirectToAction("Index");
        }
    }
}
