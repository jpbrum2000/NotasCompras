using System;
using System.Threading.Tasks;
using Domain;

namespace Repository {
    public interface INotaCompraRepository {
        Task<NotaCompra[]> GetNotasComprasAsyncByFilterDate(DateTime dataInicio, DateTime dataFim, int usuarioID);
        Task<bool> RegistraVistoAprovacaoAsyncById(int idNotaCompra, int usuarioId);

        Task<Usuario> autenticaUsuario(string login, string senha);
    }
}