using PontuaAe.Dominio.FidelidadeContexto.Consulta.ConfigPontuacaoConsulta;
using PontuaAe.Dominio.FidelidadeContexto.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PontuaAe.Dominio.FidelidadeContexto.Repositorios
{
    public interface IConfigPontosRepositorio
    {
        Task SalvaConfiguracaoPontuacao(ConfiguracaoPontos regraPontuacao);
        Task EditarConfiguracaoPontuacao(ConfiguracaoPontos configuracaoPontos);
        Task<ConfiguracaoPontos> ObterdadosConfiguracao(int IdEmpresa);
        Task<ConfiguracaoPontos> ObterValidade(int IdEmpresa);
        Task<ObterDetalheConfigPontuacao> ObterDetalheConfigPontuacao(int IdEmpresa);




    }
}
