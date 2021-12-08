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
    public class OfertasController : Controller
    {
        private readonly WeChipDataBaseContext _context;

        public OfertasController(WeChipDataBaseContext context)
        {
            _context = context;
        }

        // GET: Ofertas/Create
        public IActionResult Create(int id)
        {
            ViewBag.Cliente = _context.Cliente.Include(c => c.Status).FirstOrDefault(c => c.ID == id);

            ViewBag.Produtos = _context.Produto.ToList();

            ViewBag.Status = _context.Status.ToList();

            return View();
        }

        // POST: Ofertas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Cliente,Cep,Rua,Numero,Complemento,Bairro,Cidade,Estado,Produtos,Status")] Oferta oferta)
        {
            if (ModelState.IsValid)
            {
                _context.Add(oferta);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(oferta);
        }

        private bool OfertaExists(int id)
        {
            return _context.Oferta.Any(e => e.ID == id);
        }
    }
}
