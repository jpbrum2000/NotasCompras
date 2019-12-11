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
            .Where(nf => nf.Status != Status.Aprovada)
            .Where(nf => dataInicio <= nf.DataEmissao && nf.DataEmissao <= dataFim)
            .Where(nf => usuario.ValorMinimo <= nf.ValorTotal && nf.ValorTotal <= usuario.ValorMaximo)
            .Where(nf => nf.HistAprovNotasCompra.Where(h => h.UsuarioId == usuario.Id).Count() == 0);
            NotaCompra[] NFresult = await query.ToArrayAsync();

            if (usuario.Papel == Papel.Visto ) {
                return NFresult.Where(nf => nf.PrecisaVisto(GetConfNumVistoByValorNF(nf.ValorTotal).Result)).ToArray();
            } 
            else {
                return NFresult.Where(nf => nf.PrecisaAprovacao(GetConfNumVistoByValorNF(nf.ValorTotal).Result, GetConfNumAprovacoesByValorNF(nf.ValorTotal).Result)).ToArray();
            }
        }

        public async Task<bool> RegistraVistoAprovacaoAsyncById(int idNotaCompra, int usuarioId)
        {
            Usuario usuario = _context.Usuario.Find(usuarioId);
            NotaCompra nf = await _context.NotasCompra.Include(n => n.HistAprovNotasCompra).SingleAsync(n => n.Id == idNotaCompra);
            ConfiguracaoFaixaVistosAprovacoes ConfFaixaVistAprov = await _context.ConfFaixaVistAprov.Where(conf => conf.FaixaMin <= nf.ValorTotal && nf.ValorTotal <= conf.FaixaMax).SingleAsync();

            //Verifica se Usuario já nao aprovou/Vistoriou Nota
            bool usuarioJaRegistrou = (nf.HistAprovNotasCompra.Where(h => h.Id == idNotaCompra && h.Usuario == usuario ).Count() != 0);
            bool podeRegistrarNF = (usuario.Papel == Papel.Visto && nf.PrecisaVisto(ConfFaixaVistAprov.Vistos)) || (usuario.Papel == Papel.Aprovacao && nf.PrecisaAprovacao(ConfFaixaVistAprov.Vistos, ConfFaixaVistAprov.Aprovacoes));
            //Registra VistoAprov
            if (!usuarioJaRegistrou && podeRegistrarNF) {
                HistoricoAprovacaoNotaCompra HistAprovNF = new HistoricoAprovacaoNotaCompra {
                    Data = DateTime.Now,
                    UsuarioId = usuario.Id,
                    Operacao = usuario.Papel == Papel.Visto ? Operacao.Visto : Operacao.Aprovacao,
                    NotaCompraId = nf.Id
                };
                _context.Add(HistAprovNF);
                await _context.SaveChangesAsync();
                // Aprova da Nota
                if (nf.PodeAprovar(ConfFaixaVistAprov.Vistos,ConfFaixaVistAprov.Aprovacoes)){
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