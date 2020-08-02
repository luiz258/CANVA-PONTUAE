using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PontuaAe.Dominio.FidelidadeContexto.Consulta.MarketingConsulta
{
    public class ObterAutomacaoTipoUltimaFide
    {
        public int ID { get; set; }
        public int IdEmpresa { get; set; }
        public string NomeFantasia { get; set; }
        public string NomeCompleto { get; set; }
        public DateTime DataVisita { get; set; }
        public string Conteudo { get; set; }
        public int Estado { get; set; }
        public string Segmentacao { get; set; }
        public string Contato { get; set; }
        public string SegCustomizado { get; set; }
        public string TipoAutomacao { get; set; }
        public int TempoPorDia { get; set; }

    }
}
