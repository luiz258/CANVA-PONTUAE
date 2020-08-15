using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PontuaAe.Dominio.FidelidadeContexto.Consulta.MarketingConsulta
{
    public class ObterAutomacaoPorId
    {
        public int ID { get; set; }
        public string TipoAutomacao { get; set; }
        public string Segmentacao { get; set; }
        public string SegCustomizado { get; set; }
        public string TempoPorDiaDaSemana { get; set; }
        public int TempoPorDiaDoMes { get; set; }
        public int DiasAntesAniversario { get; set; }
        public string Conteudo { get; set; }
    }
}
