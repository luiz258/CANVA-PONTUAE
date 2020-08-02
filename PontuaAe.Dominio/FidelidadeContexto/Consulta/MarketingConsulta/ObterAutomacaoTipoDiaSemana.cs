using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PontuaAe.Dominio.FidelidadeContexto.Consulta.MarketingConsulta
{
    public class ObterAutomacaoTipoDiaSemana
    {

        public int ID { get; set; }
        public int IdEmpresa { get; set; }      
        public string NomeFantasia { get; set; }      
        public string Conteudo { get; set; }
        public int Estado { get; set; }
        public string Segmentacao { get; set; }
        public string SegCustomizado { get; set; }
        public string TipoAutomacao { get; set; }
        public string Contato { get; set; }
        public string TempoPorDiaDaSemana { get; set; }
        public string NomeDoDiaDaSemana { get; set; }

    }
}
