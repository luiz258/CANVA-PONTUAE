using FluentValidator;
using FluentValidator.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PontuaAe.Dominio.FidelidadeContexto.ObjetoValor
{
    public class ImagensResult: Notifiable
    {
        public string Nome { get; set; }
        public long Tamanho { get; set; }

        public ImagensResult(string nome, long tamanho)
        {
            Nome = nome;
            Tamanho = tamanho;

            AddNotifications(new ValidationContract()
                    .IsNotNull(Nome, "Nome", "nome não pode ser nulo"));
        }
    }
}
