using Dapper;
using PontuaAe.Dominio.FidelidadeContexto.Consulta.MarketingConsulta;
using PontuaAe.Dominio.FidelidadeContexto.Entidades;
using PontuaAe.Dominio.FidelidadeContexto.Repositorios;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PontuaAe.Infra.Repositorios.RepositorioFidelidade
{
    public class CampanhaMSGRepositorio : ICampanhaMSGRepositorio
    {
        private readonly DbConfig _db;

        public CampanhaMSGRepositorio(DbConfig db)
        {
            _db = db;
        }

        public async Task<DetalheDoResultadoDaCampanha> ObterDetalheDoResultadoDaCampanha(int Id, int IdEmpresa)
        {
            return await _db.Connection.QueryFirstOrDefaultAsync<DetalheDoResultadoDaCampanha>("SELECT r.DataRecebida as DataEnvio,  m.ID, m.Nome, m.Conteudo, m.QtdEnviada,  m.ValorInvestido,  SUM(r.ValorRecebido) as TotalVendas, m.DataEnvio, COUNT(r.DataCompra) AS QtdRetorno FROM MENSAGEM m INNER JOIN  SITUACAO_SMS r ON r.IdEmpresa = m.IdEmpresa  WHERE m.ID=@Id AND m.IdEmpresa =@IdEmpresa AND m.EstadoEnvio NOT IN ('Automatico')  " +
                "GROUP BY m.ID, m.Nome, m.Conteudo, m.QtdEnviada, m.ValorInvestido, m.DataEnvio, r.DataRecebida ", new { @ID = Id, @IdEmpresa = IdEmpresa });
        }


        //public async Task<DetalheDoResultadoDaCampanha> ObtertalheDoResultadoDaCampanha(int Id, int IdEmpresa)
        //{
        //    return await _db.Connection.QueryFirstOrDefaultAsync<DetalheDoResultadoDaCampanha>("SELECT  m.ID, m.Nome, m.Conteudo, m.QtdEnviada, m.DataEnviada, m.ValorInvestido, COUNT(r.DataCompra) AS QtdRetorno, r.TotalVendas FROM MENSAGEM m INNER JOIN  SITUACAO_SMS r ON r.IdEmpresa = m.IdEmpresa  WHERE m.ID=@Id AND NOT m.EstadoEnvio='Automatico'  ", new { @ID = Id });
        //}

        public async Task<IEnumerable<ObterListaCampanhaSMS>> listaCampanha(int IdEmpresa) 
        {
            return await _db.Connection.QueryAsync<ObterListaCampanhaSMS>("SELECT m.ID, m.Nome, m.Segmentacao, m.SegCustomizado,  m.EstadoEnvio, m.DataEnviada   FROM MENSAGEM m  WHERE  m.IdEmpresa=@IdEmpresa  AND m.EstadoEnvio NOT IN ('Automatico') ", new { @IdEmpresa = IdEmpresa }  );
        }
        //public IEnumerable<ObterListaCampanhaSMS> listaCampanhaAgendada(int IdEmpresa)
        //{
        //    return _db.Connection.Query<ObterListaCampanhaSMS>("SELECT m.Nome, m.QtdCliente, m.Segmentacao, m.SegmentoCustomizado, m.Tipocanal, r.DataEnviada, m.EstadoEnvio, m.QtdEnviada, COUNT(r.Estado='Sucesse') As QtdRecebida, m.ValorInvestido, COUNT(r.DataCompra) AS QtdRetorno, r.TotalVendas, FROM MENSAGEM m, INNER JOIN SITUACAO_MENSAGEM r ON s.IdEmpresa = r.IdEmpresa  WHERE s.Tipo=@Tipo AND m.IdEmpresa=@IdEmpresa AND m.EstadoEnvio= 'Agendada' ");
        //}

        public async Task<IEnumerable<ListaContatosPorSegmentacao>> BuscaContatosPorSegmentacao(int IdEmpresa, string Segmentacao)
        {
            return await _db.Connection.QueryAsync<ListaContatosPorSegmentacao>("SELECT pc.Contato  FROM PRE_CADASTRO pc INNER JOIN  PONTUACAO p ON pc.ID = p.IdPreCadastro  WHERE p.IdEmpresa = @IdEmpresa AND p.Segmentacao= @Segmentacao", new { @IdEmpresa = IdEmpresa, @Segmentacao = Segmentacao });
        } 

        public async Task<IEnumerable<ListaContatosPorSegCustomizado>> BuscaContatosPorSegCustomizado(int IdEmpresa, string SegCustomizado)
        {
            return await _db.Connection.QueryAsync<ListaContatosPorSegCustomizado>("SELECT pc.Contato  FROM PRE_CADASTRO pc JOIN  PONTUACAO p ON p.IdPreCadastro = pc.ID   WHERE p.IdEmpresa = @IdEmpresa AND p.SegCustomizado=@SegCustomizado", new { @IdEmpresa = IdEmpresa, @SegCustomizado  = SegCustomizado });
        }

        //public async Task AtualizarEstadoCampanha(Mensagem model)   //averiguar para remover estre 
        //{
        //    await _db.Connection.ExecuteAsync("UPDATE MENSAGEM SET EstadoEnvio WHERE  IdEmpresa=@IdEmpresa AND IdCampanha=@IdCampanha  ", new { @IdEmpresa = model.IdEmpresa, @IdCampanha = model.IdCampanha });
        //}


        public async Task Salvar(Mensagem model)
        {
           await _db.Connection.ExecuteAsync("INSERT INTO MENSAGEM (IdEmpresa, EstadoEnvio, Nome, Segmentacao, SegCustomizado, QtdSelecionado, DataEnvio, HoraEnvio, QtdEnviada, ValorInvestido, Conteudo)VALUES( @IdEmpresa, @EstadoEnvio, @Nome, @Segmentacao, @SegCustomizado, @QtdSelecionado, @DataEnvio, @HoraEnvio, @QtdEnviada, @ValorInvestido, @Conteudo)",
                new
                {
                    @IdEmpresa = model.IdEmpresa,
                    @EstadoEnvio = model.EstadoEnvio,
                    @Nome = model.Nome,
                    @Segmentacao = model.Segmentacao,
                    @SegCustomizado = model.SegCustomizado,
                    @QtdSelecionado = model.QtdSelecionado,
                    @DataEnvio =  model.Agendar.DataEnvio,
                    @HoraEnvio =  model.Agendar.HoraEnvio,
                    @QtdEnviada = model.QtdEnviada,
                    @ValorInvestido = model.ValorInvestido,
                    @Conteudo = model.Conteudo
                
                });
        }

        public async Task<IEnumerable<Mensagem>> ListaMensagem()
        {
            return await _db.Connection.QueryAsync<Mensagem>("SELECT IdCampanha, IdEmpresa FROM MENSAGEM");
        }

        public async Task<int> ObterTotalCreditoSMSdaEmpresa(int IdEmpresa)
        {
            return await _db.Connection.ExecuteScalarAsync<int>("SELECT Saldo FROM CONTA_SMS  WHERE  IdEmpresa=@IdEmpresa", new { @IdEmpresa = IdEmpresa });
        }

        public async Task<IEnumerable<ListaRetornoDoClienteCampanhaNormal>> ObterListaRetornoDoClienteCampanhaNormal(int Id, int IdEmpresa)
        {
            return await _db.Connection.QueryAsync<ListaRetornoDoClienteCampanhaNormal>("SELECT  c.NomeCompleto, s.Contatos, m.Segmentacao, m.SegCustomizado, MAX(s.DataCompra) AS DataRetorno " +
                "FROM PRE_CADASTRO, SITUACAO_SMS AS s JOIN MENSAGEM AS m ON s.IdMensagem = m.ID  JOIN PRE_CADASTRO pc ON  s.Contatos = pc.Contato  FULL OUTER JOIN CLIENTE c ON pc.Contato = c.Contato WHERE m.ID = @Id AND m.IdEmpresa = @IdEmpresa" +
                " GROUP BY  c.NomeCompleto, s.Contatos, m.Segmentacao, m.SegCustomizado", new { @Id = Id, @IdEmpresa = IdEmpresa });
        }

        public async Task<int> ObterID(int IdEmpresa)
        {
            return await _db.Connection.ExecuteScalarAsync<int>("SELECT TOP 1 ID FROM MENSAGEM WHERE IdEmpresa = @IdEmpresa ORDER BY ID DESC ", new {@IdEmpresa = IdEmpresa });
        }

        public async Task SalvarCodigoDoSMS(int Codigo, int IdEmpresa, int IdMensagem)
        {
           await _db.Connection.ExecuteAsync("INSERT INTO CODIGO_SMS (Codigo, IdEmpresa, IdMensagem) VALUES (@Codigo, @IdEmpresa, @IdMensagem)", new {@Codigo = Codigo, @IdEmpresa = IdEmpresa, IdMensagem = IdMensagem });
        }

        public Task Editar(Mensagem model)
        {
            throw new NotImplementedException();
        }

        Task IMensagem<Mensagem>.Desativar(int IdEmpresa, int ID)
        {
            throw new NotImplementedException();
        }

        Task IMensagem<Mensagem>.Deletar(int IdEmpresa, int ID)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<int>> listaDeCodigoSMS(int IdCampanha)
        {
            return await _db.Connection.QueryAsync<int>("SELECT ID FROM CODIGO_SMS WHERE IdCampanha = @IdCampanha", new { @IdCampanha = IdCampanha });
        }
    }
}
