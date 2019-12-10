using System;

namespace Domain
{
    public enum Operacao { Visto, Aprovacao }
    public class HistoricoAprovacaoNotaCompra : DomainBase
    {
        public DateTime Data {get;set;}
        public  int UsuarioId {get;set;}
        public Usuario Usuario {get;set;}
        public Operacao Operacao {get;set;}
        public int NotaCompraId {get;set;}
        public NotaCompra NotaCompra { get; set;}
    }
}