using PontuaAe.Compartilhado.Comandos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PontuaAe.Dominio.FidelidadeContexto.Comandos.PontuacaoComandos.Entradas
{
    public class AddRegraPontuacaoComando : IComando
    {
        public int IdUsuario { get; set; }
        public decimal Reais { get; set; }
        public decimal PontosFidelidade { get; set; }
        public int ValidadePontos { get; set; }

        public bool Valida()
        {
            throw new NotImplementedException();
        }
    }
}
