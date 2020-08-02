using PontuaAe.Compartilhado.Comandos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PontuaAe.Dominio.FidelidadeContexto.Comandos.PremioComandos.Entradas
{
    public class DeletarPremioComando : IComando
    {

        public int IdEmpresa { get; set; }
        public int Id { get; set; }

        public bool Valida()
        {
            throw new NotImplementedException();
        }
    }
}
