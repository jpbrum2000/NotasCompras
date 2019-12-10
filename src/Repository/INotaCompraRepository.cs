using System;
using System.Threading.Tasks;
using Domain;

namespace Repository {
    public interface INotaCompraRepository {
        Task<NotaCompra[]> GetNotasComprasAsyncByFilterDate(DateTime dataInicio, DateTime dataFim, Usuario usuario);
        Task<bool> RegistraVistoAprovacaoAsyncById(int idNotaCompra, Usuario user);
    }
}