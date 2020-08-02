using Dapper;
using PontuaAe.Dominio.FidelidadeContexto.Entidades;
using PontuaAe.Dominio.FidelidadeContexto.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PontuaAe.Infra.Repositorios.RepositorioFidelidade
{
    public class ConfiguracaoCashBackRepositorio : IConfiguracaoCashBackRepositorio
    {

        private readonly DbConfig _db;

        public ConfiguracaoCashBackRepositorio(DbConfig db)
        {
            _db = db;
        }

        public async Task Desativar(int IdEmpresa)
        {
            await _db.Connection.ExecuteAsync("UPDATE CONFIG_CASHBACK  SET Estado=0  WHERE IdEmpresa = @IdEmpresa",
              new
              {
                  @IdEmpresa = IdEmpresa
              });
        }

        public async Task Editar(ConfiguracaoCashBack model)
        {
            await _db.Connection.ExecuteAsync("UPDATE CONFIG_CASHBACK  SET Percentual=@Percentual, Estado=@Estado  WHERE IdEmpresa = @IdEmpresa",
               new
               {
                   @Percentual = model.Percentual,
                   @Estado = model.Estado
               });
        }

        public async Task Salvar(ConfiguracaoCashBack model)
        {

            await _db.Connection.ExecuteAsync("INSERT INTO CONFIG_CASHBACK (Percentual, Estado)VALUES( @Percentual, @Estado)",
                new
                {
                    @Percentual = model.Percentual,
                    @Estado = model.Estado
                });
        }

        public async Task<int> ChecarConfigCashBack()
        {
            return await _db.Connection
               .QueryFirstOrDefaultAsync<int>("SELECT CASE WHEN EXISTS( SELECT ID FROM CONFIG_PONTUACAO WHERE TipoProgramaFidelidade=1) THEN CAST( 1 AS BIT) ELSE CAST(0 AS BIT)  END");

        }

        public async Task<ConfiguracaoCashBack> ObterdadosConfiguracao(int IdEmpresa)
        {
            return await _db.Connection
            .QueryFirstAsync<ConfiguracaoCashBack>("SELECT Percentual  FROM CONFIG_CASHBACK WHERE IdEmpresa = @IdEmpresa", new { @IdEmpresa = IdEmpresa });

        }

       // public async Task<ObterDetalheConfigCashBack> ObterDetalheConfigPontuacao(int IdEmpresa)
       // {
       //     return await _db.Connection
       //.QueryFirstOrDefaultAsync<ObterDetalheConfigCashBack>("SELECT * FROM CONFIG_CASHBACK WHERE IdEmpresa=@IdEmpresa", new { @IdEmpresa = IdEmpresa });
       // }
    }
}
