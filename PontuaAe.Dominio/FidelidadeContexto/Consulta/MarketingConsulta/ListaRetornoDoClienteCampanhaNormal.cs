using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PontuaAe.Dominio.FidelidadeContexto.Consulta.MarketingConsulta
{
    public class ListaRetornoDoClienteCampanhaNormal
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public string Contatos { get; set; }
        public string Segmentacao { get; set; }
        public string SegCustomizado { get; set; }
        public string DataRetorno { get; set; } 
    }
}
