using Dapper;
using PontuaAe.Dominio.FidelidadeContexto.Consulta.EmpresaConsulta;
using PontuaAe.Dominio.FidelidadeContexto.Entidades;
using PontuaAe.Dominio.FidelidadeContexto.Repositorios;
using PontuaAe.Infra.FidelidadeContexto.DataContexto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PontuaAe.Infra.Repositorios.RepositorioFidelidade
{
    public class PontuacaoRepositorio : IPontuacaoRepositorio
    {
        private readonly DbConfig _db;
        public PontuacaoRepositorio(DbConfig db)
        {
            _db = db;
        }

        public async Task AtualizarSaldo(Pontuacao update)
        {
           await _db.Connection
                 .ExecuteAsync("UPDATE PONTUACAO SET Validade=@Validade Saldo=@Saldo, DataVisita=@DataVisita,  SaldoTransacao=@SaldoTransacao  WHERE IdEmpresa=@IdEmpresa and IdPreCadastro=@IdPreCadastro", new {
                     Validade = update.Validade,
                     @Saldo = update.Saldo,
                     @DataVisita = update.DataVisita,
                     @SaldoTransacao = update.SaldoTransacao,
                     @IdEmpresa = update.IdEmpresa,
                     @IdPreCadastro = update.IdPreCadastro,
             
                    
                 });
        }

        public Task<bool> ChecarCelular(string Telefone)   //talvez deleta
        {
            return _db.Connection
                .QueryFirstOrDefaultAsync<bool>("SELECT CASE WHEN EXISTS( SELECT Telefone FROM CLIENTE WHERE IdEmpresa = @IdEmpresa AND Telefone = @Telefone  ) THEN CAST( 1 AS BIT) ELSE CAST(0 AS BIT)  END", new { @Telefone = Telefone });
        }

        public async Task CriarPontuacao(Pontuacao pontuacao)
        {
           await _db.Connection
                .ExecuteAsync("INSERT INTO PONTUACAO (IdEmpresa, IdPreCadastro, DataVisita, Saldo, SaldoTransacao, Segmentacao, SegCustomizado) VALUES (@IdEmpresa, @IdPreCadastro, @DataVisita, @Saldo, @SaldoTransacao, @Segmentacao, @SegCustomizado)", new
                {
                    @IdEmpresa = pontuacao.IdEmpresa,
                    @IdPreCadastro = pontuacao.IdPreCadastro,
                    @DataVisita = pontuacao.DataVisita.Date,
                    @Saldo = pontuacao.Saldo,
                    @SaldoTransacao = pontuacao.SaldoTransacao,
                    @Segmentacao = pontuacao.Segmentacao,
                    @SegCustomizado =pontuacao.SegCustomizado


                });
        }

        public Task<decimal> obterSaldo(int IdEmpresa, int IdPreCadastro)
        {
            return _db.Connection
                .ExecuteScalarAsync<decimal>("SELECT Saldo FROM PONTUACAO WHERE IdEmpresa=@IdEmpresa AND IdPreCadastro=@IdPreCadastro ", new { @IdEmpresa = IdEmpresa, @IdPreCadastro = IdPreCadastro });
        }

        //corrigir resgatar 
        public async Task resgatar(Pontuacao resgatar)
        {
           await _db.Connection.ExecuteAsync("UPDATE PONTUACAO SET Saldo=@Saldo WHERE IdEmpresa=@IdEmpresa", new
            {
                @Saldo = resgatar.Saldo,

            });

        }

        public async Task<ObterIdEmpresaConsulta> ChecarCampoIdEmpresa(int IdEmpresa)
        {
            return await _db.Connection
                 .QueryFirstOrDefaultAsync<ObterIdEmpresaConsulta>("SELECT ID FROM PONTUACAO WHERE IdEmpresa=@IdEmpresa", new { @IdEmpresa = IdEmpresa });
        }


        public async Task<Pontuacao> ObterUltimaVisita(int IdEmpresa, int IdCliente)
        {
            return await _db.Connection
                 .QueryFirstOrDefaultAsync<Pontuacao>("SELECT DataVisita FROM PONTUACAO WHERE IdEmpresa=@IdEmpresa AND IdCliente=@IdCliente", new { @IdEmpresa = IdEmpresa, @IdCliente = IdCliente });
        }

        
        public async Task<decimal> SaldoAnterior(int IdPreCadastro, int IdEmpresa)
        {
            return await _db.Connection
                .QueryFirstOrDefaultAsync<decimal>("Select Saldo FROM PONTUACAO WHERE IdEmpresa=@IdEmpresa AND IdPreCadastro=@IdPreCadastro ", new { @IdEmpresa = IdEmpresa, @IdPreCadastro = IdPreCadastro });
        }

        public async Task<IEnumerable<Pontuacao>> ObterClassificacaoCliente()
        {
            return await _db.Connection.QueryAsync<Pontuacao>("SELECT p.ID, p.IdEmpresa, p.SegCustomizado, p.Segmentacao, p.DataVisita FROM PONTUACAO p ");
        }

        public async Task<bool> ChecarClienteNaBasePontuacao(int IdPreCadastro, int IdEmpresa)
        {
            return await _db.Connection.QueryFirstAsync<bool>("SELECT CASE WHEN EXISTS( SELECT IdPreCadastro FROM PONTUACAO WHERE  IdPreCadastro = @IdPreCadastro AND IdEmpresa = @IdEmpresa  ) THEN CAST( 1 AS BIT) ELSE CAST(0 AS BIT)  END", new { @IdPreCadastro = IdPreCadastro, @IdEmpresa = IdEmpresa });
        }


        public async Task<int> ObterIdPontuacao(int IdEmpresa, int IdPreCadastro)
        {
            return await _db.Connection.ExecuteScalarAsync<int>("Select ID FROM PONTUACAO WHERE IdEmpresa=@IdEmpresa AND IdPreCadastro=@IdPreCadastro", new { @IdEmpresa = IdEmpresa, @IdPreCadastro = IdPreCadastro });
        }

        public async Task RegistraResgate(RegistroResgate dado)
        {
            await _db.Connection
                  .ExecuteAsync("INSERT INTO REGISTRO_RESGATE (IdPreCadastro, IdEmpresa, IdUsuario, PontoResgatado, DataResgate) VALUES (@IdPreCadastro, @IdEmpresa, @IdUsuario, @PontoResgatado, @DataResgate)", new {
                      @IdPreCadastro = dado.IdPreCadastro,
                      @IdEmpresa =  dado.IdEmpresa,
                      @IdUsuario = dado.IdUsuario,
                      @PontoResgatado = dado.PontoResgatado,
                      @DataResgate = dado.DataResgate
                  });

        }
    }
}
