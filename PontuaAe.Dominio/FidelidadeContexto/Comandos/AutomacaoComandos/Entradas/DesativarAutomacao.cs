using PontuaAe.Compartilhado.Comandos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PontuaAe.Dominio.FidelidadeContexto.Comandos.AutomacaoComandos.Entradas
{
    public class DesativarAutomacao : IComando
    {
        public int Id { get; set; }
        public int IdEmpresa { get; set; }

        public bool Valida()
        {
            throw new NotImplementedException();
        }
    }
}
