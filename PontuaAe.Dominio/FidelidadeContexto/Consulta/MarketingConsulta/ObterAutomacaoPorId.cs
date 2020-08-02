using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PontuaAe.Dominio.FidelidadeContexto.Consulta.MarketingConsulta
{
    public class ObterAutomacaoPorId
    {
        public int Id { get; set; }
        public string Segmento { get; set; }
        public string SegCustomizado { get; set; }
        public string DiaSemana { get; set; }
        public string DiaMes { get; set; }
        public string DiasAntensAniversario { get; set; }
        public string Conteudo { get; set; }
    }
}
