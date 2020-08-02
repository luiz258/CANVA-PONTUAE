using FluentValidator;
using FluentValidator.Validation;
using System;
using System.Collections.Generic;
using System.Text;

namespace PontuaAe.Dominio.FidelidadeContexto.ObjetoValor
{
    public class Descricao:Notifiable
    {
        public Descricao(string texto)
        {
            Texto = texto;

            AddNotifications(new ValidationContract()
               .Requires()
               .HasMaxLen(Texto, 250,"Texto", "A descricao deve conter no máximo 250 caracteres")
               .HasMinLen(Texto, 10,"Texto", "A descricao deve conter no minimo 10 caracteres")
               );

        }
        public string Texto { get; private set; }

        public override string ToString()
        {
            return Texto; 
        }
    }
}
