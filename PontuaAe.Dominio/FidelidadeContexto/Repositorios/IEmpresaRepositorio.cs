using PontuaAe.Dominio.FidelidadeContexto.Consulta.ConfigPontuacaoConsulta;
using PontuaAe.Dominio.FidelidadeContexto.Consulta.EmpresaConsulta;
using PontuaAe.Dominio.FidelidadeContexto.Consulta.UsuarioConsulta;
using PontuaAe.Dominio.FidelidadeContexto.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PontuaAe.Dominio.FidelidadeContexto.Repositorios
{
    public interface IEmpresaRepositorio
    {
        Task Salvar(Empresa empresa);
        Task Editar(Empresa empresa);
        Task<bool> ChecarDocumento(string Documento);
        Task<bool> ChecarEmail(string EnderecoEmail);
        Task<string> ObterNome(int ID);
        Task<int> ObterIdEmpresa(int IdUsuario);
        Task<ObterDetalheEmpresa> ObterDetalheEmpresa(int ID);
        Task<IEnumerable<ListarEmpresasConsulta>> ListaEmpresa();




    }
}
