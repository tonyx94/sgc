using Microsoft.AspNetCore.Mvc;
using SGC.Business.Services;
using SGC.Data.Entities;
using System.Security.Claims;

namespace SGC.Controllers
{
    public class SolicitudController : Controller
    {
        private readonly ISolicitudService _solicitudService;
        private readonly ClienteService _clienteService;

        public SolicitudController(ISolicitudService solicitudService, ClienteService clienteService)
        {
            _solicitudService = solicitudService;
            _clienteService = clienteService;
        }

        public async Task<IActionResult> Index()
        {

            var rol = HttpContext.Session.GetString("Rol");
            var userId = HttpContext.Session.GetInt32("UsuarioId");

            if (rol == null || userId == null)
            {
                return RedirectToAction("Index", "Login");
            }

            var solicitudes = await _solicitudService.ObtenerTodas(rol, userId.Value);
            
            ViewBag.CurrentRole = rol;
            ViewBag.CurrentUserId = userId.Value;

            return View(solicitudes);
        }

        public async Task<IActionResult> Crear()
        {
            ViewBag.Clientes = await _clienteService.ObtenerActivosAsync();
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Crear([FromForm] SGC.Models.SolicitudViewModel viewModel)
        {
             var userId = HttpContext.Session.GetInt32("UsuarioId");
             if (userId == null) return Json(new { success = false, message = "Sesión expirada" });

             string documentoPath = "";
             if (viewModel.Archivo != null && viewModel.Archivo.Length > 0)
             {
                 var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "documentos");
                 if (!Directory.Exists(uploadsFolder))
                 {
                     Directory.CreateDirectory(uploadsFolder);
                 }
                 var uniqueFileName = Guid.NewGuid().ToString() + "_" + viewModel.Archivo.FileName;
                 var filePath = Path.Combine(uploadsFolder, uniqueFileName);
                 using (var fileStream = new FileStream(filePath, FileMode.Create))
                 {
                     await viewModel.Archivo.CopyToAsync(fileStream);
                 }
                 documentoPath = "/uploads/documentos/" + uniqueFileName;
             }

             var solicitud = new Solicitud
             {
                 ClienteId = viewModel.ClienteId,
                 Monto = viewModel.Monto,
                 Comentarios = viewModel.Comentarios,
                 Documentos = documentoPath,
                 UsuarioId = userId.Value,
                 Estado = "Registrado",
                 FechaCreacion = DateTime.Now
             };

             var result = await _solicitudService.Crear(solicitud);
             
             if (result == "OK")
             {
                 return Json(new { success = true });
             }
             else
             {
                 return Json(new { success = false, message = result });
             }
        }


        [HttpPost]
        public async Task<IActionResult> ActualizarEstado(int id, string nuevoEstado, string comentario, string accion)
        {
            var userId = HttpContext.Session.GetInt32("UsuarioId");
            if (userId == null) return Json(new { success = false, message = "Sesión expirada" });


            var rol = HttpContext.Session.GetString("Rol");


            var result = await _solicitudService.ActualizarEstado(id, nuevoEstado, comentario, userId.Value, accion);
            
            return Json(new { success = true });
        }

        [HttpGet]
        public async Task<IActionResult> GetBitacora(int id)
        {
            var bitacora = await _solicitudService.ObtenerBitacora(id);

            var result = bitacora.Select(b => new {
                fecha = b.Fecha.ToString("g"),
                accion = b.Accion,
                comentario = b.Comentario,
                usuario = b.Usuario.Username + " (" + b.Usuario.Rol + ")"
            });

             return Json(new { success = true, data = result });
        }
    }
}
