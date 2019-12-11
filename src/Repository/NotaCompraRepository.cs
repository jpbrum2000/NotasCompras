using System;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Repository {
    public class NotaCompraRepository : INotaCompraRepository
    {
        private readonly NotaCompraContext _context;
        public NotaCompraRepository(NotaCompraContext context){
            _context = context;
        }

        public async Task<NotaCompra[]> GetNotasComprasAsyncByFilterDate(DateTime dataInicio, DateTime dataFim, int usuarioId)
        {
            Usuario usuario = _context.Usuario.Find(usuarioId);

            IQueryable<NotaCompra> query = _context.NotasCompra.Include(n => n.HistAprovNotasCompra)
            .Where(nf => dataInicio <= nf.DataEmissao && nf.DataEmissao <= dataFim)
            .Where(nf => usuario.ValorMinimo <= nf.ValorTotal && nf.ValorTotal <= usuario.ValorMaximo)
            .Where(nf => nf.HistAprovNotasCompra.Where(h => h.UsuarioId == usuario.Id).Count() == 0);
            NotaCompra[] NFresult = await query.ToArrayAsync();
            
            return NFresult.Where(nf => usuario.Papel == Papel.Visto ? nf.PrecisaVisto(GetConfNumVistoByValorNF(nf.ValorTotal).Result) : !nf.PrecisaVisto(GetConfNumVistoByValorNF(nf.ValorTotal).Result) && nf.PrecisaAprovacao(GetConfNumAprovacoesByValorNF(nf.ValorTotal).Result)).ToArray();
        }

        public async Task<bool> RegistraVistoAprovacaoAsyncById(int idNotaCompra, int usuarioId)
        {
            Usuario usuario = _context.Usuario.Find(usuarioId);
            NotaCompra nf = await _context.NotasCompra.Include(n => n.HistAprovNotasCompra).SingleAsync(n => n.Id == idNotaCompra);
            ConfiguracaoFaixaVistosAprovacoes ConfFaixaVistAprov = await _context.ConfFaixaVistAprov.Where(conf => conf.FaixaMin <= nf.ValorTotal && nf.ValorTotal <= conf.FaixaMax).SingleAsync();
                    
            if ( nf.HistAprovNotasCompra.Where(h => h.Id == idNotaCompra && h.Usuario == usuario ).Count() == 0 && nf.Status != Status.Aprovada) {
                int numVistos = nf.HistAprovNotasCompra.Where(h => h.Operacao == Operacao.Visto).Count();
                int numAprovacoes = nf.HistAprovNotasCompra.Where(h => h.Operacao == Operacao.Aprovacao).Count();
                
                if (usuario.Papel == Papel.Visto && ConfFaixaVistAprov.Vistos > numVistos) {
                    _context.Add(new HistoricoAprovacaoNotaCompra {
                            Data = DateTime.Now,
                            UsuarioId = usuario.Id,
                            Operacao = Operacao.Visto,
                            NotaCompraId = nf.Id
                    });
                    await _context.SaveChangesAsync();
                }
                else if (usuario.Papel == Papel.Aprovacao && ConfFaixaVistAprov.Vistos == numVistos) {
                    
                    if (numVistos >= ConfFaixaVistAprov.Vistos) {
                        _context.Add(new HistoricoAprovacaoNotaCompra{
                            Data = DateTime.Now,
                            UsuarioId = usuario.Id,
                            Operacao = Operacao.Aprovacao,
                            NotaCompraId = nf.Id
                        });
                        await _context.SaveChangesAsync();
                    }
                }

                // Aprovacao da Nota
                numVistos = nf.HistAprovNotasCompra.Where(h => h.Operacao == Operacao.Visto).Count();
                numAprovacoes = nf.HistAprovNotasCompra.Where(h => h.Operacao == Operacao.Aprovacao).Count();
                if (ConfFaixaVistAprov.Vistos == numVistos && ConfFaixaVistAprov.Aprovacoes == numAprovacoes){
                    nf.Status = Status.Aprovada;
                    _context.Update(nf);
                    return await _context.SaveChangesAsync() > 0;
                }
                return true;
            }
            return false;
        }

        public async Task<Usuario> autenticaUsuario(string login, string senha) {
            return await _context.Usuario.Where(u => u.Login == login && u.Senha == senha).SingleOrDefaultAsync();
        }
    
        public async Task<int> GetConfNumVistoByValorNF(double valorNF){
           ConfiguracaoFaixaVistosAprovacoes conf = await _context.ConfFaixaVistAprov.Where(c => c.FaixaMin < valorNF && valorNF < c.FaixaMax).SingleAsync();
            return conf.Vistos;
        }
        public async Task<int> GetConfNumAprovacoesByValorNF(double valorNF){
            ConfiguracaoFaixaVistosAprovacoes conf = await _context.ConfFaixaVistAprov.Where(c => c.FaixaMin < valorNF && valorNF < c.FaixaMax).SingleAsync();
            return conf.Aprovacoes;
        }

    }
}