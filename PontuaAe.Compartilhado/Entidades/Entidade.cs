using FluentValidator;
using System;


namespace PontuaAe.Compartilhado.Entidades
{
    public abstract class Entidade : Notifiable
    {        
        public int ID { get; private set; }
    }
}

  
