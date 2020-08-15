using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PontuaAe.Compartilhado.Comandos;
using PontuaAe.Dominio.FidelidadeContexto.Comandos.MarketingComandos.Entradas;
using PontuaAe.Dominio.FidelidadeContexto.Comandos.MarketingComandos.Manipulador;
using PontuaAe.Dominio.FidelidadeContexto.Comandos.MarketingComandos.Resultado;
using PontuaAe.Dominio.FidelidadeContexto.Consulta.MarketingConsulta;
using PontuaAe.Dominio.FidelidadeContexto.Repositorios;

namespace PontuaAe.Api.Controllers.Marketing
{

    [ApiController]
    [Route("v1/campanha")]
    [AllowAnonymous]
    public class CampanhaController : Controller
    {
        private readonly ICampanhaMSGRepositorio _repCampanha;
        private readonly CampanhaManipulado _manipulador;
        public CampanhaController(CampanhaManipulado manipulador, ICampanhaMSGRepositorio repCampanha)
        {
            _manipulador = manipulador;
            _repCampanha = repCampanha;
            
        }

        [HttpPost]
        [Route("v1/post")]
        //[Authorize(Policy = "Admin")]
        //[Authorize(Policy = "Funcionario")]
        public async Task<IComandoResultado> PostAsync([FromBody]AddCampanhaComando comando)
        {
    
              var resultado = (ComandoResultado) await _manipulador.ManipularAsync(comando);
              return resultado;

        }


        [HttpGet]
        [Route("v1/lista/{idEmpresa}")]
        [AllowAnonymous]
        public async Task<IEnumerable<ObterListaCampanhaSMS>> ObterListaCampanha(int idEmpresa)
        {
            //executar metodo para buscar e atualiza situação dos envios de sms,
            
            return await _repCampanha.listaCampanha(idEmpresa);
        }


        [HttpGet]
        [Route("v1/relatorio/{id}/{idEmpresa}")]
        public async Task<DetalheDoResultadoDaCampanha> DetalheDoRelatorioDoRetornoCampanha(int id, int idEmpresa)
        {
            
            return await _repCampanha.ObterDetalheDoResultadoDaCampanha(id, idEmpresa);
        }


        
        [HttpGet]
        [Route("v1/ListaRetornoContatos/{id}/{idEmpresa}")]
        //[ResponseCache(Duration = 15)]  // durante 15 minutos não vai have requisição 
        public Task<IEnumerable<ListaRetornoDoClienteCampanhaNormal>> ObterListaRetornoDoCliente(int Id, int IdEmpresa)
        {
            //testa este metodo
            return  _repCampanha.ObterListaRetornoDoClienteCampanhaNormal(Id,  IdEmpresa);
        }

        ////[HttpGet]
        ////[Route("v1/contatos/{idEmpresa}/{segmentacao}")]
        ////public Task<IEnumerable<ListaContatosPorSegmentacao>> BuscaContatosPorSegmentacao(int idEmpresa, string segmentacao)
        ////{

        ////    return _repCampanha.BuscaContatosPorSegmentacao(idEmpresa, segmentacao);

        ////}
                             
                        
        [HttpGet]          
        [Route("v1/ListaContato/{idEmpresa}/{segCustomizado}")]       
        [AllowAnonymous]                             
        public async Task<IEnumerable<ListaContatosPorSegCustomizado>> ObterContatosPorSegCustomizado( int idEmpresa, string segCustomizado )
        {

            return  await _repCampanha.BuscaContatosPorSegCustomizado(idEmpresa, segCustomizado);
            

        }


        [HttpGet]
        [Route("v1/creditoSMS/{idEmpresa}")]
        public Task<int> ObterCreditoSMS(int idEmpresa)
        {

            return _repCampanha.ObterTotalCreditoSMSdaEmpresa(idEmpresa);
        }

    }
}