using Microsoft.AspNetCore.Mvc;
using SGC.Business.Services;
using SGC.Business.Dtos;

namespace SGC.Web.Controllers
{
    public class ClienteController : Controller
    {
        private readonly ClienteService _clienteService;

        public ClienteController(ClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        // GET: Cliente/Index
        public async Task<IActionResult> Index()
        {
            var clientes = await _clienteService.ObtenerTodosAsync();
            return View(clientes);
        }

        // GET: Cliente/Create
        public IActionResult Create()
        {
            return View(new ClienteDTO());
        }

        // POST: Cliente/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ClienteDTO clienteDto)
        {
            if (!ModelState.IsValid)
            {
                return View(clienteDto);
            }

            var resultado = await _clienteService.CrearAsync(clienteDto);

            if (resultado.Success)
            {
                TempData["Success"] = resultado.Message;
                return RedirectToAction(nameof(Index));
            }

            TempData["Error"] = resultado.Message;
            return View(clienteDto);
        }

        // GET: Cliente/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var cliente = await _clienteService.ObtenerPorIdAsync(id);
            if (cliente == null)
            {
                TempData["Error"] = "Cliente no encontrado";
                return RedirectToAction(nameof(Index));
            }

            return View(cliente);
        }

        // POST: Cliente/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ClienteDTO clienteDto)
        {
            if (id != clienteDto.Id)
            {
                TempData["Error"] = "ID de cliente no coincide";
                return RedirectToAction(nameof(Index));
            }

            if (!ModelState.IsValid)
            {
                return View(clienteDto);
            }

            var resultado = await _clienteService.ActualizarAsync(clienteDto);

            if (resultado.Success)
            {
                TempData["Success"] = resultado.Message;
                return RedirectToAction(nameof(Index));
            }

            TempData["Error"] = resultado.Message;
            return View(clienteDto);
        }

        // GET: Cliente/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var cliente = await _clienteService.ObtenerPorIdAsync(id);
            if (cliente == null)
            {
                TempData["Error"] = "Cliente no encontrado";
                return RedirectToAction(nameof(Index));
            }

            return View(cliente);
        }

        // POST: Cliente/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var resultado = await _clienteService.EliminarAsync(id);

            if (resultado.Success)
            {
                TempData["Success"] = resultado.Message;
            }
            else
            {
                TempData["Error"] = resultado.Message;
            }

            return RedirectToAction(nameof(Index));
        }
    }
}