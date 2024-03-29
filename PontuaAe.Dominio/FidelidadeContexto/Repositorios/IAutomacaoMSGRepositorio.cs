﻿using PontuaAe.Dominio.FidelidadeContexto.Consulta.MarketingConsulta;
using PontuaAe.Dominio.FidelidadeContexto.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace PontuaAe.Dominio.FidelidadeContexto.Repositorios
{
    public interface IAutomacaoMSGRepositorio : IMensagem<Mensagem>
    {

        //obtem as automações  configuradas 
     Task<IEnumerable<ObterAutomacaoTipoAniversario>> ObterDadosAutomacaoAniversario(string Segmentacao, string SegCustomizado, int ID);
     Task<IEnumerable<ObterAutomacaoTipoDiaSemana>> ObterDadosAutomacaoSemana(string TipoAutomacao, string Segmentacao, string SegCustomizado, int IdEmpresa);
     Task<IEnumerable<ObterAutomacaoTipoDiaMes>> ObterDadosAutomacaoMes(string AutomacaoMes, string Segmentacao, string SegCustomizado, int IdEmpresa);
     Task<IEnumerable<ObterAutomacaoTipoUltimaFide>> ObterContatosQueVisitaramAposTrintaDias(string Segmentacao, string SegCustomizado, int IdEmpresa);
     Task<IEnumerable<ObterAutomacaoTipoUltimaFide>> ObterContatosQueVisitaramAposUltimaFidelizacao(DateTime DataVisita, string Segmentacao, string SegCustomizado, int IdEmpresa);
     Task<DetalheDoResultadoDaCampanhaAutomatica> ObterDetalheDoResultadoDaCampanha(int ID, int IdEmpresa);
     Task<ObterAutomacaoPorId> ObterDetalheDaAutomacao (int ID, int IdEmpresa); 
     Task<IEnumerable<ListaRetornoDoClienteCampanhaNormal>> ObterListaRetornoDoClienteCampanhaNormal(int Id, int IdEmpresa);
     Task<IEnumerable<ObterListaAutomacao>> listaAutomacao(int IdEmpresa, int Estado);
     //obtem lista de mensagem de todas as empresas
     Task<IEnumerable<Mensagem>> ListaTipoAutomacao();
     Task<IEnumerable<Mensagem>> ListaDatasUlimasVisitas(int IdEmpresa, int TempoPorDia, string SegCustomizado);
     Task atualizarDadosMensagem(Mensagem model);
     string[] ListaTelefones(int IdEmpresa, string SegCustomizado, string Segmentacao);
     //Task AtualizarEstadoCampanha(Mensagem model);  averigua se pode excluir
     Task<IEnumerable<Mensagem>> ListaMensagem();
    

    }
}
