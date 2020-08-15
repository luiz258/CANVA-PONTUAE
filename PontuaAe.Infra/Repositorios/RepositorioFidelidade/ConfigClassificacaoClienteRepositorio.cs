using Dapper;
using PontuaAe.Compartilhado.DbConfig;
using PontuaAe.Dominio.FidelidadeContexto.Entidades;
using PontuaAe.Dominio.FidelidadeContexto.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PontuaAe.Infra.Repositorios.RepositorioFidelidade
{
    public class ConfigClassificacaoClienteRepositorio : IConfigClassificacaoClienteRepositorio
    {
        private readonly DbConfig _db;

        public ConfigClassificacaoClienteRepositorio(DbConfig db)
        {
            _db = db;
        }

        public Task Editar(TemplateClassificacaoCliente model)
        {
            throw new NotImplementedException();
        }

        public async Task<ConsultaTemplateClassificacaoCliente> ObterConfig(int IdEmpresa)
        {
            return await _db.Connection
               .QueryFirstOrDefaultAsync<ConsultaTemplateClassificacaoCliente>("SELECT * FROM CONFIG_CLASSIFICACAO_CLIENTE WHERE IdEmpresa=@IdEmpresa", new
               { @IdEmpresa = IdEmpresa });
        }

        public async Task Salvar(TemplateClassificacaoCliente model)
        {
           await  _db.Connection.ExecuteAsync("INSERT INTO CONFIG_CLASSIFICACAO_CLIENTE (IdEmpresa, QtdVisitasClassificacaoOuro, QtdVisitasClassificacaoPrata, QtdVisitasClassificacaoBronze, QtdVisitaClassificacaoAtivo, TempoEmDiasClienteOuro, TempoEmDiasClientePrata , TempoEmDiasClienteBronze, TempoEmDiasClientePedido, TempoEmDiasClienteInativo )VALUES (@IdEmpresa, @QtdVisitasClassificacaoOuro, @QtdVisitasClassificacaoPrata, @QtdVisitasClassificacaoBronze, @QtdVisitaClassificacaoAtivo, @TempoEmDiasClienteOuro, @TempoEmDiasClientePrata , @TempoEmDiasClienteBronze, @TempoEmDiasClientePedido, @TempoEmDiasClienteInativo)", new 
            {
                @IdEmpresa = model.IdEmpresa,
                @QtdVisitasClassificacaoOuro = model.QtdVisitasClassificacaoOuro,
                @QtdVisitasClassificacaoPrata = model.QtdVisitasClassificacaoPrata,
                @QtdVisitasClassificacaoBronze = model.QtdVisitasClassificacaoBronze ,
                @QtdVisitaClassificacaoAtivo = model.QtdVisitaClassificacaoAtivo,
                
                @TempoEmDiasClienteOuro = model.TempoEmDiasClienteOuro,
                @TempoEmDiasClientePrata = model.TempoEmDiasClientePrata,
                @TempoEmDiasClienteBronze = model.TempoEmDiasClienteBronze ,
                @TempoEmDiasClientePedido = model.TempoEmDiasClientePedido ,
                @TempoEmDiasClienteInativo = model.TempoEmDiasClienteInativo 
            });
    }
    }
}
