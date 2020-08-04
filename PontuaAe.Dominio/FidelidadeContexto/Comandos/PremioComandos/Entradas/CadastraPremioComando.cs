using PontuaAe.Compartilhado.Comandos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PontuaAe.Dominio.FidelidadeContexto.Comandos.PremioComandos.Entradas
{
    public class CadastraPremioComando : IComando
    {
        public int IdEmpresa { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public decimal QtdPontos { get; set; }


        public bool Valida()
        {
            throw new NotImplementedException();
        }
    }
}
