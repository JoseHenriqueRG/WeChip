﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WeChip.Data;

namespace WeChip.Migrations
{
    [DbContext(typeof(WeChipDataBaseContext))]
    [Migration("20211214201119_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.12");

            modelBuilder.Entity("WeChip.Models.Cliente", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Cpf")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Credito")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int?>("StatusCodigoStatus")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Telefone")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.HasIndex("StatusCodigoStatus");

                    b.ToTable("Cliente");
                });

            modelBuilder.Entity("WeChip.Models.Oferta", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Bairro")
                        .HasColumnType("TEXT");

                    b.Property<string>("Cep")
                        .HasColumnType("TEXT");

                    b.Property<string>("Cidade")
                        .HasColumnType("TEXT");

                    b.Property<int?>("ClienteID")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Complemento")
                        .HasColumnType("TEXT");

                    b.Property<string>("Estado")
                        .HasColumnType("TEXT");

                    b.Property<int?>("Numero")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Rua")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("ValorTotal")
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.HasIndex("ClienteID");

                    b.ToTable("Oferta");
                });

            modelBuilder.Entity("WeChip.Models.OfertaProdutos", b =>
                {
                    b.Property<int>("OfertaID")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ProdutoID")
                        .HasColumnType("INTEGER");

                    b.HasKey("OfertaID", "ProdutoID");

                    b.HasIndex("ProdutoID");

                    b.ToTable("OfertaProdutos");
                });

            modelBuilder.Entity("WeChip.Models.Produto", b =>
                {
                    b.Property<int>("CodigoProduto")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Descricao")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Preco")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Tipo")
                        .HasColumnType("INTEGER");

                    b.HasKey("CodigoProduto");

                    b.ToTable("Produto");
                });

            modelBuilder.Entity("WeChip.Models.Status", b =>
                {
                    b.Property<int>("CodigoStatus")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("ContabilizaVenda")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Descricao")
                        .HasColumnType("TEXT");

                    b.Property<bool>("FinalizaCliente")
                        .HasColumnType("INTEGER");

                    b.HasKey("CodigoStatus");

                    b.ToTable("Status");
                });

            modelBuilder.Entity("WeChip.Models.Usuario", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Login")
                        .HasColumnType("TEXT");

                    b.Property<string>("Senha")
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.ToTable("Usuario");
                });

            modelBuilder.Entity("WeChip.Models.Cliente", b =>
                {
                    b.HasOne("WeChip.Models.Status", "Status")
                        .WithMany()
                        .HasForeignKey("StatusCodigoStatus");

                    b.Navigation("Status");
                });

            modelBuilder.Entity("WeChip.Models.Oferta", b =>
                {
                    b.HasOne("WeChip.Models.Cliente", "Cliente")
                        .WithMany()
                        .HasForeignKey("ClienteID");

                    b.Navigation("Cliente");
                });

            modelBuilder.Entity("WeChip.Models.OfertaProdutos", b =>
                {
                    b.HasOne("WeChip.Models.Oferta", "Oferta")
                        .WithMany("OfertaProdutos")
                        .HasForeignKey("OfertaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WeChip.Models.Produto", "Produto")
                        .WithMany("OfertaProdutos")
                        .HasForeignKey("ProdutoID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Oferta");

                    b.Navigation("Produto");
                });

            modelBuilder.Entity("WeChip.Models.Oferta", b =>
                {
                    b.Navigation("OfertaProdutos");
                });

            modelBuilder.Entity("WeChip.Models.Produto", b =>
                {
                    b.Navigation("OfertaProdutos");
                });
#pragma warning restore 612, 618
        }
    }
}