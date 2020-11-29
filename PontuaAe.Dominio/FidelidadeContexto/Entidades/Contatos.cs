using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PontuaAe.Dominio.FidelidadeContexto.Entidades
{
    public class Contatos
    {
        public Contatos()
        {

        }
        public Contatos(int IDEmpresa, string numero)
        {
            IdEmpresa = IDEmpresa;
            Numero = numero;
        }

        public int IdEmpresa {  get;  set; }
        public string Numero {  get;  set; }
    }
}
