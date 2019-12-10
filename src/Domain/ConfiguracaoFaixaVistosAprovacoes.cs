using System;

namespace Domain {
    public class ConfiguracaoFaixaVistosAprovacoes : DomainBase {
        public double FaixaMin {get; set;}
        public double FaixaMax {get; set;}
        public int Vistos {get;set;}
        public int Aprovacoes {get;set;}

    }
}