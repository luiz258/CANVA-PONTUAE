using PontuaAe.Dominio.FidelidadeContexto.Consulta.ConsultaCliente;
using PontuaAe.Dominio.FidelidadeContexto.Entidades;
using System.Collections.Generic;
using PontuaAe.Dominio.FidelidadeContexto.Consulta.ClienteConsulta;
using PontuaAe.Dominio.FidelidadeContexto.Consulta.RelatoriosConsulta;
using PontuaAe.Dominio.FidelidadeContexto.Consulta.EmpresaConsulta;
using PontuaAe.Dominio.FidelidadeContexto.Consulta.UsuarioConsulta;
using System.Threading.Tasks;

namespace PontuaAe.Dominio.FidelidadeContexto.Repositorios
{
   public interface IClienteRepositorio
    {
        Task<bool> ChecarTelefone(string Telefone);
        Task<int> ChecarCampoIdUsuarioPorContato(string Contato);
        Task<int> ObterID(int IdUsuario);
        Task<ObterPerfilCliente> ObterClientePorIdUsuario(int IdUsuario);
        Task<ObterDadosDoCliente> ObterDetalheCliente(int IdEmpresa, int IdUsuario);
        Task<ObterUsuarioCliente> ObterDadosDoUsuarioCliente(int IdUsuario);
        Task Salvar(Cliente cliente);
        Task Editar(Cliente cliente);
        //List<Cliente> ObterDadosClientes(int IdEmpresa, string Segmento, string SegCustomizado);   //verifica se não estiver utilizando deleta
        Task<IEnumerable<ListaRankingClientesConsulta>> ListaRankingClientes(int IdEmpresa);
        Task EditarClassificacaoCliente(Pontuacao cliente);
        Task<ObterSaldoClienteConsulta> ObterSaldo(int IdEmpresa, string Contato);
        Task<IEnumerable<ObterSaldoClienteConsulta>> ListaDeSaldoPorEmpresa(string Contato);
        Task<int> ObterTotalCliente(int IdEmpresa);
        Task<int> ObterTotalClientesRetido(int IdEmpresa);
        





    }
}
