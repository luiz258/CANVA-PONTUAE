using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PontuaAe.Dominio.FidelidadeContexto.Consulta.MarketingConsulta
{
    public class ObterAutomacaoTipoDiaMes
    {
        public int ID { get; set; } 
        public int IdEmpresa { get; set; }
        public string TempoPorDiaDoMes { get; set; }
        public string Conteudo { get; set; }
        public bool Estado { get; set; }
        public string Segmentacao { get; set; }
        public string SegCustomizado { get; set; }
        public string TipoAutomacao { get; set; }
        public string[] Telefone { get; set; }
    }
}
