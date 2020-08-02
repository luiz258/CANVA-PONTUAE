using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using PontuaAe.Dominio.FidelidadeContexto.Entidades;
using PontuaAe.Dominio.FidelidadeContexto.Repositorios;
using PontuaAe.Infra.FidelidadeContexto.DataContexto;


namespace PontuaAe.Infra.Repositorios.RepositorioFidelidade
{
    public class ReceitaRepositorio : IReceitaRepositorio
    {
        private readonly DbConfig _db;


        public ReceitaRepositorio(DbConfig db)
        {
            _db = db;
        }       

        public async Task Salvar(Receita receita)
        {
           await _db.Connection     
                 .ExecuteAsync("INSERT INTO RECEITA ( IdEmpresa, IdPontuacao, IdUsuario, Valor, DataVenda, TipoAtividade) values (@IdEmpresa, @IdPontuacao, @IdUsuario, @Valor, @DataVenda, @TipoAtividade)", 
                 new { @IdEmpresa = receita.IdEmpresa,
                       @IdPontuacao = receita.IdPontuacao,
                       @IdUsuario = receita.IdUsuario,
                       @Valor = receita.Valor, 
                       @DataVenda = receita.DataVenda,
                       @TipoAtividade = receita.TipoAtividade });
        }

        public Task<int> ObterQtdDiasAusenteClassificacaoPrata(int IdEmpresa, int IdPontuacao, int TempoEmDiasClientePrata) //https://www.devmedia.com.br/funcoes-de-data-no-sql-server/1946
        {
            return _db.Connection.ExecuteScalarAsync<int>("SELECT COUNT (DataVenda) FROM RECEITA WHERE IdPontuacao = @IdPontuacao and IdEmpresa = @IdEmpresa and DataVenda  BETWEEN DATEADD(MONTH, @TempoEmDiasClientePrata, CONVERT(date, GETDATE())) AND DATEADD(MONTH, 1, CONVERT(DATE, GETDATE()))", new
            { @IdPontuacao = IdPontuacao, @IdEmpresa = IdEmpresa, @TempoEmDiasClientePrata  = TempoEmDiasClientePrata });
        }

        public async Task<int> ObterQtdDiasAusenteClassificacaoOuro(int IdEmpresa, int IdPontuacao, int TempoEmdiasClienteOuro)
        {
            return await _db.Connection.ExecuteScalarAsync<int>("SELECT COUNT (DataVenda) FROM RECEITA WHERE IdPontuacao = @IdPontuacao and IdEmpresa = @IdEmpresa and DataVenda  BETWEEN DATEADD(MONTH, @TempoEmdiasClienteOuro, CONVERT(date, GETDATE())) AND DATEADD(MONTH, 1 , CONVERT(DATE, GETDATE()))", new
            { @IdPontuacao = IdPontuacao, @IdEmpresa = IdEmpresa, @TempoEmdiasClienteOuro = TempoEmdiasClienteOuro });
        }

        public async Task<int> ObterQtdDiasAusenteClassificacaoBronze(int IdEmpresa, int IdPontuacao, int TempoEmDiasClienteBronze)
        {
            return await _db.Connection.ExecuteScalarAsync<int>("SELECT COUNT ( DataVenda) FROM RECEITA WHERE IdPontuacao = @IdPontuacao and IdEmpresa = @IdEmpresa and DataVenda BETWEEN DATEADD(MONTH, @TempoEmDiasClienteBronze, CONVERT(date, GETDATE())) AND DATEADD(MONTH, 1, CONVERT(DATE, GETDATE()))", new
            { @IdPontuacao = IdPontuacao,  @IdEmpresa = IdEmpresa, @TempoEmDiasClienteBronze = TempoEmDiasClienteBronze });
        }

        //CALCULO DO TICKETMEDIO   REMOVE O  IdCliente
        public async Task<decimal> ObterTicketMedio(int IdEmpresa)
        {
            return await _db.Connection.ExecuteScalarAsync<decimal>("SELECT SUM(r.Valor) / COUNT(r.IdPontuacao) as totalTicket FROM RECEITA  AS r  WHERE IdEmpresa = @IdEmpresa AND DataVenda BETWEEN DATEADD(MONTH, -1, CONVERT(date, GETDATE())) AND DATEADD(MONTH, 1, CONVERT(DATE, GETDATE()))", new { @IdEmpresa = IdEmpresa });
        }

        public async Task<decimal> ObterTotalVendasMes(int IdEmpresa)
        {
            return await _db.Connection.ExecuteScalarAsync<decimal>("SELECT SUM(r.Valor) as totalVendas FROM RECEITA AS r WHERE IdEmpresa = @IdEmpresa AND DataVenda BETWEEN DATEADD(MONTH, -1, CONVERT(date, GETDATE())) AND DATEADD(MONTH, 1, CONVERT(DATE, GETDATE()))", new { @IdEmpresa = IdEmpresa});
        }

        public async Task<decimal> ObterReceitaRetidosMes(int IdEmpresa)
        {
            return await _db.Connection.ExecuteScalarAsync<decimal>("SELECT SUM(r.Valor) FROM RECEITA AS r CLIENTE AS c WHERE c.TipoCliente = 'Vip' AND IdEmpresa = @IdEmpresa AND DataVenda BETWEEN DATEADD(MONTH, 0, CONVERT(DATE, GETDATE())) AND DATEADD(MONTH, 1, CONVERT(DATE, GETDATE()))", new { @IdEmpresa = IdEmpresa });
        }

        //NÃO FOI TESTADO, LEMBRA DE TESTA ESSE COMANDO
        public async Task<decimal> ObterReceitaRetidosSemana(int IdEmpresa)  
        {
            return await _db.Connection.ExecuteScalarAsync<decimal>("SELECT SUM(r.Valor) FROM RECEITA AS r CLIENTE AS c WHERE c.TipoCliente = 'Vip' AND IdEmpresa = @IdEmpresa AND  DATEADD(WEEK, -1, CONVERT(DATE, GETDATE()))", new { @IdEmpresa = IdEmpresa });
        }

        public async Task<decimal> ObterReceitaRetidosDia(int IdEmpresa)
        {
            return await _db.Connection.ExecuteScalarAsync<decimal>("SELECT SUM(r.Valor) FROM RECEITA AS r, CLIENTE AS c WHERE c.TipoCliente = 'Vip' AND IdEmpresa = @IdEmpresa AND  DATEADD(Day, 1, CONVERT(DATE, GETDATE()))", new { @IdEmpresa = IdEmpresa });
        }



        //public IList<ObterDiasPicoVendasConsulta> ObterDiasPicoVendas(int IdEmpresa) Verifica esse comando
        //{
        //    return _db.Connection.QueryFirstOrDefault <List<ObterDiasPicoVendasConsulta>>("SELECT DATEPART(DAY,r.DataVenda) AS Dias_Semana COUNT(*) AS contador FROM RECEITA AS r WHERE IdEmpresa = @IdEmpresa GROUP BY DATEPART(DAY, r.DataVenda) ORDER BY contador", new { @IdEmpresa = IdEmpresa });
        //}

    }
}
