using PontuaAe.Compartilhado.Comandos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PontuaAe.Dominio.FidelidadeContexto.Comandos.AutenticaComandos.Resultados
{
    public class ComandoUsuarioResultado : IComandoResultado
    {
        public ComandoUsuarioResultado(bool sucesso)
        {
            Sucesso = sucesso;
        }
        public ComandoUsuarioResultado(bool sucesso, string mensage, object dado)
        {
            Sucesso = sucesso;
            Mensage = mensage;
            Dado = dado;
        }
        public ComandoUsuarioResultado(bool sucesso,string mensage)
        {
            Sucesso = sucesso;
            Mensage = mensage;
        }
        public int Id { get; set; }
        public string Mensage { get; set; }
        public bool Sucesso { get; set; }
        public object Dado { get; set; }
    
    }
}
