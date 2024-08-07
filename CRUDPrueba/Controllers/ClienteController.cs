using CRUDPrueba.Data;
using CRUDPrueba.Models;
using Microsoft.AspNetCore.Mvc;

namespace CRUDPrueba.Controllers
{
    public class ClienteController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ClienteController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(string busqueda)
        {
            var clientes = _context.ReadClientesSP().ToList();

            if (!string.IsNullOrWhiteSpace(busqueda))
            {
                var lower = busqueda.Trim().ToLower();
                clientes = clientes
                    .Where(c => c.Tipo.Trim().ToLower() == lower)
                    .ToList();
            }

            return View(clientes /*?? new List<Cliente>()*/);

        }

        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Crear(Cliente cliente)
        {
            if (ModelState.IsValid && cliente.Nombre is not null)
            {
                _context.CreateClienteSP(cliente.Nombre, cliente.Apellido, cliente.Tipo);
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Actualizar(int cliente_id)
        {
            var cliente = _context.SearchClienteSP(cliente_id);
            return View(cliente);
        }

        [HttpPost]
        public IActionResult Actualizar(Cliente cliente)
        {
            if (ModelState.IsValid && cliente.Nombre is not null && cliente.Cliente_Id > 0)
            {
                _context.UpdateClienteSP(cliente.Cliente_Id, cliente.Nombre, cliente.Apellido, cliente.Tipo);
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Eliminar(int cliente_id)
        {
            var cliente = _context.SearchClienteSP(cliente_id);
            return View(cliente);
        }

        [HttpPost]
        public IActionResult Eliminar(Cliente cliente)
        {
            if (cliente.Cliente_Id > 0)
            {
                _context.DeleteClienteSP(cliente.Cliente_Id);
                return RedirectToAction("Index");
            }
            return View();
        }



    }
}
