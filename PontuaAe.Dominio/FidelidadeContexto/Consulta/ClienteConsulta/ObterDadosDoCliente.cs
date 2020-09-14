using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PontuaAe.Dominio.FidelidadeContexto.Consulta.ClienteConsulta
{
    public class ObterDadosDoCliente
    {
        public string NomeCompleto { get; set; }
        public string Contato { get; set; }
        public string Sexo { get; set; }
        public string Email { get; set; }
        public DateTime DataNascimento { get; set; }
       // public string Resgate { get; set; }
        public decimal Saldo { get; set; }
        public decimal Captado { get; set; }
        public decimal GastoMedio { get; set; }
        public DateTime UltimaVisita { get; set; }
        public string Segmentacao { get; set; }
        public string SegCustomizado { get; set; }

    }
}
