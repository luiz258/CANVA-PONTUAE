using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PontuaAe.Dominio.FidelidadeContexto.Entidades
{
    public class ContaSMS
    {
        public ContaSMS()
        {

        }
        public ContaSMS(int idEmpresa, int saldo)
        {
            IdEmpresa = idEmpresa;
            Saldo = saldo;
        }

        public ContaSMS(int id, int idEmpresa, int saldo )
        {
            ID = id;
            IdEmpresa = idEmpresa;
            Saldo = saldo;
        }

      

        public int ID { get;  set; }
        public int IdEmpresa { get;  set; }
        public int Saldo { get; set; }


    }
}
