using FluentValidator;
using FluentValidator.Validation;
using PontuaAe.Compartilhado.Comandos;

namespace PontuaAe.Dominio.FidelidadeContexto.Comandos.EmpresaComandos.Entradas
{
   public class EditarPerfilEmpresaComando : Notifiable, IComando
    {
        public int Id { get; set; } //IdUsuario
        public int IdEmpresa { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string Horario { get; set; }
        public string NomeFantasia { get; set; }
        public string NomeResponsavel { get; set; }
        public string Descricao { get; set; }
        public string Telefone { get; set; }
        public string Seguimento { get; set; }
        public string Facebook { get; set; }
        public string Instagram { get; set; }
        public string Delivery { get; set; }
        public string Logo { get; set; }
        public string Bairro { get; set; }
        public string Rua { get; set; }
        public string Numero { get; set; }
        public string Cep { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string Complemento { get; set; }


        public bool Valida()
        {
            AddNotifications(new ValidationContract()

               .IsEmail(Email, "Email", "O E-mail é inválido")
           ); ;
            return IsValid;
        }
    }
}
