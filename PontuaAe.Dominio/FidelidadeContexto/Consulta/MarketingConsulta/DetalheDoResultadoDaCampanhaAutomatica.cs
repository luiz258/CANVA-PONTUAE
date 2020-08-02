using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PontuaAe.Dominio.FidelidadeContexto.Consulta.MarketingConsulta
{
    public class DetalheDoResultadoDaCampanhaAutomatica
    {

        public int ID { get; set; }
        public string TipoAutomacao { get; set; }
        public string Conteudo { get; set; }
        public int QtdEnviada { get; set; }
        public int Estado { get; set; }
        public decimal ValorInvestido { get; set; }
    
        public int TotalVendas { get; set; }
        public DateTime DataEnvio { get; set; }



    }
}
