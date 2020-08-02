using FluentValidator;
using FluentValidator.Validation;


namespace PontuaAe.Dominio.FidelidadeContexto.ObjetoValor
{
    public class Email : Notifiable
    {

        public Email(string endereco)
        {
            Endereco = endereco;

            AddNotifications(new ValidationContract().Requires().IsEmail(Endereco, "Email", "O E-mail é inválido"));                
            
        }
        public string Endereco { get; set; }

        public override string ToString()
        {
            return Endereco;
        }
    }
}
