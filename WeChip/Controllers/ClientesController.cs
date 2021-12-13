using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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
            {
                ViewBag.Message = TempData["Message"];
            }

            var clientes = from c in _context.Cliente
                           where c.Status.FinalizaCliente == false
                           select c;

            if (!String.IsNullOrEmpty(filtro))
            {
                clientes = clientes.Where(c => c.Nome!.Contains(filtro) || c.Cpf.Contains(filtro));
            }             

            return View(await clientes.Include(c => c.Status).Take(20).ToListAsync());
        }

        // GET: Clientes/Create
        public IActionResult Create()
        {
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
                cliente.Credito = Convert.ToDecimal(cliente.CreditoString);
                cliente.Status = _context.Status.Where(s => s.Descricao == "Nome Disponível").FirstOrDefault();
                _context.Add(cliente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cliente);
        }
    }
}
