using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PontuaAe.Dominio.FidelidadeContexto.Consulta.ClienteConsulta
{
    public class ObterAtividadesClienteConsulta
    {

        public string NomeFuncionario { get; set; }
        public DateTime Data { get; set; }
        public string TipoAtividade { get; set; }
        public decimal QtdPontos { get; set; }
        public decimal ValorGasto { get; set; }
    }
}
