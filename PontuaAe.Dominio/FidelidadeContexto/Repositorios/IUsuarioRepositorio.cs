using PontuaAe.Dominio.FidelidadeContexto.Consulta;
using PontuaAe.Dominio.FidelidadeContexto.Consulta.EmpresaConsulta;
using PontuaAe.Dominio.FidelidadeContexto.Entidades;
using PontuaAe.Dominio.FidelidadeContexto.ObjetoValor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PontuaAe.Dominio.FidelidadeContexto.Repositorios
{
    public interface IUsuarioRepositorio
    {
        Task Salvar(Usuario usuario);
        Task Editar(Usuario usuario);
        Task Deletar(int ID);
        Task Desativar(int ID);
        Task AlteraConta(int ID, string Email, string Senha);
        Task ResetaSenha(string Senha, int ID);
        Task<bool> ValidaEmail(string Email);
        Task<Usuario> ObterUsuario(string Email);
        Task<Usuario> ObterUsuarioCliente(string Email);



    }
}
