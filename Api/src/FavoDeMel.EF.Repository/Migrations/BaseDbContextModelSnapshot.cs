﻿// <auto-generated />
using System;
using FavoDeMel.EF.Repository.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FavoDeMel.EF.Repository.Migrations
{
    [DbContext(typeof(BaseDbContext))]
    partial class BaseDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("FavoDeMel.Domain.Comandas.Comanda", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("GarcomId")
                        .HasColumnType("int");

                    b.Property<decimal>("GorjetaGarcom")
                        .HasColumnType("decimal(65,30)");

                    b.Property<int>("Situacao")
                        .HasColumnType("int");

                    b.Property<decimal>("TotalAPagar")
                        .HasColumnType("decimal(65,30)");

                    b.HasKey("Id");

                    b.HasIndex("GarcomId");

                    b.HasIndex("Situacao");

                    b.ToTable("Comanda");
                });

            modelBuilder.Entity("FavoDeMel.Domain.Comandas.ComandaPedido", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("ComandaId")
                        .HasColumnType("int");

                    b.Property<int?>("ProdutoId")
                        .HasColumnType("int");

                    b.Property<int>("Quantidade")
                        .HasColumnType("int");

                    b.Property<int>("Situacao")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ComandaId");

                    b.HasIndex("ProdutoId");

                    b.ToTable("ComandaPedido");
                });

            modelBuilder.Entity("FavoDeMel.Domain.Produtos.Produto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<decimal>("Preco")
                        .HasColumnType("decimal(65,30)");

                    b.Property<string>("UlrImage")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.HasIndex("Nome");

                    b.ToTable("Produto");
                });

            modelBuilder.Entity("FavoDeMel.Domain.Usuarios.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<bool>("Ativo")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Login")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("Nome")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Password")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("Perfil")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Ativo");

                    b.HasIndex("Login");

                    b.HasIndex("Perfil");

                    b.ToTable("Usuario");
                });

            modelBuilder.Entity("FavoDeMel.Domain.Comandas.Comanda", b =>
                {
                    b.HasOne("FavoDeMel.Domain.Usuarios.Usuario", "Garcom")
                        .WithMany()
                        .HasForeignKey("GarcomId");
                });

            modelBuilder.Entity("FavoDeMel.Domain.Comandas.ComandaPedido", b =>
                {
                    b.HasOne("FavoDeMel.Domain.Comandas.Comanda", "Comanda")
                        .WithMany("Pedidos")
                        .HasForeignKey("ComandaId");

                    b.HasOne("FavoDeMel.Domain.Produtos.Produto", "Produto")
                        .WithMany()
                        .HasForeignKey("ProdutoId");
                });
#pragma warning restore 612, 618
        }
    }
}
