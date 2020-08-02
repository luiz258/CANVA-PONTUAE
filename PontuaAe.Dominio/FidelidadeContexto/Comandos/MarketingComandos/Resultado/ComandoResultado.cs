using PontuaAe.Compartilhado.Comandos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PontuaAe.Dominio.FidelidadeContexto.Comandos.MarketingComandos.Resultado
{
    public class ComandoResultado : IComandoResultado
    {
        public ComandoResultado(bool sucesso)
        {

        }
        public ComandoResultado(bool sucesso, string mensage, object dado)
        {
            Sucesso = sucesso;
            Mensage = mensage;
            Dado = dado;

        }

        public bool Sucesso { get; set; }
        public string Mensage { get; set; }
        public object Dado { get; set; }
    }
}
