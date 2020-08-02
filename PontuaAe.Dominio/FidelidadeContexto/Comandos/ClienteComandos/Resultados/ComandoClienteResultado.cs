using PontuaAe.Compartilhado.Comandos;
using System;

namespace PontuaAe.Dominio.FidelidadeContexto.Comandos.ClienteComandos.Resultados
{
   public class ComandoClienteResultado : IComandoResultado
    {
        public ComandoClienteResultado(bool sucesso)
        {

        }
        public ComandoClienteResultado(bool sucesso, string mensage, object dado)
        {
            Sucesso = sucesso;
            Mensage = mensage;
            Dado = dado;           
            
        }
        
        public bool Sucesso { get; set; }
        public string Mensage { get; set; }
        public object Dado { get; set; }
    }
}
