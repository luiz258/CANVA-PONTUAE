using Microsoft.AspNetCore.Mvc;
using PontuaAe.Compartilhado.Comandos;
using PontuaAe.Dominio.FidelidadeContexto.Consulta.ConfigPontuacaoConsulta;
using PontuaAe.Dominio.FidelidadeContexto.Repositorios;
using Microsoft.AspNetCore.Authorization;
using PontuaAe.Dominio.FidelidadeContexto.Comandos.EmpresaComandos.Manipulador;
using PontuaAe.Dominio.FidelidadeContexto.Comandos.EmpresaComandos.Entradas;
using PontuaAe.Dominio.FidelidadeContexto.Comandos.EmpresaComandos.Resultados;
using System.Threading.Tasks;

namespace PontuaAe.Api.Controllers
{

    [ApiController]
    [Route("v1/ajuste")]
    [AllowAnonymous]
    public class ConfigPontuacaoController : Controller
    {
        private readonly IConfigPontosRepositorio _repConfigPontos;
        private readonly EmpresaManipulador _manipulador;

        public ConfigPontuacaoController(IConfigPontosRepositorio repConfigPontos, EmpresaManipulador manipulador)
        {

            _repConfigPontos = repConfigPontos;
            _manipulador = manipulador;

        }



        [HttpPost]
        [Route("v1/post")]
        //[Authorize(Policy ="Admin")]
        [AllowAnonymous]
        public async Task<IComandoResultado> CriarProgramaFidelidadeComandoAsync([FromBody]AddRegraProgramaFidelidadeComando comando)
        {
            var resultado = (ComandoEmpresaResultado)await _manipulador.ManipularAsync(comando);
            return resultado;

        }


        [HttpPut] 
        [Route("v1/put")]
        //[Authorize(Policy ="Admin")]
        public async Task<IComandoResultado> EditarProgramaFidelidadeComandoAsync([FromBody]EditarRegraPontuacaoComando comando)
        {
            var resultado = (ComandoEmpresaResultado)await _manipulador.ManipularAsync(comando);
            return resultado;
        
        }

        [HttpGet]
        [Route("v1/detalhe/{idEmpresa}")]
        //[Authorize(Policy ="Admin")]
        public async Task<ObterDetalheConfigPontuacao> ObterRegradePontosAsync(int IdEmpresa)
        {
            return await _repConfigPontos.ObterDetalheConfigPontuacao(IdEmpresa);
        }
    }
}
