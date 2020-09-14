using PontuaAe.Dominio.FidelidadeContexto.Consulta.ConfigPontuacaoConsulta;
using PontuaAe.Dominio.FidelidadeContexto.Entidades;
using PontuaAe.Dominio.FidelidadeContexto.Repositorios;
using System;
using Dapper;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PontuaAe.Infra.FidelidadeContexto.DataContexto;

namespace PontuaAe.Infra.Repositorios.RepositorioFidelidade  
{
    public class ConfigPontosRepositorio : IConfigPontosRepositorio
    {
        private readonly DbConfig _db;
        public ConfigPontosRepositorio(DbConfig db)
        {
            _db = db;
        }

        public async Task<ObterDetalheConfigPontuacao> ObterDetalheConfigPontuacao(int IdEmpresa)
        {         
            return await _db.Connection
                 .QueryFirstOrDefaultAsync<ObterDetalheConfigPontuacao>("SELECT * FROM CONFIG_PONTUACAO WHERE IdEmpresa=@IdEmpresa", new {@IdEmpresa = IdEmpresa });

        }

        public async Task EditarConfiguracaoPontuacao(ConfiguracaoPontos regra)
        {
           await _db.Connection
              .ExecuteAsync("UPDATE CONFIG_PONTUACAO SET Nome=@Nome, Reais=@Reais, PontosFidelidade=@PontosFidelidade, ValidadePontos=@ValidadePontos WHERE IdEmpresa = @IdEmpresa ", new
              {
                  @Nome = regra.Nome,
                  @Reais = regra.Reais,
                  @PontosFidelidade = regra.PontosFidelidade,
                  @ValidadePontos = regra.ValidadePontos,
                  @IdEmpresa = regra.IdEmpresa
                 
              });
        }

        public async Task SalvaConfiguracaoPontuacao(ConfiguracaoPontos regra)
        {
           await _db.Connection
                .ExecuteAsync("INSERT INTO CONFIG_PONTUACAO ( Nome, Reais, PontosFidelidade, ValidadePontos, TipoProgramaFidelidade, IdEmpresa) VALUES (@Nome, @Reais, @PontosFidelidade, @ValidadePontos, @TipoProgramaFidelidade, @IdEmpresa)", new
                {
                    @Nome = regra.Nome,
                    @Reais = regra.Reais,
                    @PontosFidelidade = regra.PontosFidelidade,
                    @ValidadePontos = regra.ValidadePontos,
                    @TipoProgramaFidelidade = regra.TipoDeProgramaFidelidade,
                    @IdEmpresa = regra.IdEmpresa

                });
        }


        public async Task<ConfiguracaoPontos> ObterValidade(int IdEmpresa)
        {
            return await _db.Connection
                .QueryFirstOrDefaultAsync<ConfiguracaoPontos>("SELECT ValidadePontos  FROM CONTA_PONTUACAO WHERE IdEmpresa = @IdEmpresa", new { @IdEmpresa = IdEmpresa });
        }

        public async Task<ConfiguracaoPontos> ObterdadosConfiguracao(int IdEmpresa)
        {
            return await _db.Connection
                .QueryFirstAsync<ConfiguracaoPontos>("SELECT * FROM CONFIG_PONTUACAO WHERE IdEmpresa = @IdEmpresa", new { @IdEmpresa = IdEmpresa });
       
        }
    }

}

