using PontuaAe.Dominio.FidelidadeContexto.Consulta.PremiosConsulta;
using PontuaAe.Dominio.FidelidadeContexto.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PontuaAe.Dominio.FidelidadeContexto.Repositorios
{
    public interface IPremioRepositorio
    {
        Task Salvar(Premios premio);
        Task Editar(Premios premio);
        Task Deletar(int IdEmpresa, int ID);
        Task<ObterDetalhePremioConsulta> ObterDetalhePremio(int ID, int IdEmpresa);
        Task<IEnumerable<ListarPremiosConsulta>> listaPremios(int IdEmpresa);
        Task<IEnumerable<ListarPremiosPorClienteConsulta>> listaPremiosPorCliente(int IdEmpresa, int IdPreCadastro);
        Task<IEnumerable<Premios>> PremiosDisponiveis(int IdEmpresa, decimal Saldo);
        Task<Premios> obterPontosNecessario(int IdEmpresa);
        //ObterDetalhePremioConsulta DetalhePremiacao(int Id, int IdEmpresa);
        
    }
}
