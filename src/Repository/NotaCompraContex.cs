using System;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Repository {
    public class NotaCompraContext : DbContext {
        public NotaCompraContext(DbContextOptions options) : base(options){}

        public DbSet<NotaCompra> NotasCompra {get; set;}
        public DbSet<Usuario> Usuario {get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<NotaCompra>(notaCompra => 
            {
                notaCompra.HasKey(nf => nf.Id);
            });
            
            modelBuilder.Entity<Usuario>(usuario => 
            {
                usuario.HasKey(u => u.Id);
            });

            Seed(modelBuilder);
        }
    
        protected void Seed(ModelBuilder modelBuilder) {
            modelBuilder.Entity<NotaCompra>(notaCompra => 
            {
                notaCompra.HasData(
                    new NotaCompra { 
                        Id = 1, 
                        DataEmissao = new DateTime(2019,12,1,8,30,0),
                        ValorMercadorias = 100.10,
                        ValorDesconto = 10,
                        ValorFrete = 15,
                        ValorTotal = 105.10,
                        Status = Status.Pendente
                    },
                    new NotaCompra { 
                        Id = 2, 
                        DataEmissao = new DateTime(2019,12,2,18,30,0),
                        ValorMercadorias = 1500.10,
                        ValorDesconto = 10,
                        ValorFrete = 15,
                        ValorTotal = 1505.10,
                        Status = Status.Pendente
                    },
                    new NotaCompra { 
                        Id = 3, 
                        DataEmissao = new DateTime(2019,12,20,8,30,0),
                        ValorMercadorias = 10500.10,
                        ValorDesconto = 10,
                        ValorFrete = 15,
                        ValorTotal = 10505.10,
                        Status = Status.Pendente
                    },
                    new NotaCompra { 
                        Id = 4, 
                        DataEmissao = new DateTime(2019,12,25,18,30,0),
                        ValorMercadorias = 200000,
                        ValorDesconto = 10,
                        ValorFrete = 15,
                        ValorTotal = 200005,
                        Status = Status.Pendente
                    }

                );
            });
            
            modelBuilder.Entity<Usuario>(usuario => 
            {
                usuario.HasData(
                    new Usuario { Id = 1, Login = "gerente", Senha = "gerente", Papel = Papel.Aprovacao, ValorMinimo = 50000.01, ValorMaximo = 999999.99 },
                    new Usuario { Id = 2, Login = "subgerente", Senha = "subgerente", Papel = Papel.Aprovacao, ValorMinimo = 1000.01, ValorMaximo = 999999.99 },
                    new Usuario { Id = 3, Login = "vendedor", Senha = "vendedor", Papel = Papel.Visto, ValorMinimo = 10000.01, ValorMaximo = 999999.99 },
                    new Usuario { Id = 4,  Login = "auxiliar", Senha = "auxiliar", Papel = Papel.Visto, ValorMinimo = 0, ValorMaximo = 999999.99}
                );
            });
        }
    
    }
}