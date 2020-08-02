using System;
using System.Collections.Generic;
using System.Text;

namespace PontuaAe.Compartilhado.Comandos
{
   public interface IComandoResultado
    {
        bool Sucesso { get; set; }
        string Mensage  { get; set; }
        object Dado { get; set; }
    }
}
