﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Repository;

namespace Repository.Migrations
{
    [DbContext(typeof(NotaCompraContext))]
    partial class NotaCompraContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity("Domain.ConfiguracaoFaixaVistosAprovacoes", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Aprovacoes");

                    b.Property<double>("FaixaMax");

                    b.Property<double>("FaixaMin");

                    b.Property<int>("Vistos");

                    b.HasKey("Id");

                    b.ToTable("ConfFaixaVistAprov");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Aprovacoes = 0,
                            FaixaMax = 1000.0,
                            FaixaMin = 0.0,
                            Vistos = 1
                        },
                        new
                        {
                            Id = 2,
                            Aprovacoes = 1,
                            FaixaMax = 10000.0,
                            FaixaMin = 1000.01,
                            Vistos = 1
                        },
                        new
                        {
                            Id = 3,
                            Aprovacoes = 1,
                            FaixaMax = 50000.0,
                            FaixaMin = 10000.01,
                            Vistos = 2
                        },
                        new
                        {
                            Id = 4,
                            Aprovacoes = 2,
                            FaixaMax = 999999.98999999999,
                            FaixaMin = 50000.010000000002,
                            Vistos = 2
                        });
                });

            modelBuilder.Entity("Domain.HistoricoAprovacaoNotaCompra", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Data");

                    b.Property<int>("NotaCompraId");

                    b.Property<int>("Operacao");

                    b.Property<int>("UsuarioId");

                    b.HasKey("Id");

                    b.HasIndex("NotaCompraId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("HistAprovNotaCompra");
                });

            modelBuilder.Entity("Domain.NotaCompra", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DataEmissao");

                    b.Property<int>("Status");

                    b.Property<double>("ValorDesconto");

                    b.Property<double>("ValorFrete");

                    b.Property<double>("ValorMercadorias");

                    b.Property<double>("ValorTotal");

                    b.HasKey("Id");

                    b.ToTable("NotasCompra");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DataEmissao = new DateTime(2019, 12, 1, 8, 30, 0, 0, DateTimeKind.Unspecified),
                            Status = 0,
                            ValorDesconto = 10.0,
                            ValorFrete = 15.0,
                            ValorMercadorias = 100.09999999999999,
                            ValorTotal = 105.09999999999999
                        },
                        new
                        {
                            Id = 2,
                            DataEmissao = new DateTime(2019, 12, 2, 18, 30, 0, 0, DateTimeKind.Unspecified),
                            Status = 0,
                            ValorDesconto = 10.0,
                            ValorFrete = 15.0,
                            ValorMercadorias = 1500.0999999999999,
                            ValorTotal = 1505.0999999999999
                        },
                        new
                        {
                            Id = 3,
                            DataEmissao = new DateTime(2019, 12, 20, 8, 30, 0, 0, DateTimeKind.Unspecified),
                            Status = 0,
                            ValorDesconto = 10.0,
                            ValorFrete = 15.0,
                            ValorMercadorias = 10500.1,
                            ValorTotal = 10505.1
                        },
                        new
                        {
                            Id = 4,
                            DataEmissao = new DateTime(2019, 12, 25, 18, 30, 0, 0, DateTimeKind.Unspecified),
                            Status = 0,
                            ValorDesconto = 10.0,
                            ValorFrete = 15.0,
                            ValorMercadorias = 200000.0,
                            ValorTotal = 200005.0
                        });
                });

            modelBuilder.Entity("Domain.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Login");

                    b.Property<int>("Papel");

                    b.Property<string>("Senha");

                    b.Property<double>("ValorMaximo");

                    b.Property<double>("ValorMinimo");

                    b.HasKey("Id");

                    b.ToTable("Usuario");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Login = "gerente",
                            Papel = 1,
                            Senha = "gerente",
                            ValorMaximo = 999999.98999999999,
                            ValorMinimo = 50000.010000000002
                        },
                        new
                        {
                            Id = 2,
                            Login = "subgerente",
                            Papel = 1,
                            Senha = "subgerente",
                            ValorMaximo = 999999.98999999999,
                            ValorMinimo = 1000.01
                        },
                        new
                        {
                            Id = 3,
                            Login = "vendedor",
                            Papel = 0,
                            Senha = "vendedor",
                            ValorMaximo = 999999.98999999999,
                            ValorMinimo = 10000.01
                        },
                        new
                        {
                            Id = 4,
                            Login = "auxiliar",
                            Papel = 0,
                            Senha = "auxiliar",
                            ValorMaximo = 999999.98999999999,
                            ValorMinimo = 0.0
                        });
                });

            modelBuilder.Entity("Domain.HistoricoAprovacaoNotaCompra", b =>
                {
                    b.HasOne("Domain.NotaCompra", "NotaCompra")
                        .WithMany("HistAprovNotasCompra")
                        .HasForeignKey("NotaCompraId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Domain.Usuario", "Usuario")
                        .WithMany()
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
