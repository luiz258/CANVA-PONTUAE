using PontuaAe.Dominio.FidelidadeContexto.Entidades;
using PontuaAe.Dominio.FidelidadeContexto.Repositorios;
using System;
using System.Collections.Generic;
using Dapper;
using PontuaAe.Dominio.FidelidadeContexto.Consulta.PremiosConsulta;
using PontuaAe.Infra.FidelidadeContexto.DataContexto;
using System.Threading.Tasks;

namespace PontuaAe.Infra.Repositorios.RepositorioFidelidade
{
    public class PremioRepositorio : IPremioRepositorio
    {
        private readonly DbConfig _db;

        public PremioRepositorio(DbConfig db)
        {
            _db = db;
        }

        public async Task Salvar(Premios premios)
        {
          await  _db.Connection.ExecuteAsync("INSERT INTO PREMIOS ( IdEmpresa, Nome,  Texto, PontosNecessario) values (@IdEmpresa, @Nome,  @Texto,  @PontosNecessario)", new
                 {
                     @IdEmpresa = premios.IdEmpresa,
                     @Nome = premios.Nome,
                     @Texto = premios.Descricao.Texto,                   
                     @PontosNecessario = premios.PontosNecessario

                 });
        }

        public async Task Editar(Premios premio)
        {
           await _db.Connection.ExecuteAsync("UPDATE PREMIOS SET  Nome=@NOME, Texto=@Texto,   PontosNecessario=@PontosNecessario WHERE  ID=@ID AND IdEmpresa=@IdEmpresa", new
                 {
                     @Nome = premio.Nome,
                     @Texto = premio.Descricao.Texto,
                     @PontosNecessario = premio.PontosNecessario,
                     @ID = premio.ID,
                     @IdEmpresa = premio.IdEmpresa,

            });
        }
        public async Task Deletar(int ID, int IdEmpresa)
        {
           await _db.Connection
            .ExecuteAsync("DELETE FROM PREMIOS WHERE ID = @ID AND  IdEmpresa=@IdEmpresa", new { @ID = ID, @IdEmpresa = IdEmpresa });

        }


        //public ObterDetalhePremioConsulta DetalhePremiacao(int ID, int IdEmpresa)
        //{
        //    return _db.Connection
        //         .QueryFirstOrDefault<ObterDetalhePremioConsulta>("SELECT * FROM PREMIOS WHERE ID=@ID AND IdEmpresa=@IdEmpresa  ", new
        //         {
        //             @IdEmpresa = IdEmpresa,
        //             @ID = ID
        //         });
        //}


        public async Task<IEnumerable<ListarPremiosConsulta>> listaPremios(int IdEmpresa)
        {
            return await _db.Connection
                .QueryAsync<ListarPremiosConsulta>("SELECT * FROM PREMIOS WHERE IdEmpresa = @IdEmpresa", new
                {
                    @IdEmpresa = IdEmpresa
                });
        }

        public async Task<IEnumerable<ListarPremiosPorClienteConsulta>> listaPremiosPorCliente(int IdEmpresa, int IdPreCadastro)
        {
            return await _db.Connection
                .QueryAsync<ListarPremiosPorClienteConsulta>("SELECT PREMIOS.Nome, PREMIOS.PontosNecessario, PONTUACAO.Saldo, pc.ID FROM PREMIOS, PRE_CADASTRO AS pc INNER JOIN PONTUACAO  ON  pc.ID = PONTUACAO.IdPreCadastro WHERE PONTUACAO.IdPreCadastro = @IdPreCadastro  AND PONTUACAO.IdEmpresa = @IdEmpresa ", new
                { @IdPreCadastro = IdPreCadastro, @IdEmpresa = IdEmpresa });
        }

        public async Task<Premios> obterPontosNecessario(int IdEmpresa, int ID)
        {
            return await _db.Connection
                  .QueryFirstOrDefaultAsync<Premios>("SELECT PontosNecessario FROM PREMIOS IdEmpresa=@IdEmpresa AND ID=@ID",
                  new { @IdEmpresa = IdEmpresa, @ID = ID });
        }


        //AVERIGUAR ESTE METODO
        public async Task<IEnumerable<Premios>> PremiosDisponiveis(int IdEmpresa, decimal Saldo)
        { 
            return await _db.Connection.QueryAsync<Premios>("select p nome, p pontoNecessario from PREMIO p  WHERE p pontoNecessario >= @Saldo AND IdEmpresa=@IdEmpresa", new { @IdEmpresa =IdEmpresa, @Saldo = Saldo });
        } 

        public async Task<ObterDetalhePremioConsulta> ObterDetalhePremio(int ID, int IdEmpresa)
        {
            return await _db.Connection.QueryFirstOrDefaultAsync<ObterDetalhePremioConsulta>("select ID, PontosNecessario, Texto, Nome from PREMIOS  WHERE ID=@ID AND IdEmpresa=@IdEmpresa", new { @ID = ID, @IdEmpresa = IdEmpresa });
        }



        public Task<Premios> obterPontosNecessario(int IdEmpresa)
        {
            throw new NotImplementedException();
        }


    }
}
