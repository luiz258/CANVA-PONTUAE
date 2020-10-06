using Dapper;
using PontuaAe.Dominio.FidelidadeContexto.Consulta.ClienteConsulta;
using PontuaAe.Dominio.FidelidadeContexto.Entidades;
using PontuaAe.Dominio.FidelidadeContexto.Repositorios;
using PontuaAe.Infra.FidelidadeContexto.DataContexto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PontuaAe.Infra.Repositorios.RepositorioAvaliacao
{
    public class ClienteRepositorio : IClienteRepositorio
    {
        private readonly DbConfig _db;

        public ClienteRepositorio(DbConfig db)
        {
            _db = db;
        }
               

        public async Task<int> ChecarCampoIdUsuarioPorContato(string Contato)
        {
            return await _db.Connection
                 .QueryFirstOrDefaultAsync<int>("SELECT IdUsuario  FROM CLIENTE WHERE Contato = @Contato", new { @Contato = Contato });
        }

        public async Task Editar(Cliente cliente)
        {
           await  _db.Connection
                .ExecuteAsync("UPDATE CLIENTE SET NomeCompleto=@NomeCompleto, DataNascimento=@DataNascimento, Sexo=@Sexo, Contato = @Contato WHERE IdUsuario=@IdUsuario", new
                {
                    NomeCompleto = cliente.NomeCompleto,
                    @Contato = cliente.Contato,
                    @DataNascimento = cliente.DataNascimento,
                    @Sexo = cliente.Sexo,
                    @IdUsuario = cliente.IdUsuario
                });
        }

        //public IList<ListaClienteConsulta> ListaCliente(int IdEmpresa)
        //{
        //    return _db.Connection
        //        .Query<ListaClienteConsulta>("SELECT * FROM CLIENTE WHERE IdEmpresa=@IdEmpresa", new { @IdEmpresa = IdEmpresa }).ToList();
        //}

        public async Task Salvar(Cliente cliente)
        {

            await _db.Connection
           .ExecuteAsync("INSERT INTO CLIENTE ( IdUsuario, NomeCompleto, DataNascimento, Contato, Sexo, Email, Cidade) values (@IdUsuario, @NomeCompleto, @DataNascimento, @Contato, @Sexo, @Email, @Cidade)", new
           {
               @IdUsuario = cliente.IdUsuario,
               @NomeCompleto = cliente.NomeCompleto,
               @DataNascimento = cliente.DataNascimento,
               @Contato = cliente.Contato,
               @Sexo = cliente.Sexo,
               @Email = cliente.Email.Endereco,
               @Cidade = cliente.Cidade


           }); ;
        }

        public async Task EditarClassificacaoCliente(Pontuacao pontuacao)
        {
           await  _db.Connection
               .ExecuteAsync("UPDATE PONTUACAO SET Segmentacao=@Segmentacao, SegCustomizado=@SegCustomizado WHERE ID=@ID AND IdEmpresa=@IdEmpresa",
               new
               {
                   @Segmentacao = pontuacao.Segmentacao,
                   @SegCustomizado = pontuacao.SegCustomizado,
                   @ID = pontuacao.ID,
                   @IdEmpresa =pontuacao.IdEmpresa

               });;
        }


        //REFATOREI
        public async Task<ObterSaldoClienteConsulta> ObterSaldo(int IdEmpresa, string Contato)
        {
            return await _db.Connection.QueryFirstOrDefaultAsync<ObterSaldoClienteConsulta>("SELECT  p.Saldo, e.NomeFantasia FROM PONTUACAO p INNER JOIN EMPRESA e ON p.IdEmpresa = e.ID  INNER JOIN PRE_CADASTRO pc ON p.IdPreCadastro = pc.ID  WHERE p.IdEmpresa = @IdEmpresa AND pc.Contato = @Contato", new
            {
                @IdEmpresa = IdEmpresa,
                @Contato = Contato
            });
        }

        //REFATOREI
        public async Task<IEnumerable<ObterSaldoClienteConsulta>> ListaDeSaldoPorEmpresa(string Contato)
        {
            return await _db.Connection.QueryAsync<ObterSaldoClienteConsulta>("SELECT p.IdPreCadastro, p.IdEmpresa, p.Saldo, e.NomeFantasia FROM PONTUACAO p INNER JOIN EMPRESA e ON p.IdEmpresa = e.ID  INNER JOIN PRE_CADASTRO pc ON p.IdPreCadastro = pc.ID  WHERE  pc.Contato = @Contato", new
            {
                @Contato = Contato
            });
        }

        //REFATOREI
        public async Task<IEnumerable<ListaRankingClientesConsulta>> ListaRankingClientes(int IdEmpresa)
        {
            return await _db.Connection.QueryAsync<ListaRankingClientesConsulta>("SELECT top 50 pc.ID, c.NomeCompleto, pc.Contato, p.Segmentacao, p.SegCustomizado,  max(r.DataVenda) AS UltimaVisita, AVG(r.valor) AS GastoMedio, SUM(r.valor) AS GastoTotal, COUNT(r.DataVenda) AS QtdVisita FROM PONTUACAO  p JOIN RECEITA AS r ON p.IdEmpresa = r.IdEmpresa, PRE_CADASTRO AS pc FULL OUTER JOIN CLIENTE AS c ON pc.Contato = c.Contato   WHERE p.IdEmpresa = @IdEmpresa AND p.ID = r.IdPontuacao   AND  p.IdPreCadastro = pc.ID   GROUP BY  pc.ID, c.NomeCompleto, pc.Contato, r.IdPontuacao, r.IdPontuacao, p.Segmentacao, p.SegCustomizado ORDER BY MAX(r.Valor) DESC", new { @IdEmpresa = IdEmpresa });

        }

        //Refatora pensei em busca o total de cada Segmentação customizada,  Lembra de alterar
        public async Task<int> ObterTotalClientesRetido(int IdEmpresa)
        {
            return await _db.Connection.ExecuteScalarAsync<int>("SELECT COUNT(IdPreCadastro) AS TotalRecorrentes  FROM PONTUACAO  WHERE IdEmpresa = @IdEmpresa AND Segmentacao = 'Vip' ", new { @IdEmpresa = IdEmpresa });
        }

        public async Task<int> ObterTotalCliente(int IdEmpresa)
        {
            return await _db.Connection.ExecuteScalarAsync<int>("SELECT COUNT(IdPreCadastro) AS Total FROM PONTUACAO WHERE IdEmpresa=@IdEmpresa", new { @IdEmpresa = IdEmpresa });
        }

        //Este metodo vai se repensado
        //public IEnumerable<ObterHistóricoClientesConsulta> ObterHistoricoTodosClientes(int IdEmpresa)
        //{
        //    return _db.Connection.Query<ObterHistóricoClientesConsulta>("SELECT u.NomeCompleto, p.DataVisita, r.TipoAtividade, p.Saldo, r.Valor c.Segmentacao, c.TipoCliente FROM RECEITA  AS r INNER JOIN PONTUACAO AS p ON  r.IdEmpresa = p.IdEmpresa INNER JOIN CLIENTE AS c ON r.IdEmpresa = c.IdEmpresa INNER JOIN USUARIO AS u ON r.IdUsuario = u.ID  WHERE IdEmpresa = @IdEmpresa", new { @IdEmpresa=IdEmpresa});
        //}


        //verifica se vai se utilizado se não deleta
        public async Task<IEnumerable<Cliente>> ObterDadosClientes(int IdEmpresa, string Segmento, string SegCustomizado)
        {
            return await _db.Connection.QueryFirstOrDefaultAsync<IEnumerable<Cliente>>("SELECT DataNascimento, Contato, Sexo FROM CLIENTE WHERE IdEmpresa = @IdEmpresa AND month(GETDATE())", new { @IdEmpresa = IdEmpresa });
        }
        

        public async Task<int> ObterID(int IdUsuario)
        {
            return await _db.Connection.ExecuteScalarAsync<int>("Select ID from Cliente where IdUsuario = @IdUsuario", new { @IdUsuario = IdUsuario });
        }

        public async Task<ObterPerfilCliente> ObterClientePorIdUsuario(int IdUsuario)
        {
            return await _db.Connection.QueryFirstAsync<ObterPerfilCliente>("Select * from CLIENTE where IdUsuario= @IdUsuario", new { @IdUsuario = IdUsuario });
        }

        public async Task<ObterUsuarioCliente> ObterDadosDoUsuarioCliente(int IdUsuario)
        {
            return await _db.Connection
              .QueryFirstOrDefaultAsync<ObterUsuarioCliente>("SELECT c.Contato, c.Sexo, u.ID FROM USUARIO u INNER JOIN CLIENTE c ON u.ID = c.IdUsuario WHERE c.IdUsuario = @IdUsuario", new { @IdUsuario = IdUsuario });
        }

        public async Task<bool> ChecarTelefone(string Telefone)
        {
            return await _db.Connection
                .QueryFirstOrDefaultAsync<bool>("SELECT CASE WHEN EXISTS( SELECT Contato FROM CLIENTE WHERE Contato = @Telefone ) THEN CAST( 1 AS BIT) ELSE CAST(0 AS BIT)  END", new { @Telefone = Telefone });
        }

        public async Task<ObterDadosDoCliente> ObterDetalheCliente(int IdEmpresa, int IdUsuario)
        {
            return await _db.Connection
               .QueryFirstOrDefaultAsync<ObterDadosDoCliente>(" SELECT c.NomeCompleto, pc.Contato, c.Sexo, c.Email, c.DataNascimento, p.Saldo,  p.Segmentacao, p.SegCustomizado, max(r.DataVenda) AS UltimaVisita, AVG(r.valor) AS GastoMedio, SUM(r.valor) AS Captado " +
               "FROM  PONTUACAO AS p JOIN RECEITA AS r ON p.IdEmpresa = r.IdEmpresa, PRE_CADASTRO AS pc FULL OUTER JOIN CLIENTE AS c ON pc.Contato = c.Contato " +
               "WHERE p.IdEmpresa = @IdEmpresa AND p.IdPreCadastro = @IdUsuario AND p.ID = r.IdPontuacao   AND  p.IdPreCadastro = pc.ID " +
               "GROUP BY c.NomeCompleto, pc.Contato,c.Sexo, c.Email, c.DataNascimento, p.Saldo,  p.Segmentacao, p.SegCustomizado", new {  @IdEmpresa = IdEmpresa, @IdUsuario = IdUsuario});

        }
    }
}
