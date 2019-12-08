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

        public async Task<NotaCompra[]> GetNotasComprasAsyncByFilterDate(DateTime dataInicio, DateTime dataFim, Usuario usuario)
        {
            IQueryable<NotaCompra> query = _context.NotasCompra;
            return await query.ToArrayAsync();
        }

        public async Task<bool> RegistraVistoAprovacaoAsyncById(int idNotaCompra)
        {
            throw new NotImplementedException();
        }
    }
}