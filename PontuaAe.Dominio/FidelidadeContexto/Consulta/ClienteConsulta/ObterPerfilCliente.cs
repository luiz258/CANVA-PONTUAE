using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PontuaAe.Dominio.FidelidadeContexto.Consulta.ClienteConsulta
{
    
    public class ObterPerfilCliente
    {
        public int ID { get; set; }
        public int IdUsuario { get; set; }
        public string NomeCompleto { get;  set; }
        public string Email { get;  set; }
        public string Contato { get;  set; }
        public DateTime DataNascimento { get;  set; }
        public string Cidade { get;  set; }
        public string Sexo { get;  set; }
        


    }
}
