using FluentValidator;
using FluentValidator.Validation;
using PontuaAe.Compartilhado.Comandos;
using System;

namespace PontuaAe.Dominio.FidelidadeContexto.Comandos.FuncionarioComandos.Entradas
{
    public class EditarFuncionarioComando : Notifiable, IComando
    {
        public int IdUsuario { get; set; }
        public int IdEmpresa { get; set; }
        public string NomeCompleto { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string Contato { get; set; }


        public bool Valida()
        {
            AddNotifications(new ValidationContract()
              .HasMinLen(NomeCompleto, 3, "Nome", "O nome deve conter pelo menos 3 caracteres")
              .HasMaxLen(NomeCompleto, 40, "Nome Completo", "O nome deve conter no máximo 40 caracteres")
              .IsEmail(Email, "Email", "O E-mail é inválido")

          );
            return IsValid;
        }
    }
}

