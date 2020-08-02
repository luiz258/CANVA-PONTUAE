using PontuaAe.Compartilhado.Comandos;
using PontuaAe.Dominio.FidelidadeContexto.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PontuaAe.Dominio.FidelidadeContexto.Comandos.PontuacaoComandos.Resultados
{
    public class PontuacaoResultado  : IComandoResultado
    {
        public PontuacaoResultado(bool sucesso, string mensage, object dado, object dado2, string texto2)
        {
            Sucesso = sucesso;
            Mensage = mensage;           
            Dado = dado;
            Dado2 = dado2;            
            Texto2 = texto2;
        }

        public bool Sucesso { get; set; }
        public string Mensage { get; set; }       
        public object Dado2 { get; set; }
        public object Dado { get; set; }        
        public string Texto2 { get; set; }

    }
}
