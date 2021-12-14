using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeChip.Data;
using WeChip.Models;

namespace WeChip.Data
{
    public static class PersistirDados
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new WeChipDataBaseContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<WeChipDataBaseContext>>()))
            {
                if (!context.Status.Any())
                {
                    context.Status.AddRange(
                    new Status
                    {
                        Descricao = "Nome Disponível",
                        FinalizaCliente = false,
                        ContabilizaVenda = false
                    },

                    new Status
                    {
                        Descricao = "Não deseja ser contatado",
                        FinalizaCliente = true,
                        ContabilizaVenda = false
                    },

                    new Status
                    {
                        Descricao = "Cliente Aceitou Oferta",
                        FinalizaCliente = true,
                        ContabilizaVenda = true
                    },

                    new Status
                    {
                        Descricao = "Caiu a ligação",
                        FinalizaCliente = false,
                        ContabilizaVenda = false
                    },

                    new Status
                    {
                        Descricao = "Viajou",
                        FinalizaCliente = false,
                        ContabilizaVenda = false
                    },

                    new Status
                    {
                        Descricao = "Falecido",
                        FinalizaCliente = true,
                        ContabilizaVenda = false
                    }
                );
                    context.SaveChanges();
                }

                if (!context.Usuario.Any())
                {
                    context.Usuario.Add(
                        new Usuario()
                        {
                            Login = "Teste",
                            Senha = "1234"
                        });
                    context.SaveChanges();
                }

                if (!context.Produto.Any())
                {
                    context.Produto.AddRange(
                        new Produto()
                        {
                            CodigoProduto = 15,
                            Descricao = "Mouse",
                            Preco = 20.00M,
                            Tipo = Tipo.Hardware
                        },
                        new Produto()
                        {
                            CodigoProduto = 106,
                            Descricao = "Teclado",
                            Preco = 30.00M,
                            Tipo = Tipo.Hardware
                        },
                        new Produto()
                        {
                            CodigoProduto = 200,
                            Descricao = "Monitor 17'",
                            Preco = 200.00M,
                            Tipo = Tipo.Hardware
                        },
                        new Produto()
                        {
                            CodigoProduto = 459,
                            Descricao = "Avast",
                            Preco = 199.99M,
                            Tipo = Tipo.Software
                        },
                        new Produto()
                        {
                            CodigoProduto = 1104,
                            Descricao = "Pacote Office",
                            Preco = 499.00M,
                            Tipo = Tipo.Software
                        });
                    context.SaveChanges();
                }
            }
        }
    }
}