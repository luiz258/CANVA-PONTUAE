using Dapper;
using PontuaAe.Dominio.FidelidadeContexto.Consulta.MarketingConsulta;
using PontuaAe.Dominio.FidelidadeContexto.Entidades;
using PontuaAe.Dominio.FidelidadeContexto.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PontuaAe.Infra.Repositorios.RepositorioFidelidade
{
    public class AutomacaoSMSReposiorio : IAutomacaoMSGRepositorio
    {
        private readonly DbConfig _db; 

        public AutomacaoSMSReposiorio(DbConfig db)
        {
            _db = db;
        }
        public async Task Deletar(int IdEmpresa, int Id)
        {
           await  _db.Connection.ExecuteAsync("DELETE FROM MENSAGEM WHERE IdEmpresa=@IdEmpresa AND ID=@Id", new { @IdEmpresa = IdEmpresa, @ID = Id });
        }

        //public void Desativar(int IdEmpresa, int Id, int Estado)
        //{
        //    _db.Connection.Execute("UPDATE MENSAGEM SET Estado=@Estado WHERE IdEmpresa=@IdEmpresa AND ID=@ID", new { @IdEmpresa = IdEmpresa, @ID = Id, @Estado = Estado });
        //}



            //falta edita a agenda pra dfinir  o momento do envio
        public async Task Editar(Mensagem model)
        {
          await _db.Connection.ExecuteAsync("UPDATE MENSAGEM SET  TempoPorDia=@TempoPorDia, TipoAutomacao=@TipoAutomacao, TempoPorDiaDaSemana=@TempoPorDiaDaSemana, DiasAntesAniversario=@DiasAntesAniversario, TempoPorDiaDoMes=@TempoPorDiaDoMes, Conteudo=@Conteudo, Estado=@Estado, Segmentacao=@Segmentacao, SegCustomizado=@SegCustomizado, QtdEnviada=@QtdEnviada, ValorInvestido=@ValorInvestido  WHERE IdEmpresa=@IdEmpresa AND ID=@ID", 
              new 
              {
                  @TempoPorDia = model.TempoPorDia,
                  @TipoAutomacao = model.TipoAutomacao,
                  @TempoPorDiaDaSemana = model.TempoPorDiaDaSemana,
                  @DiasAntesAniversario = model.DiasAntesAniversario,
                  @TempoPorDiaDoMes = model.TempoPorDiaDoMes,
                  @Conteudo = model.Conteudo,
                  @Estado = model.Estado,
                  @Segmentacao = model.Segmentacao,
                  @SegCustomizado = model.SegCustomizado,
                  @QtdEnviada = model.QtdEnviada,
                  @ValorInvestido = model.ValorInvestido,
                  @IdEmpresa = model.IdEmpresa,
                  @ID = model.ID,
              });
        }

        public async Task Salvar(Mensagem model)
        {

            await _db.Connection.ExecuteAsync("INSERT INTO MENSAGEM (IdEmpresa, TempoPorDia, TipoAutomacao, TempoPorDiaDaSemana, DiasAntesAniversario, TempoPorDiaDoMes, Conteudo, SegCustomizado, Segmentacao, Estado, QtdEnviada, ValorInvestido )VALUES(@IdEmpresa, @TempoPorDia, @TipoAutomacao, @TempoPorDiaDaSemana, @DiasAntesAniversario, @TempoPorDiaDoMes, @Conteudo, @SegCustomizado, @Segmentacao, @Estado, @QtdEnviada, @ValorInvestido)",
              new
              {
                  @IdEmpresa = model.IdEmpresa,
                  @TempoPorDia = model.TempoPorDia,
                  @TipoAutomacao = model.TipoAutomacao,
                  @TempoPorDiaDaSemana = model.TempoPorDiaDaSemana,
                  @DiasAntesAniversario = model.DiasAntesAniversario,
                  @TempoPorDiaDoMes = model.TempoPorDiaDoMes,
                  @Conteudo = model.Conteudo,
                  @SegCustomizado = model.SegCustomizado,
                  @Segmentacao = model.Segmentacao,
                  @Estado = model.Estado,
                  @QtdEnviada = model.QtdEnviada,
                  @ValorInvestido = model.ValorInvestido
              });

        }

        public async Task atualizarDadosMensagem(Mensagem model)
        {
            await _db.Connection.ExecuteAsync("UPDATE MENSAGEM SET QtdEnviada=@QtdEnviada, ValorInvestido=@ValorInvestido  WHERE IdEmpresa=@IdEmpresa AND ID=@ID",
                new
                {
                    @QtdEnviada = model.QtdEnviada,
                    @ValorInvestido = model.ValorInvestido,
                    @IdEmpresa = model.IdEmpresa,
                    @ID = model.ID,
                });
        }

        

       
        public async Task<IEnumerable<ObterAutomacaoTipoAniversario>> ObterDadosAutomacaoAniversario( string Segmentacao, string SegCustomizado, int ID) //MELHORA ESSE METODO,  USA APENA O ID DA AUTOMAÇÃO GERADA E IDEMPRESA
        {
            return _db.Connection.Query<ObterAutomacaoTipoAniversario>("SELECT m.ID, m.TipoAutomacao, m.Conteudo, c.Contato,c.NomeCompleto, m.Segmentacao, m.SegCustomizado, m.DiasAntesAniversario, c.DataNascimento, e.NomeFantasia, m.Estado FROM   EMPRESA e INNER JOIN MENSAGEM m ON  e.ID = m.IdEmpresa , PRE_CADASTRO pc INNER JOIN PONTUACAO p ON pc.ID = p.IdPreCadastro INNER JOIN CLIENTE c ON pc.Contato = c.Contato WHERE e.ID = @ID AND p.Segmentacao = @Segmentacao OR p.SegCustomizado = @SegCustomizado AND MONTH(GETDATE())",

                 new
                 {
                     @ID = ID,
                     @Segmentacao = Segmentacao,
                     @SegCustomizado = SegCustomizado 
                 });
        }

        //IEnumerable<ObterAutomacaoTipoDiaSemana> IAutomacaoMSGRepositorio.ObterDadosAutomacaoSemana(string AutomacaoSemana, string Segmentacao, string SegCustomizado, int IdEmpresa)
        //{
        //    throw new NotImplementedException();
        //}
        public async Task<IEnumerable<ObterAutomacaoTipoDiaSemana>> ObterDadosAutomacaoSemana(string TipoAutomacao, string Segmentacao, string SegCustomizado, int IdEmpresa)
        {

            return _db.Connection.Query<ObterAutomacaoTipoDiaSemana>(" SELECT m.ID, m.TipoAutomacao, m.Conteudo, m.Segmentacao, m.SegCustomizado, m.TempoPorDiaDaSemana, m.Estado, e.NomeFantasia, c.NomeCompleto, pc.Contato, m.Estado FROM   EMPRESA e  JOIN MENSAGEM m ON e.ID = m.IdEmpresa, PRE_CADASTRO pc  JOIN PONTUACAO p ON pc.ID = p.IdPreCadastro FULL OUTER JOIN CLIENTE c ON pc.Contato = c.Contato WHERE p.SegCustomizado = @SegCustomizado OR p.Segmentacao = @Segmentacao AND e.ID = @IdEmpresa", new {
                    @TipoAutomacao = TipoAutomacao,
                    //@Segmentacao = Segmentacao,
                    @SegCustomizado = SegCustomizado,
                    @IdEmpresa = IdEmpresa
                   });
             
        }

        public Task<IEnumerable<ObterAutomacaoTipoDiaMes>> ObterDadosAutomacaoMes(string AutomacaoMes, string Segmentacao, string SegCustomizado, int IdEmpresa)
        {
            
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Mensagem>> ListaTipoAutomacao()
        {
            return await _db.Connection.QueryAsync<Mensagem>("SELECT ID, IdEmpresa, Segmentacao, SegCustomizado,TempoPorDia, Estado, TipoAutomacao FROM MENSAGEM ORDER by TipoAutomacao  ");
        }

        public async Task<IEnumerable<Mensagem>> ListaMensagem()
        {
            return await _db.Connection.QueryAsync<Mensagem>("SELECT ID, IdEmpresa FROM MENSAGEM");
        }

        public async Task<DetalheDoResultadoDaCampanhaAutomatica> ObterDetalheDoResultadoDaCampanha(int ID, int IdEmpresa)
        {
            return await _db.Connection.QueryFirstOrDefaultAsync<DetalheDoResultadoDaCampanhaAutomatica>("SELECT m.TipoAutomacao, m.Conteudo, m.QtdEnviada, m.ValorInvestido,  SUM(s.ValorRecebido) AS TotalVendas, COUNT(s.DataCompra) AS QtdRetorno, m.Estado FROM MENSAGEM m INNER JOIN SITUACAO_SMS S ON s.IdEmpresa = m.IdEmpresa  WHERE m.IdEmpresa = @IdEmpresa AND m.ID = @ID  AND m.EstadoEnvio NOT IN ('ok') GROUP BY m.ID, m.Nome, m.Conteudo, m.QtdEnviada, m.ValorInvestido, m.DataEnvio, m.TipoAutomacao,  m.Estado  ", new { @IdEmpresa = IdEmpresa , @ID = ID });
        }

        public async Task<ObterAutomacaoPorId> ObterDetalheDaAutomacao(int ID, int IdEmpresa)
        {
            return await _db.Connection.QueryFirstOrDefaultAsync<ObterAutomacaoPorId>("SELECT m.TipoAutomacao, m.Segmentacao, m.SegCustomizado, m.TempoPorDiaDaSemana, m.TempoPorDiaDoMes, m.DiasAntesAniversario, m.Conteudo FROM MENSAGEM m WHERE m.IdEmpresa = @IdEmpresa AND m.ID = @ID  ", new { @IdEmpresa = IdEmpresa, @ID = ID });
        }

        public async Task<IEnumerable<ObterListaAutomacao>> listaAutomacao(int IdEmpresa, int Estado)
        {
            return await _db.Connection.QueryAsync<ObterListaAutomacao>("SELECT m.ID, m.TipoAutomacao, m.Segmentacao, m.SegCustomizado, m.Estado FROM MENSAGEM m WHERE m.IdEmpresa= @IdEmpresa AND m.Estado = @Estado ", new { @IdEmpresa = IdEmpresa, @Estado = Estado });
        }

        public async Task<string[]> ListaTelefones(int IdEmpresa, string SegCustomizado, string Segmentacao)
        {
            return await _db.Connection.QueryFirstOrDefaultAsync<string[]>("SELECT pc.Contato, pc.ID,  FROM PRE_CADASTRO c INNER JOIN  PONTUACAO p ON pc.ID = p.IdPreCadastro WHERE p.IdEmpresa = @IdEmpresa AND  p.Segmentacao = @Segmentacao  OR p.SegCustomizado = @SegCustomizado ", new { @IdEmpresa = IdEmpresa, @Segmentacao = Segmentacao, @SegCustomizado = SegCustomizado});
        } 


        public async Task Desativar(int IdEmpresa, int ID)
        {

           await _db.Connection.ExecuteAsync("UPDATE MENSAGEM SET Estado=0 WHERE  IdEmpresa=@IdEmpresa  AND ID=@ID ", new {@IdEmpresa = IdEmpresa, @ID = ID});
        }
        //Esta ação vai fica comentada até me descidir se vou altera ou não
        //public async Task<IEnumerable<ObterAutomacaoTipoUltimaFide>> ObterContatosQueVisitaramAposQuinzeDias(string TipoAutomacao, string Segmentacao, string SegCustomizado, int IdEmpresa)
        //{
        //    return await _db.Connection.QueryAsync<ObterAutomacaoTipoUltimaFide>("SELECT m.ID, m.TipoAutomacao, m.Conteudo, m.Segmentacao, m.TempoPorDia, m.SegCustomizado, e.NomeFantasia, pc.Contato, c.NomeCompleto " +
        //        "FROM MENSAGEM m INNER JOIN EMPRESA e ON e.ID = m.IdEmpresa, PRE_CADASTRO pc INNER JOIN PONTUACAO p ON pc.ID = p.IdPreCadastro  FULL OUTER JOIN CLIENTE c ON pc.Contato = c.Contato " +
        //        "WHERE e.ID = @IdEmpresa AND m.TipoAutomacao = 'Quinze dias'  AND p.Segmentacao = @Segmentacao OR p.SegCustomizado = @SegCustomizado AND  m.TipoAutomacao <> 'Aniversariante' " +
        //        " AND m.TipoAutomacao <> 'Dia da semana '  AND m.TipoAutomacao <> 'Dia do Mes' AND m.TipoAutomacao <> 'Trinta dias' AND m.TipoAutomacao <> 'Ultima fidelizacao' " 
        //        , new { @TipoAutomacao = TipoAutomacao, @Segmentacao = Segmentacao, @SegCustomizado = SegCustomizado, @IdEmpresa = IdEmpresa });
        //}

        public async Task<IEnumerable<ObterAutomacaoTipoUltimaFide>> ObterContatosQueVisitaramAposTrintaDias(string TipoAutomacao, string Segmentacao, string SegCustomizado, int IdEmpresa)
        {
            throw new NotImplementedException();
        }


        public async Task<IEnumerable<Mensagem>> ListaDatasUlimasVisitas(int IdEmpresa, int TempoPorDia, string SegCustomizado)// está query e um complemento da query abaixo
        {
            return await _db.Connection.QueryAsync<Mensagem>("select p.SegCustomizado, dateadd (day, @TempoPorDia, GETDATE()) AS DataUlimaVisita  from EMPRESA e, PRE_CADASTRO pc INNER JOIN PONTUACAO p ON pc.ID = p.IdPreCadastro  WHERE e.ID = @IdEmpresa and p.IdEmpresa = @IdEmpresa  and p.SegCustomizado = @SegCustomizado", new { @IdEmpresa = IdEmpresa, @TempoPorDia = TempoPorDia, @SegCustomizado = SegCustomizado });
        }


        public async Task<IEnumerable<ObterAutomacaoTipoUltimaFide>> ObterContatosQueVisitaramAposUltimaFidelizacao(DateTime DataVisita, string Segmentacao, string SegCustomizado, int IdEmpresa)
        {
            return await _db.Connection.QueryAsync<ObterAutomacaoTipoUltimaFide>("SELECT m.ID, m.TipoAutomacao, m.Conteudo, m.TempoPorDia, m.SegCustomizado, e.NomeFantasia, pc.Contato, c.NomeCompleto FROM MENSAGEM m INNER JOIN EMPRESA e ON e.ID = m.IdEmpresa, PRE_CADASTRO pc INNER JOIN PONTUACAO p ON pc.ID = p.IdPreCadastro FULL OUTER JOIN CLIENTE c ON pc.Contato = c.Contato WHERE  m.Estado = 1 AND e.ID = @IdEmpresa AND   p.SegCustomizado = @SegCustomizado AND   p.IdEmpresa = @IdEmpresa AND  cast(p.DataVisita as date) = cast(@DataVisita as date)", new { @IdEmpresa = IdEmpresa, @SegCustomizado = SegCustomizado, @DataVisita = DataVisita  });
        }
        public async Task<IEnumerable<ListaRetornoDoClienteCampanhaNormal>> ObterListaRetornoDoClienteCampanhaNormal(int Id, int IdEmpresa)
        {
            return await _db.Connection.QueryAsync<ListaRetornoDoClienteCampanhaNormal>("SELECT  c.NomeCompleto, s.Contatos, m.Segmentacao, m.SegCustomizado, MAX(s.DataCompra) AS DataRetorno " +
                  "FROM PRE_CADASTRO, SITUACAO_SMS AS s JOIN MENSAGEM AS m ON s.IdMensagem = m.ID  JOIN PRE_CADASTRO pc ON  s.Contatos = pc.Contato  FULL OUTER JOIN CLIENTE c ON pc.Contato = c.Contato WHERE m.ID = @Id AND m.IdEmpresa = @IdEmpresa" +
                  " GROUP BY  c.NomeCompleto, s.Contatos, m.Segmentacao, m.SegCustomizado", new { @Id = Id, @IdEmpresa = IdEmpresa });
        }

        string[] IAutomacaoMSGRepositorio.ListaTelefones(int IdEmpresa, string SegCustomizado, string Segmentacao)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ObterAutomacaoTipoUltimaFide>> ObterContatosQueVisitaramAposTrintaDias(string Segmentacao, string SegCustomizado, int IdEmpresa)
        {          
             return await _db.Connection.QueryAsync<ObterAutomacaoTipoUltimaFide>("SELECT m.ID, m.TipoAutomacao, m.Conteudo, m.Segmentacao, m.SegCustomizado, e.NomeFantasia, pc.Contato, c.NomeCompleto " +
             "FROM MENSAGEM m INNER JOIN EMPRESA e ON e.ID = m.IdEmpresa, PRE_CADASTRO pc INNER JOIN PONTUACAO p ON pc.ID = p.IdPreCadastro " +
             " FULL OUTER JOIN CLIENTE c ON pc.Contato = c.Contato WHERE e.ID = @IdEmpresa AND p.Segmentacao = @Segmentacao OR p.SegCustomizado = @SegCustomizado AND DataVisita BETWEEN DATEADD(MONTH, -1, CONVERT(date, GETDATE())) AND DATEADD(MONTH, 1 , CONVERT(DATE, GETDATE()))   "
             , new { @IdEmpresa = IdEmpresa, @Segmentacao = Segmentacao, @SegCustomizado = SegCustomizado });
        }

    }
}