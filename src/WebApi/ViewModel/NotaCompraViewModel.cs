using System;

namespace WebApi.ViewModel {
    public enum Status { Pendente, Aprovada }
    public class NotaCompraViewModel {
        public int id {get;set;}
        public DateTime dataEmissao {get;set;}
        public double valorMercadorias {get;set;}
        public double valorDesconto {get;set;}
        public double valorFrete {get;set;}
        public double valorTotal {get;set;}
        public Status status {get;set;}
    }
}