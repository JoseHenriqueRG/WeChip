﻿using Microsoft.EntityFrameworkCore;
using WeChip.Models;

namespace WeChip.Data
{
    public class WeChipDataBaseContext : DbContext
    {
        public WeChipDataBaseContext (DbContextOptions<WeChipDataBaseContext> options)
            : base(options)
        {
        }

        public DbSet<WeChip.Models.Cliente> Cliente { get; set; }

        public DbSet<WeChip.Models.Oferta> Oferta { get; set; }

        public DbSet<WeChip.Models.Produto> Produto { get; set; }
        
        public DbSet<WeChip.Models.Status> Status { get; set; }

        public DbSet<WeChip.Models.Usuario> Usuario { get; set; }

        public DbSet<WeChip.Models.OfertaProdutos> OfertaProdutos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OfertaProdutos>()
                .HasKey(op => new { op.OfertaID, op.ProdutoID });
        }
    }
}
