
using PontuaAe.Compartilhado.Comandos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PontuaAe.Dominio.FidelidadeContexto.Comandos.PremioComandos.Entradas
{
    public class EditarPremioComando : IComando
    {
        public int ID { get; set; }
        public int IdEmpresa { get; set; }
        public string Title { get; set; }
        public string Texto { get; set; }
        public decimal QtdPontos { get; set; }
      
        public bool Valida()
        {
            throw new NotImplementedException();
        }
    }
}
