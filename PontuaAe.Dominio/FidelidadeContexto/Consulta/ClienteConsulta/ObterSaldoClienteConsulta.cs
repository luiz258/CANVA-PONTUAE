using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PontuaAe.Dominio.FidelidadeContexto.Consulta.ClienteConsulta
{
    public class ObterSaldoClienteConsulta
    {
        public int Id { get; set; }
        public int IdEmpresa { get; set; }
        public decimal Saldo { get; set; }
        public string NomeFantasia { get; set; }
    }
}
