using Microsoft.AspNetCore.Authorization;
using PontuaAe.Compartilhado.Comandos;
using PontuaAe.Dominio.FidelidadeContexto.Comandos.AutenticaComandos.Resultados;
using PontuaAe.Dominio.FidelidadeContexto.Repositorios;
using PontuaAe.Dominio.FidelidadeContexto.Comandos.FuncionarioComandos.Manipulador;
using PontuaAe.Dominio.FidelidadeContexto.Comandos.FuncionarioComandos.Entradas;
using Microsoft.AspNetCore.Mvc;
using PontuaAe.Dominio.FidelidadeContexto.Comandos.FuncionarioComandos.Resultados;
using System.Collections.Generic;
using PontuaAe.Dominio.FidelidadeContexto.Consulta.FuncionarioConsulta;
using System.Threading.Tasks;

namespace PontuaAe.Api.Controllers.Account
{


    [ApiController]
    [Route("v1/funcionario")]
    [AllowAnonymous] 
    public class FuncionarioController: Controller
    {
        private readonly FuncionarioComandoManipulador _manipulador;
        private readonly IFuncionarioRepositorio _repFuncionario;

        public FuncionarioController(FuncionarioComandoManipulador manipulador, IFuncionarioRepositorio repFuncionario)
        {
            _manipulador = manipulador;
            _repFuncionario = repFuncionario;
        }


        [HttpPost]
        [Route("v1/post")]
        //[Authorize(Policy = "Admin")]
        public async Task<IComandoResultado> CreateFuncionario([FromBody] CadastrarFuncionarioComando comando)
        {

            return  (ComandoFuncionarioResultado)await _manipulador.ManipularAsync(comando);
          
        }

        //[HttpPut]
        //[Route("v1/updateFuncionario")]
        ////[Authorize(Policy = "Admin")]
        //public async Task<IComandoResultado> EditFuncionarioAsync([FromBody] EditarFuncionarioComando comando)
        //{

        //    var resultado = (ComandoFuncionarioResultado) await _manipulador.ManipularAsync(comando);
        //    return resultado;

        //}


        [HttpGet]
        [Route("v1/lista/{idEmpresa}")]
        //[Authorize(Policy = "Admin")]
        public async Task<IEnumerable<ListaFuncionarioConsulta>> ListaFuncionarioAsync(int IdEmpresa)
        {
            return await _repFuncionario.ListaFuncionario(IdEmpresa);
        }

        [HttpGet]
        [Route("v1/detalhe/{id}/{idEmpresa}")]
        // [Authorize(Policy ="Admin")]
        public async Task<ObterDetalheFuncionarioConsulta> Detalhe(int Id, int idEmpresa)
        {
            return await _repFuncionario.ObterDetalheFuncionario(Id, idEmpresa);
        }

        //[HttpPost]
        //[Route("v1/Deletar")]
        ////[Authorize(Policy = "Admin")]
        //public async Task<IComandoResultado> Deletar([FromBody] RemoverColaboradorComando d )
        //{

        //    return  (ComandoFuncionarioResultado)await _manipulador.ManipularAsync(d);

        //}
    }

}
