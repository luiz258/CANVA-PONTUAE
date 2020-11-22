using FluentValidator;
using FluentValidator.Validation;
using PontuaAe.Compartilhado.Comandos;
using System;

namespace PontuaAe.Dominio.FidelidadeContexto.Comandos.ClienteComandos.Entradas
{
    public class EditarClienteComando : Notifiable, IComando
    {
        public int IdUsuario { get; set; }
        public string NomeCompleto { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Cidade { get; set; }
        public string Contato { get; set; }
        public string Sexo { get; set; }


        public bool Valida()
        {
            AddNotifications(new ValidationContract()
                .HasMinLen(NomeCompleto, 3, "FirstName", "O nome deve conter pelo menos 3 caracteres")
          
               
            );
            return IsValid;
        }
    }
}
