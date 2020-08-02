using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PontuaAe.Dominio.FidelidadeContexto.Consulta.ClienteConsulta
{
    public class ListaRankingClientesConsulta
    {
        public int ID { get; set; }
        public string NomeCompleto { get; set; }
        public string Contato { get; set; }
        public DateTime UltimaVisita { get; set; }

        public int QtdVisita { get; set; }
        public string Segmentacao { get; set; }
        public string SegCustomizado { get; set; }
        public decimal GastoTotal { get; set; }    //soma
        public decimal GastoMedio { get; set; } // soma de todos os gastos  e dividir po total de visista

 

    }
}
