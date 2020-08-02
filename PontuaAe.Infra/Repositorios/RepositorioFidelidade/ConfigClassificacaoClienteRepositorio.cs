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

        public Task Editar(ConfigClassificacaoCliente model)
        {
            throw new NotImplementedException();
        }

        public Task<ConfigClassificacaoCliente> ObterConfig(int IdEmpresa)
        {
            throw new NotImplementedException();
        }

        public async Task Salvar(ConfigClassificacaoCliente model)
        {
           await  _db.Connection.ExecuteAsync("INSERT INTO CONFIG_CLASSIFICACAO_CLIENTE (QtdVisitasClassificacaoOuro, QtdVisitasClassificacaoPrata, QtdVisitasClassificacaoBronze, QtdVisitaClassificacaoAtivo, TempoEmDiasClienteOuro, TempoEmDiasClientePrata , TempoEmDiasClienteBronze, TempoEmDiasClientePedido, TempoEmDiasClienteInativo )VALUES (@QtdVisitasClassificacaoOuro, @QtdVisitasClassificacaoPrata, @QtdVisitasClassificacaoBronze, @QtdVisitaClassificacaoAtivo, @TempoEmDiasClienteOuro, @TempoEmDiasClientePrata , @TempoEmDiasClienteBronze, @TempoEmDiasClientePedido, @TempoEmDiasClienteInativo)", new 
            {
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
