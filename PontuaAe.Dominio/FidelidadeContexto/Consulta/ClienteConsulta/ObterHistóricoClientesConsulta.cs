using System;

namespace PontuaAe.Dominio.FidelidadeContexto.Consulta.ConsultaCliente
{
    public class ObterHistóricoClientesConsulta
    {
        public string NomeCompleto { get; set; }
        public DateTime DataVisita { get; set; }
        public string TipoAtividade { get; set; }
        public decimal QtdPontos { get; set; }
        public decimal ValorGasto { get; set; }
    }
}
