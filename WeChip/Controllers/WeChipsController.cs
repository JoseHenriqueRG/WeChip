using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WeChip.Data;
using WeChip.Models;

namespace WeChip.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class WeChipsController : ControllerBase
    {
        private readonly WeChipDataBaseContext _context;

        public WeChipsController(WeChipDataBaseContext context)
        {
            _context = context;
        }

        // POST: api/WeChip
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Produto>> PostProduto(Produto produto)
        {
            _context.Produto.Add(produto);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProduto", new { id = produto.CodigoProduto }, produto);
        }

        private bool ProdutoExists(int id)
        {
            return _context.Produto.Any(e => e.CodigoProduto == id);
        }
    }
}
