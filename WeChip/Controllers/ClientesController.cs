using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using WeChip.Data;
using WeChip.Models;

namespace WeChip.Controllers
{
    public class ClientesController : Controller
    {
        private readonly WeChipDataBaseContext _context;

        public ClientesController(WeChipDataBaseContext context)
        {
            _context = context;
        }

        // GET: Cliestes
        public async Task<IActionResult> Index(string filtro)
        {
            if (TempData.Keys.Contains("Message"))
                ViewBag.Message = TempData["Message"];

            var clientes = from c in _context.Cliente
                           where c.Status.FinalizaCliente == false
                           select c;

            if (!String.IsNullOrEmpty(filtro))
            {
                filtro = filtro.Replace(".", "").Replace("-", "");
                clientes = clientes.Where(c => c.Nome.Contains(filtro) || c.Cpf.Contains(filtro));
            }

            return View(await clientes.Include(c => c.Status).Take(20).ToListAsync());
        }

        // GET: Clientes/Create
        public IActionResult Create()
        {
            if (TempData.Keys.Contains("Message"))
                ViewBag.Message = TempData["Message"];
            
            return View();
        }

        // POST: Clientes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nome,Cpf,CreditoString,Telefone")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                cliente.Cpf = cliente.Cpf.Replace(".", "").Replace("-", "");
                cliente.Credito = Convert.ToDecimal(cliente.CreditoString);
                cliente.Status = _context.Status.Where(s => s.Descricao == "Nome Disponível").FirstOrDefault();
                
                _context.Add(cliente);
                await _context.SaveChangesAsync();

                TempData["Message"] = "Cliente salvo com sucesso!";
                return RedirectToAction(nameof(Index));
            }
            TempData["Message"] = "Ocorreu um erro inesperado. Tente novamente mais tarde.";
            return View(cliente);
        }
    }
}
