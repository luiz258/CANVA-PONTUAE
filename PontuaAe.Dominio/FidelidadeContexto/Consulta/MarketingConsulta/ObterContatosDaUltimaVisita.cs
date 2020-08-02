using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PontuaAe.Dominio.FidelidadeContexto.Consulta.MarketingConsulta
{
    public class ObterContatosDaUltimaVisita
    {
        public int ID { get; set; }
        public int IdCliente { get; set; }
        public string Contato { get; set; }
        public DateTime DataVisita { get; set; }

    }
}
