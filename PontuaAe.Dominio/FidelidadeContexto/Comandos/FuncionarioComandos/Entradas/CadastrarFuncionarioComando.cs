using FluentValidator;
using FluentValidator.Validation;
using PontuaAe.Compartilhado.Comandos;

namespace PontuaAe.Dominio.FidelidadeContexto.Comandos.FuncionarioComandos.Entradas
{
   public class CadastrarFuncionarioComando : IComando
    {
        public int IdEmpresa { get; set; }
        public string NomeCompleto { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string Contato { get; set; }
        public int ControleUsuario { get; set; }

        public bool Valida()
        {
            throw new System.NotImplementedException();
        }
    }
}
