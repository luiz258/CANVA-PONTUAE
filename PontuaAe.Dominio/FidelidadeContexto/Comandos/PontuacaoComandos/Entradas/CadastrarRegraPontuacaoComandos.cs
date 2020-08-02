using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PontuaAe.Dominio.FidelidadeContexto.Comandos.PontuacaoComandos.Entradas
{
    public class CadastrarRegraPontuacaoComandos
    {
        public int IdUsuario { get; set; }
        public decimal PontosFidelidade { get; set; }
        public decimal ValorGasto { get; set; }
        public int ValidadePontos { get; set; }
    }
}
