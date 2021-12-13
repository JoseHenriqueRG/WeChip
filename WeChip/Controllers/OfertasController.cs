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
            if(TempData.Keys.Contains("Message"))
                ViewBag.Message = TempData["Message"];

            ViewBag.Cliente = _context.Cliente.Include(c => c.Status).FirstOrDefault(c => c.ID == id);

            ViewBag.Produtos = new SelectList(
                _context.Produto.Select(p => 
                    new
                    {
                        CodigoProduto = p.CodigoProduto,
                        Descricao = "Descrição: " + p.Descricao + " | Preço: R$" + p.Preco + " | Tipo: " + p.Tipo
                    })
                .ToList(), "CodigoProduto", "Descricao");

            ViewBag.Status = new SelectList(_context.Status
                .Where(s => s.Descricao != "Nome Disponível") // Forçar trocar o status inicial
                .ToList(), "CodigoStatus", "Descricao");

            return View();
        }

        // POST: Ofertas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Cliente,Cep,Rua,Numero,Complemento,Bairro,Cidade,Estado")] Oferta oferta, [Bind("CodigoStatus")] Status status, int[] produtos)
        {
            if (ModelState.IsValid)
            {
                status = _context.Status.FirstOrDefault(s => s.CodigoStatus == status.CodigoStatus);
                if (produtos.Length > 0)
                {
                    // Contendo produto(s) o endereço precisa ser preenchido
                    if(string.IsNullOrEmpty(oferta.Cep) || 
                       string.IsNullOrEmpty(oferta.Rua) || 
                       string.IsNullOrEmpty(oferta.Bairro) || 
                       string.IsNullOrEmpty(oferta.Cidade) || 
                       string.IsNullOrEmpty(oferta.Estado) || 
                       oferta.Numero == null)
                    {
                        TempData["Message"] = "Preencha os campos de endereço.";
                        return RedirectToAction("Create", new { id = oferta.Cliente.ID });
                    }

                    List<Produto> ListaProdutos = new();
                    var totalProd = 0.0M;
                    // Obter os produtos enviados no post e somar os valores
                    foreach (int item in produtos)
                    {
                        var produto = _context.Produto.FirstOrDefault(p => p.CodigoProduto == item);
                        ListaProdutos.Add(produto);
                        totalProd += produto.Preco;
                    }

                    var credito = Convert.ToDecimal(oferta.Cliente.Credito);
                    // Verificar se o cliente tem crédito para essa oferta de produtos
                    if (credito < totalProd)
                    {
                        TempData["Message"] = "Crédito insuficiente.";
                        return RedirectToAction("Create", new { id = oferta.Cliente.ID });
                    }

                    // Oferta realizada com sucesso Cliente.Status.ContabilizaVenda deve ser verdadeiro 
                    oferta.Cliente.Status = status.ContabilizaVenda != true ? _context.Status.First(s => s.ContabilizaVenda == true) : status;

                    // Subtrair o crédito do cliente e informar o EF que ouve modificação nesse objeto
                    oferta.Cliente.Credito = credito - totalProd;
                    _context.Entry(oferta.Cliente).State = EntityState.Modified;

                    // Adicionar a lista de produtos para a oferta
                    oferta.Produtos = new List<Produto>();
                    oferta.Produtos.AddRange(ListaProdutos);
                }
                else
                {
                    // Sem produto a venda não é contabilizada
                    oferta.Cliente.Status = status.ContabilizaVenda != false ? _context.Status.First(s => s.ContabilizaVenda == false) : status;
                }

                TempData["Message"] = "Oferta salva com sucesso!";
                _context.Add(oferta);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Clientes");
            }
            return View(oferta);
        }
    }
}
