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