using FluentValidator;
using FluentValidator.Validation;
using PontuaAe.Compartilhado.Comandos;
using System;

namespace PontuaAe.Dominio.FidelidadeContexto.Comandos.FuncionarioComandos.Entradas
{
    public class EditarFuncionarioComando : Notifiable, IComando
    {
        public int Id { get; set; }
        public int IdEmpresa { get; set; }
        public int ControleUsuario { get; set; }
        public string NomeCompleto { get; set; }
        public string Email { get; set; }
        public string Contato { get; set; }


        public bool Valida()
        {
            AddNotifications(new ValidationContract()
              .IsEmail(Email, "Email", "O E-mail é inválido")

          );
            return IsValid;
        }
    }
}

