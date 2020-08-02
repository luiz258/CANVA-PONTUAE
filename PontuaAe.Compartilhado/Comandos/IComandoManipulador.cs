using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PontuaAe.Compartilhado.Comandos
{
   public interface IComandoManipulador<T> where T: IComando
    {
        Task<IComandoResultado> ManipularAsync(T comando);
    }
}
