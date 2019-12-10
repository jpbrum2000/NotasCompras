using System;

namespace WebApi.ViewModel {
    public enum Status { Pendente, Aprovada }
    public class NotaCompraViewModel {
        public DateTime DataEmissao {get;set;}
        public double ValorMercadorias {get;set;}
        public double ValorDesconto {get;set;}
        public double ValorFrete {get;set;}
        public double ValorTotal {get;set;}
        public Status Status {get;set;}
    }
}