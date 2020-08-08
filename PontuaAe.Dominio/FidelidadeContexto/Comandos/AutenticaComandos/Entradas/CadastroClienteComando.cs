using FluentValidator;
using FluentValidator.Validation;
using PontuaAe.Compartilhado.Comandos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PontuaAe.Dominio.FidelidadeContexto.Comandos.ClienteComandos.Entradas
{
    public class CadastroClienteComando : Notifiable, IComando
    {
        public CadastroClienteComando()
        {
        }

        public string Nome { get; set; }
        public DateTime? DataNascimento { get; set; }
        public string Email { get; set; }
        public string Contato { get; set; }
        public string Senha { get; set; }
        public string Cidade { get; set; }
        public string RoleId => "Cliente";



        public bool Valida()
        {
           AddNotifications(new ValidationContract()
          .HasMinLen(Nome, 3, "Nome", "O nome deve conter pelo menos 3 caracteres")
          .HasMaxLen(Nome, 40, "Nome Completo", "O nome deve conter no máximo 40 caracteres")
          .IsEmail(Email, "Email", "O E-mail é inválido")

      );
            return IsValid;
        }
    }
}
