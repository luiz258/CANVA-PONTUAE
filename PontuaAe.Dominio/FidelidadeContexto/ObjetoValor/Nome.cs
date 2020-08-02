using FluentValidator;
using FluentValidator.Validation;

namespace PontuaAe.Dominio.FidelidadeContexto.ObjetoValor

{
    public class Nome : Notifiable
    {
        public Nome(string primeiroNome)
        {
            PrimeiroNome = primeiroNome;
           // SobreNome = sobreNome;

            AddNotifications(new ValidationContract()
               .Requires()
               .HasMinLen(PrimeiroNome, 3, "PrimeiroNome", "O nome deve conter pelo menos 3 caracteres")
               .HasMaxLen(PrimeiroNome, 40, "PrimeiroNome", "O nome deve conter no máximo 40 caracteres")
               //.HasMinLen(SobreNome, 3, "SobreNome", "O sobrenome deve conter pelo menos 3 caracteres")
               //.HasMaxLen(SobreNome, 40, "SobreNome", "O sobrenome deve conter no máximo 40 caracteres")
           );

        }

        public string PrimeiroNome { get; private set; }
        public string SobreNome { get; private set; }

        public override string ToString()
        {
            return $"{PrimeiroNome} {SobreNome}";
        }
    }
}
