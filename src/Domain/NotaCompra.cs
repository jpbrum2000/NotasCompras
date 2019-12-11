using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain
{
    public enum Status { Pendente, Aprovada }
    public class NotaCompra : DomainBase
    {
        public DateTime DataEmissao {get;set;}
        public double ValorMercadorias {get;set;}
        public double ValorDesconto {get;set;}
        public double ValorFrete {get;set;}
        public double ValorTotal {get;set;}
        public Status Status {get;set;}
        public ICollection<HistoricoAprovacaoNotaCompra> HistAprovNotasCompra {get;set;}

        public bool PrecisaVisto(int numVistoConf) { 
                int numVisto = HistAprovNotasCompra.Where(h => h.Operacao == Operacao.Visto).Count();
                return numVisto < numVistoConf;
        }
        public bool PrecisaAprovacao(int numVistoConf, int numAprovConf) { 
            if(!PrecisaVisto(numVistoConf)){
                int numAprov = HistAprovNotasCompra.Where(h => h.Operacao == Operacao.Aprovacao).Count();
                return numAprov < numAprovConf;
            }
            return false;
        }

        public bool PodeAprovar(int numVistoConf, int numAprovConf){
           return (!PrecisaVisto(numVistoConf) && !PrecisaAprovacao(numVistoConf,numAprovConf));
        }
    }
}