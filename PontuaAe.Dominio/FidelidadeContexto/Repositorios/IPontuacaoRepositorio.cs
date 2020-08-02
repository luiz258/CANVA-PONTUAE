using PontuaAe.Dominio.FidelidadeContexto.Consulta.ConfigPontuacaoConsulta;
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
    public interface IPontuacaoRepositorio
    {
        
        Task resgatar(Pontuacao resgatar);
        Task RegistraResgate(RegistroResgate dado);
        Task AtualizarSaldo(Pontuacao update);
        Task CriarPontuacao(Pontuacao pontuacao);
        Task<bool> ChecarClienteNaBasePontuacao(int IdPreCadastro, int IdEmpresa);
        Task<IEnumerable<Pontuacao>> ObterClassificacaoCliente();
        //Pontuacao ObterEstadoFidelidade(int IdCliente, int IdEmpresa);   vê se pode deleta
        Task<ObterIdEmpresaConsulta> ChecarCampoIdEmpresa(int IdEmpresa);
        Task<Pontuacao> ObterUltimaVisita(int IdEmpresa, int IdCliente);

        Task<int> ObterIdPontuacao(int IdEmpresa, int IdPreCadastro);
        Task<decimal> obterSaldo(int IdEmpresa, int IdPreCadastro);
        Task<decimal> SaldoAnterior(int IdEmpresa, int IdPreCadastro);

    }
}
