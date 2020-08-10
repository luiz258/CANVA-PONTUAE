using PontuaAe.Domain.FidelidadeContexto.Entidades;
using PontuaAe.Dominio.FidelidadeContexto.Consulta.FuncionarioConsulta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PontuaAe.Dominio.FidelidadeContexto.Repositorios
{
    public interface IFuncionarioRepositorio
    {
        Task Salvar(Funcionario funcionario);
        Task Editar(Funcionario funcionario);
        Task Deletar(int IdUsuario, int IdEmpresa);
        Task<int> ObterId(int ID);
        Task<int> ObterIdEmpresa(int IdUsuario);
        Task<IEnumerable<ListaFuncionarioConsulta>> ListaFuncionario(int IdEmpresa);
        Task<ObterDetalheFuncionarioConsulta> ObterDetalheFuncionario(int ID, int IdEmpresa);

        Task<string> ObterContatoFuncionario(int IdUsuario);
    }
}
