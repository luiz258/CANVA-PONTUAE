using PontuaAe.Dominio.FidelidadeContexto.Entidades;
using PontuaAe.Dominio.FidelidadeContexto.ObjetoValor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PontuaAe.Dominio.FidelidadeContexto.Repositorios
{
    public interface IReceitaRepositorio
    {
        Task Salvar(Receita receita);
        Task<int> ObterQtdDiasAusenteClassificacaoOuro(int IdEmpresa, int IdPontuacao, int TempoEmDiasClienteOuro);
        Task<int> ObterQtdDiasAusenteClassificacaoPrata(int IdEmpresa, int IdPontuacao, int TempoEmDiasClientePrata);
        Task<int> ObterQtdDiasAusenteClassificacaoBronze(int IdEmpresa, int IdPontuacao, int TempoEmDiasClienteBronze);

        //Task<int> TempoEmdiasClienteOuro(int IdEmpresa, int IdPontuacao, int QtdVisitasClassificacaoOuro);
        //Task<int> TempoEmDiasClientePrata(int IdEmpresa, int IdPontuacao,);
        //Task<int> TempoEmDiasClienteBronze(int IdEmpresa, int IdPontuacao);
        //Task<int> TempoEmDiasClientePedido(int IdEmpresa, int IdPontuacao);
        //Task<int> TempoEmDiasClienteCasual(int IdEmpresa, int IdPontuacao);
        //Task<int> TempoEmDiasClienteInativo(int IdEmpresa, int IdPontuacao);



        //Consultas Dashboard
        Task<decimal> ObterTicketMedio(int IdEmpresa);
        Task<decimal> ObterTotalVendasMes(int IdEmpresa);
        Task<decimal> ObterReceitaRetidosMes(int IdEmpresa);
        Task<decimal> ObterReceitaRetidosSemana(int IdEmpresa);
        Task<decimal> ObterReceitaRetidosDia(int IdEmpresa);






    }
}
