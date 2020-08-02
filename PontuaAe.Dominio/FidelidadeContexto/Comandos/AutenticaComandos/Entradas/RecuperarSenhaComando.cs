using PontuaAe.Compartilhado.Comandos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PontuaAe.Dominio.FidelidadeContexto.Comandos.AutenticaComandos.Entradas
{
    public class RecuperarSenhaComando : IComando

    {
        public string Email { get; set; }
        public string EmailName { get; set; }

        public bool Valida()
        {
            throw new NotImplementedException();
        }
    }
}
