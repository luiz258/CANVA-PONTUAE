using System;

namespace PontuaAe.Dominio.FidelidadeContexto.Consulta.ConsultaCliente
{
    public class ListaClienteConsulta
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public int Visitas { get; set; }
        public int Resgates { get; set; }
        public int PontosAcumulados { get; set; }
    }
}
