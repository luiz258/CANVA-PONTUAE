using PontuaAe.Compartilhado.Comandos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PontuaAe.Dominio.FidelidadeContexto.Comandos.MarketingComandos.Entradas
{
    public class RemoverCampanhaComando : IComando
    {
        public RemoverCampanhaComando(int id, int idEmpresa)
        {
            ID = id;
            IdEmpresa = idEmpresa;
        }

        public int ID { get; set; } 
        public int IdEmpresa { get; set; } 

        public bool Valida()
        {
            throw new NotImplementedException();
        }
    }
}
