using System;

namespace Domain
{
    public enum Status { Pendente, Aprovada }
    public class NotaCompra
    {
        public int Id {get;set;}
        public DateTime DataEmissao {get;set;}
        public double ValorMercadorias {get;set;}
        public double ValorDesconto {get;set;}
        public double ValorFrete {get;set;}
        public double ValorTotal {get;set;}
        public Status Status {get;set;}
        
    }
}