using PontuaAe.Dominio.FidelidadeContexto.Consulta.MarketingConsulta;
using PontuaAe.Dominio.FidelidadeContexto.Entidades;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PontuaAe.Dominio.FidelidadeContexto.Repositorios
{
    public interface IAutomacaoMSGRepositorio : IMensagem<Mensagem>
    {

        //obtem as automações  configuradas 
     IEnumerable<ObterAutomacaoTipoAniversario> ObterDadosAutomacaoAniversario(string TipoAutomacao, string Segmentacao, string SegCustomizado, int ID);
     IEnumerable<ObterAutomacaoTipoDiaSemana> ObterDadosAutomacaoSemana(string TipoAutomacao, string Segmentacao, string SegCustomizado, int IdEmpresa);
     Task<IEnumerable<ObterAutomacaoTipoDiaMes>> ObterDadosAutomacaoMes(string AutomacaoMes, string Segmentacao, string SegCustomizado, int IdEmpresa);
     Task<IEnumerable<ObterAutomacaoTipoUltimaFide>> ObterContatosQueVisitaramAposQuinzeDias(string TipoAutomacao, string Segmentacao, string SegCustomizado, int IdEmpresa);
     Task<IEnumerable<ObterAutomacaoTipoUltimaFide>> ObterContatosQueVisitaramAposTrintaDias(string TipoAutomacao, string Segmentacao, string SegCustomizado, int IdEmpresa);
     Task<IEnumerable<ObterAutomacaoTipoUltimaFide>> ObterContatosQueVisitaramAposUltimaFidelizacao(string TipoAutomacao, string Segmentacao, string SegCustomizado, int IdEmpresa);
     Task<DetalheDoResultadoDaCampanhaAutomatica> ObterDetalheDoResultadoDaCampanha(int ID, int IdEmpresa);
     Task<IEnumerable<ListaRetornoDoClienteCampanhaNormal>> ObterListaRetornoDoClienteCampanhaNormal(int Id, int IdEmpresa);
     Task<IEnumerable<ObterListaAutomacao>> listaAutomacao(int IdEmpresa, int Estado);

     //obtem lista de mensagem de todas as empresas
     IEnumerable<Mensagem> ListaTipoAutomacao();
     Task atualizarDadosMensagem(Mensagem model);
     string[] ListaTelefones(int IdEmpresa, string SegCustomizado, string Segmentacao);
     //Task AtualizarEstadoCampanha(Mensagem model);  averigua se pode excluir
     Task<IEnumerable<Mensagem>> ListaMensagem();
    

    }
}
