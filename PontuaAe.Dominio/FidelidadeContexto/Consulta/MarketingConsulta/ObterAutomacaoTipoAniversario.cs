using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PontuaAe.Dominio.FidelidadeContexto.Consulta.MarketingConsulta
{
    public class ObterAutomacaoTipoAniversario
    {
  
        //public ObterAutomacaoTipoAniversario(DateTime nasc, string[] contato, string conteudo)
        //{
        //    DataNascimento = nasc;
        //    Telefone = contato;
        //    Conteudo = conteudo;
        //}

        public int ID { get; set; }
        public int IdEmpresa { get; set; }
        public string TipoAutomacao { get; set; }
        public string Conteudo { get; set; }
        public string Segmentacao { get; set; }
        public string SegCustomizado { get; set; }
        public int Estado { get; set; }
        public string Contato { get; set; }
        public string DataNascimeto{ get; set; }
        public int DiasAntesAniversario { get; set; }
    }
}
