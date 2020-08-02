using PontuaAe.Compartilhado.Comandos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PontuaAe.Dominio.FidelidadeContexto.Comandos.AutenticaComandos.Entradas
{
    public class RemoveUsuarioComando : IComando
    {
        public int IdUsuario { get; set; }
        public bool Valida()
        {
            throw new NotImplementedException();
        }
    }
}
