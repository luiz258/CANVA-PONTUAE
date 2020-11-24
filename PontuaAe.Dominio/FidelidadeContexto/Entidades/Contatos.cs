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
        public Contatos(int Id, string numero)
        {
            ID = Id;
            Numero = numero;
        }

        public int ID {  get;  set; }
        public string Numero {  get;  set; }
    }
}
