using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PontuaAe.Compartilhado.Comandos;
using PontuaAe.Dominio.FidelidadeContexto.Comandos.AutomacaoComandos.Entradas;
using PontuaAe.Dominio.FidelidadeContexto.Comandos.AutomacaoComandos.Manipulador;
using PontuaAe.Dominio.FidelidadeContexto.Comandos.AutomacaoComandos.Resultados;
using PontuaAe.Dominio.FidelidadeContexto.Comandos.ClienteComandos.Manipulador;
using PontuaAe.Dominio.FidelidadeContexto.Consulta.MarketingConsulta;
using PontuaAe.Dominio.FidelidadeContexto.Repositorios;

namespace PontuaAe.Api.Controllers.Marketing
{
    [ApiController]
    [Route("v1/automacao")]
    [AllowAnonymous]
    public class AutomacaoController : Controller
    {
        private readonly ICampanhaMSGRepositorio _repCampanha;
        private readonly IAutomacaoMSGRepositorio _repAutomacao;
        private readonly AutomacaoManipulador _manipulador;
        private readonly ClienteManipulador _manipuladorCliente;


        public AutomacaoController(AutomacaoManipulador manipulador, ClienteManipulador manipuladorCliente, IAutomacaoMSGRepositorio repAutomacao, ICampanhaMSGRepositorio repCampanha)
        {
            _manipuladorCliente = manipuladorCliente;
            _manipulador = manipulador;
            _repAutomacao = repAutomacao;
            _repCampanha = repCampanha;
        }

        [HttpPost]
        [Route("v1/post")]
        //[Authorize(Policy = "Admin")]
        //[Authorize(Policy = "Funcionario")]

        public async Task<IComandoResultado> PostAsync([FromBody]AddAutomacaoComando comando)
        {
            var resultado = (ComandoResultado)await _manipulador.ManipularAsync(comando);
            return resultado;
        }

        [HttpPut]
        [Route("v1/put")]
        //[Authorize(Policy = "Admin, Funcionario")]
        public async Task<IComandoResultado> PutAsync([FromBody]EditarAutomacaoComando comando)
        {
            try
            {
                var resultado = (ComandoResultado)await _manipulador.ManipularAsync(comando);
                return resultado;
            }
            catch (Exception e)
            {

                throw;
            }
           
        }


        [HttpPut]
        [Route("v1/Desativar/{id}/{idEmpresa}")]
        //[Authorize(Policy = "Admin, Funcionario")]
        public async Task<IComandoResultado> DesativarAutomacao(int id, int idEmpresa)
        {

            var objeto = new DesativarAutomacao { Id = id, IdEmpresa = idEmpresa };
            var resultado = (ComandoResultado)await _manipulador.ManipularAsync(objeto);
            return resultado;
        }

        [HttpGet]
        [Route("v1/Lista/{idEmpresa}/{estado}")]
        public async Task<IEnumerable<ObterListaAutomacao>> ListaAutomacao(int idEmpresa, int estado)
        {  
           
            return await _repAutomacao.listaAutomacao(idEmpresa, estado);
        }


        [HttpGet]
        [Route("v1/relatorio/{id}/{idEmpresa}")]
        public async Task<DetalheDoResultadoDaCampanhaAutomatica> DetalheDoRelatorioDoRetornoCampanha(int id, int idEmpresa)
        {

            return await _repAutomacao.ObterDetalheDoResultadoDaCampanha(id, idEmpresa);
        }


        [HttpGet]
        [Route("v1/Detalhe/{id}/{idEmpresa}")]
        public async Task<ObterAutomacaoPorId> ObterDetalheAutomacao(int id, int idEmpresa)
        {

            return await _repAutomacao.ObterDetalheDaAutomacao(id, idEmpresa);
        }


        [HttpGet]
        [Route("v1/{id}/{idEmpresa}")]
        public async Task<IEnumerable<ListaRetornoDoClienteCampanhaNormal>> ListaRetornoDosClientesAposRecebeCampanhaAutomatica(int id, int idEmpresa)
        {

            return await _repAutomacao.ObterListaRetornoDoClienteCampanhaNormal(id, idEmpresa);


        }




        [HttpDelete]
        [Route("v1/{id}/{idEmpresa}")]
        //[Authorize(Policy = "Admin")]
        public async Task<IComandoResultado> DeletarAsync(int Id, int IdEmpresa)
        {
            RemoverAutomacaoComando delete = new RemoverAutomacaoComando(Id, IdEmpresa);
            var deletar = (ComandoResultado)await _manipulador.ManipularAsync(delete);
            return deletar;
        }

        //Metodos abaixo   serão usados temporariamente para executar Jobs

        [HttpPost]
        [Route("v1/jobAutomacaoDiaDaSemana")]
        //[Authorize(Policy = "Admin")]
        //[Authorize(Policy = "Funcionario")]
        public async Task<IActionResult> jobAutomacaoDiaDaSemana()
        {
            try
            {
                await _manipulador.AutomacaoTipoDiaDaSemana();
                return Ok("OK");
            }
            catch (System.Exception)
            {

                throw;
            }
           
        }

        [HttpPost]
        [Route("v1/jobAutomacaoAniversariante")]
        //[Authorize(Policy = "Admin")]
        //[Authorize(Policy = "Funcionario")]
        public async Task<IActionResult> jobAutomacaoAniversariante()
        {
            try
            {
                await _manipulador.AutomacaoTipoAniversarianteAsync();
                return Ok("OK");
            }
            catch (System.Exception)
            {

                throw;
            }

        }


        [HttpPost]
        [Route("v1/jobAutomacaoQuinzeDias")]
        //[Authorize(Policy = "Admin")]
        //[Authorize(Policy = "Funcionario")]
        public async Task<IActionResult> jobAutomacaoQuinzeDias()
        {
            try
            {
                await _manipulador.AutomacaoClientesInativoQuinzeDias();
                return Ok("OK");
            }
            catch (System.Exception)
            {

                throw;
            }

        }


        [HttpPost]
        [Route("v1/jobAutomacaoTrintaDias")]
        //[Authorize(Policy = "Admin")]
        //[Authorize(Policy = "Funcionario")]
        public async Task<IActionResult> jobAutomacaoTrintaDias()
        {
            try
            {
                await _manipulador.AutomacaoTipoTrintaDias();
                return Ok("OK");
            }
            catch (System.Exception)
            {

                throw;
            }

        }

        [HttpPost]
        [Route("v1/ClassificaTipoClienteJob")]
        public async Task<IActionResult> ClassificaTipoClienteJob()
        {
            try
            {
                await _manipuladorCliente.ClassificaRecorrencia();
                return Ok("OK");
            }
            catch (Exception e)
            {

                return NotFound(e);
            }
        }

            //}
            //[HttpGet]
            //[Route("v1/ListaContato/{segCustomizado}/{idEmpresa}")]
            //[AllowAnonymous]
            //public async Task<IEnumerable<ListaContatosPorSegCustomizado>> BuscaContatosPorSegCustomizado(int idEmpresa)
            //{

            //    return await _repCampanha.BuscaContatosPorSegCustomizado(idEmpresa);  


            //}


        }
}