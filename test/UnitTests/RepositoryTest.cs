using System;
using System.Linq;
using Domain;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Repository;
using Xunit;

namespace UnitTests
{
    public class RepositoryTest
    {
        [Fact]
        public async void TestRegistraVistoAprovacaoAsyncById()
        {
            // In-memory database only exists while the connection is open
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            var options = new DbContextOptionsBuilder<NotaCompraContext>()
                .UseSqlite(connection)
                .Options;

            using (var context = new NotaCompraContext(options))
            {
                // Create the schema in the database
                context.Database.EnsureCreated();

                INotaCompraRepository nfCompraRepository = new NotaCompraRepository(context);
                //Testa Visto e aprovacao
                Usuario userAuxiliar = context.Usuario.Find(4);
                Usuario userSubGerente = context.Usuario.Find(2);
                await nfCompraRepository.RegistraVistoAprovacaoAsyncById(1, userAuxiliar.Id);
                await nfCompraRepository.RegistraVistoAprovacaoAsyncById(2, userAuxiliar.Id);
                await nfCompraRepository.RegistraVistoAprovacaoAsyncById(2, userSubGerente.Id);

                NotaCompra nfFaixa1 = context.NotasCompra.AsNoTracking().Include(n => n.HistAprovNotasCompra).Single(n => n.Id == 1);
                NotaCompra nfFaixa2 = context.NotasCompra.AsNoTracking().Include(n => n.HistAprovNotasCompra).Single(n => n.Id == 2);

                int numVistosNfFaixa1 = nfFaixa1.HistAprovNotasCompra.Where(h => h.Operacao == Operacao.Visto).Count();
                int numVistosNfFaixa2 = nfFaixa2.HistAprovNotasCompra.Where(h => h.Operacao == Operacao.Visto).Count();
                int numAprovaNfFaixa2 = nfFaixa2.HistAprovNotasCompra.Where(h => h.Operacao == Operacao.Aprovacao).Count();
                Assert.True(numVistosNfFaixa1 == 1);
                Assert.True(numVistosNfFaixa2 == 1);
                Assert.True(numAprovaNfFaixa2 == 1);

                //Testa Unico Visto ou aprovacao Para mesma nota
                await nfCompraRepository.RegistraVistoAprovacaoAsyncById(1, userAuxiliar.Id);
                await nfCompraRepository.RegistraVistoAprovacaoAsyncById(2, userAuxiliar.Id);
                await nfCompraRepository.RegistraVistoAprovacaoAsyncById(2, userSubGerente.Id);

                numVistosNfFaixa1 = nfFaixa1.HistAprovNotasCompra.Where(h => h.Operacao == Operacao.Visto).Count();
                numVistosNfFaixa2 = nfFaixa2.HistAprovNotasCompra.Where(h => h.Operacao == Operacao.Visto).Count();
                numAprovaNfFaixa2 = nfFaixa2.HistAprovNotasCompra.Where(h => h.Operacao == Operacao.Aprovacao).Count();
                Assert.True(numVistosNfFaixa1 == 1);
                Assert.True(numVistosNfFaixa2 == 1);
                Assert.True(numAprovaNfFaixa2 == 1);

                //Testa nao Aprovar se quantidade de vistos nao for suficiente
                await nfCompraRepository.RegistraVistoAprovacaoAsyncById(3, userSubGerente.Id);
                NotaCompra nfFaixa3 = context.NotasCompra.Include(n => n.HistAprovNotasCompra).Single(n => n.Id == 3);
                int numAprovaNfFaixa3 = nfFaixa1.HistAprovNotasCompra.Where(h => h.Operacao == Operacao.Aprovacao).Count();
                Assert.True(numAprovaNfFaixa3 == 0);

                //Testa Nota Status Aprovada quando atinge numero de vistos e aprovações suficientes
                Assert.True(nfFaixa1.Status == Status.Aprovada);
                Assert.True(nfFaixa2.Status == Status.Aprovada);
                Assert.True(nfFaixa3.Status == Status.Pendente);
            }


        }

        [Fact]
        public async void TestGetNotasComprasAsyncByFilterDate()
        {
            // In-memory database only exists while the connection is open
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            var options = new DbContextOptionsBuilder<NotaCompraContext>()
                .UseSqlite(connection)
                .Options;

            using (var context = new NotaCompraContext(options))
            {
                // Create the schema in the database
                context.Database.EnsureCreated();
                
                INotaCompraRepository nfCompraRepository = new NotaCompraRepository(context);
                
                // Testa Filtro por data inicial e final
                DateTime dtInicio = new DateTime(2019,12,01);
                DateTime dtFim = new DateTime(2019,12,31);
                Usuario userAuxiliar = context.Usuario.Find(4);
                NotaCompra[] nfComprasAuxiliar = await nfCompraRepository.GetNotasComprasAsyncByFilterDate(dtInicio, dtFim, userAuxiliar.Id);
                Assert.True(nfComprasAuxiliar.Count() == 4);
                
                // Notas Estao no Limite do Papel do Usuario 
                Usuario userVendedor = context.Usuario.Find(3);
                NotaCompra[] nfComprasVendedor = await nfCompraRepository.GetNotasComprasAsyncByFilterDate(dtInicio, dtFim, userVendedor.Id);
                Assert.True(nfComprasVendedor.Count() == 2);

                // Estao no Status do Usuario
                await nfCompraRepository.RegistraVistoAprovacaoAsyncById(2, userAuxiliar.Id);
                Usuario userSubGerente = context.Usuario.Find(2);
                NotaCompra[] nfComprasSubGerente = await nfCompraRepository.GetNotasComprasAsyncByFilterDate(dtInicio, dtFim, userSubGerente.Id);
                Assert.True(nfComprasSubGerente.Count() == 1);

                // Usuario Não Registrou Visto ou Aprovacao para a Nota
                await nfCompraRepository.RegistraVistoAprovacaoAsyncById(4, userAuxiliar.Id);
                nfComprasAuxiliar = await nfCompraRepository.GetNotasComprasAsyncByFilterDate(dtInicio, dtFim, userAuxiliar.Id);
                NotaCompra nfFaixa4 = context.NotasCompra.AsNoTracking().Include(n => n.HistAprovNotasCompra).Single(n => n.Id == 4);
                Assert.True(nfFaixa4.Status == Status.Pendente);
                Assert.True(nfComprasAuxiliar.Count() == 2);

            }

        }

        [Fact]
        public async void TestAutenticaUsuario(){
            // In-memory database only exists while the connection is open
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            var options = new DbContextOptionsBuilder<NotaCompraContext>()
                .UseSqlite(connection)
                .Options;

            using (var context = new NotaCompraContext(options))
            {
                // Create the schema in the database
                context.Database.EnsureCreated();
                
                INotaCompraRepository nfCompraRepository = new NotaCompraRepository(context);

                //Usuario Valido
                Assert.True(await nfCompraRepository.autenticaUsuario("gerente","gerente") != null);
                //Usuario Invalido
                Assert.True(await nfCompraRepository.autenticaUsuario("gerente","gerente1") == null);

            }
        }

    }
}
