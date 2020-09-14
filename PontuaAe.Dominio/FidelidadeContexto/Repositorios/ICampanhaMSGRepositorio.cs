using PontuaAe.Dominio.FidelidadeContexto.Consulta.MarketingConsulta;
using PontuaAe.Dominio.FidelidadeContexto.Entidades;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PontuaAe.Dominio.FidelidadeContexto.Repositorios
{
    public interface ICampanhaMSGRepositorio : IMensagem<Mensagem>
    {
        Task<IEnumerable<ObterListaCampanhaSMS>> listaCampanha(int IdEmpresa);
        //IEnumerable<ObterListaCampanhaSMS> listaCampanhaAgendada(int IdEmpresa);
        Task<IEnumerable<ListaRetornoDoClienteCampanhaNormal>> ObterListaRetornoDoClienteCampanhaNormal(int Id, int IdEmpresa);
        Task<IEnumerable<ListaContatosPorSegmentacao>> BuscaContatosPorSegmentacao(int IdEmpresa, string Segmentacao); 
        Task<IEnumerable<ListaContatosPorSegCustomizado>> BuscaContatosPorSegCustomizado(int IdEmpresa, string SegCustomizado);
        //Task AtualizarEstadoCampanha(Mensagem model);  // editar o atributo EstadoEnvio
        Task<DetalheDoResultadoDaCampanha> ObterDetalheDoResultadoDaCampanha(int Id, int IdEmpresa);
        Task<int> ObterID(int IdEmpresa);

        Task<int> ObterTotalCreditoSMSdaEmpresa(int IdEmpresa);
        Task SalvarCodigoDoSMS(int Codigo, int IdEmpresa, int IdMensagem);
        Task<IEnumerable<int>> listaDeCodigoSMS(int IdCampanha);




    }
}
