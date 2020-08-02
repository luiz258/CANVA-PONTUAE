using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PontuaAe.Dominio.FidelidadeContexto.Consulta.ConfigPontuacaoConsulta
{
    public class ObterDetalheConfigPontuacao
    {

        public int ID { get; set; }
        public string Nome { get; set; }
        public decimal Reais { get; set; }
        public decimal PontosFidelidade { get; set; }
        public int validadePontos { get; set; }
        public int Percentual { get; set; }
        public int TipoProgramaFidelidade { get; set; }
        //public decimal PontosInicial { get; set; } Remover

    }
}
