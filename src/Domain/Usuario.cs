﻿namespace Domain
{
    public enum Papel { Visto, Aprovacao }
    public class Usuario : DomainBase
    {
        public string Login { get; set; }
        public string Senha { get; set; }
        public Papel Papel {get;set;}
        public double ValorMinimo { get; set; }
        public double ValorMaximo { get; set; }
    }
}
