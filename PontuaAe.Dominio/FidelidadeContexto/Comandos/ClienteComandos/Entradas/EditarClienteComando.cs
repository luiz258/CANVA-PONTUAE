using FluentValidator;
using FluentValidator.Validation;
using PontuaAe.Compartilhado.Comandos;
using System;

namespace PontuaAe.Dominio.FidelidadeContexto.Comandos.ClienteComandos.Entradas
{
    public class EditarClienteComando : Notifiable, IComando
    {
        public int IdUsuario { get; set; }
        public string Nome { get; set; }
        public DateTime? DataNascimento { get; set; }
        public string Cidade { get; set; }
        public string Contato { get; set; }


        public bool Valida()
        {
            AddNotifications(new ValidationContract()
                .HasMinLen(Nome, 3, "FirstName", "O nome deve conter pelo menos 3 caracteres")
          
               
            );
            return IsValid;
        }
    }
}
