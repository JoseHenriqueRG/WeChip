using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeChip.Data;

namespace WeChip.Models
{
    public static class PersistirDados
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new WeChipDataBaseContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<WeChipDataBaseContext>>()))
            {
                if (context.Status.Any())
                {
                    return;
                }

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
        }
    }
}