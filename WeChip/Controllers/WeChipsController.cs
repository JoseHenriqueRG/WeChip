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

        // GET: api/WeChip/ConsultarVendas?produto=&cpf=
        [HttpGet("ConsultarVendas")]
        public async Task<ActionResult<IEnumerable<dynamic>>> ConsultarVendas(string produto, string cpf)
        {
            var ofertas = from o in _context.Oferta
                          where o.Cliente.Status.ContabilizaVenda == true
                          select o;

            // Filtrar produto
            if (!string.IsNullOrEmpty(produto))
            {
                ofertas = ofertas.Where(o => o.OfertaProdutos.Any(p => p.Produto.Descricao.Contains(produto)));
            }

            // Filtrar cpf
            if (!string.IsNullOrEmpty(cpf))
            {
                cpf = cpf.Replace(".", "").Replace("-", "");
                ofertas = ofertas.Where(o => o.Cliente.Cpf.Contains(cpf));
            }

            // montar os dados de retorno
            return await ofertas.Include(o => o.Cliente)
                                .Include(o => o.OfertaProdutos)
                                    .ThenInclude(op => op.Produto)
                                .Select(o => new { 
                                    CodigoOferta = o.ID, 
                                    Cliente = new {
                                        o.Cliente.Nome,
                                        o.Cliente.Cpf,
                                        o.Cliente.Credito,
                                        o.Cliente.Telefone,
                                    },
                                    Produtos = o.OfertaProdutos.Select(op => op.Produto)
                                        .Select(p => new
                                        {
                                            p.CodigoProduto,
                                            p.Descricao,
                                            p.Preco,
                                            Tipo = p.Tipo.ToString()
                                        }).ToList(),
                                    o.ValorTotal
                                })
                                .ToListAsync();
        }

        // POST: api/WeChip/CadastrarProduto
        [HttpPost("CadastrarProduto")]
        [ProducesResponseType(201)]
        public async Task<ActionResult<Produto>> PostProduto(Produto produto)
        {
            // Se o produto já existe não pode ser cadastrado novamente
            if(ProdutoExists(produto.CodigoProduto))
            {
                return BadRequest("Produto já cadastrado.");
            }

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
