using FluentValidator;
using PontuaAe.Compartilhado.Comandos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PontuaAe.Dominio.FidelidadeContexto.Comandos.AutenticaComandos.Entradas
{
    public class AtualizarStatusComando : Notifiable, IComando
    {
        public bool status { get; set; }
        public bool Valida()
        {
            throw new NotImplementedException();
        }
    }
}
