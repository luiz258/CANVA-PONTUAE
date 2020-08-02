using PontuaAe.Compartilhado.Comandos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PontuaAe.Dominio.FidelidadeContexto.Comandos.AutenticaComandos.Entradas
{
    //Vê se está utilizando esta classe se não delet
    public class AddContaFuncionarioComando : IComando
    {
        public string NomeCompleto { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }

        public string RoleId => "Funcionario";

        public bool Valida()
        {
            throw new NotImplementedException();
        }
    }
}
