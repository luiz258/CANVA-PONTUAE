using Dapper;
using PontuaAe.Dominio.FidelidadeContexto.Entidades;
using PontuaAe.Dominio.FidelidadeContexto.Repositorios;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PontuaAe.Infra.Repositorios.RepositorioFidelidade
{
    public class SituacaoRepositorio : ISituacaoRepositorio
    {
        private readonly DbConfig _db;
        public SituacaoRepositorio(DbConfig db)
        {
            _db = db;
        }

        //public async Task atualizarSituacaoSMS(SituacaoSMS model)
        //{
        //    await _db.Connection.ExecuteAsync("UPDATE SITUACAO_SMS SET DataCompra=@DataCompra, TotalVendas=@TotalVendas WHERE IdEmpresa=@IdEmpresa AND ID=@ID ",
        //    new
        //    {
        //        @DataCompra = model.DataCompra,
        //        @ValorRecebido = model.@ValorRecebido,
        //        @IdEmpresa = model.IdEmpresa,
        //        @IdMensagem = model.IdMensagem,
        //        @IdSMS = model.IdSMS,
        //        @ID = model.ID,

        //    });
        //}

 

        public async Task<IEnumerable<SituacaoSMS>> ListaSituacaoSMS(int IdEmpresa)
        {
            return await _db.Connection.QueryAsync<SituacaoSMS>("SELECT ID, IdEmpresa, IdMensagem, IdSMS, DataRecebida, Contatos FROM SITUACAO_SMS WHERE IdEmpresa=@IdEmpresa ", new {@IdEmpresa = IdEmpresa });
        }

        public async Task<SituacaoSMS> obterIdAsync(int IdEmpresa, int IdCampanha)
        {
            return await _db.Connection.QueryFirstAsync<SituacaoSMS>("SELECT ID FROM SITUACAO_SMS WHERE IdEmpresa=@IdEmpresa, IdMensagem=@IdCampanha", 
                new { @IdEmpresa = IdEmpresa, @IdCampanha = IdCampanha});
        }

        //public async Task<int> ObterQtdRetorno(int IdEmpresa, int ID)
        //{
        //    return await _db.Connection.ExecuteScalarAsync<int>("SELECT COUNT(DataCompra) FROM SITUACAO_SMS WHERE IdEmpresa=@IdEmpresa AND ID=@ID", new { @IdEmpresa = IdEmpresa, @ID = ID });
        //}

 


        public async Task SalvarSituacao(SituacaoSMS model)
        {
            await _db.Connection.ExecuteAsync("INSERT INTO SITUACAO_SMS ( DataCompra, Contatos, DataRecebida, ValorRecebido, IdEmpresa, IdMensagem, IdSMS) VALUES ( @DataCompra, @Contatos, @DataRecebida, @ValorRecebido, @IdEmpresa, @IdMensagem, @IdSMS)",
                 new
                 {

                     @DataCompra = model.DataCompra,
                     @Contatos = model.Contatos,
                     @DataRecebida = model.DataRecebida,
                     @ValorRecebido = model.ValorRecebido,
                     @IdEmpresa = model.IdEmpresa,
                     @IdMensagem = model.IdMensagem,
                     @IdSMS = model.IdSMS
                 });

        }
    }
}
