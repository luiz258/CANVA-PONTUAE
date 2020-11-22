using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using PontuaAe.Dominio.FidelidadeContexto.Repositorios;
using PontuaAe.Dominio.FidelidadeContexto.Comandos.PremioComandos.Entradas;
using PontuaAe.Dominio.FidelidadeContexto.Comandos.PremioComandos.Manipulador;
using PontuaAe.Dominio.FidelidadeContexto.Comandos.PremioComandos.Resultados;
using PontuaAe.Dominio.FidelidadeContexto.Consulta.PremiosConsulta;
using PontuaAe.Compartilhado.Comandos;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using PontuaAe.Dominio.FidelidadeContexto.Entidades;
using System.ComponentModel;
using System;
using System.Threading.Tasks;

namespace PontuaAe.Api.Controllers
{

    [ApiController]
    [Route("v1/Premio")]
    [AllowAnonymous]
    public class PremiacaoController: Controller
    {
        private readonly IPremioRepositorio _repPremio;
        private readonly IPreCadastroRepositorio _repPreCadastro; 
        private readonly PremioManipulador _manipulador;

        public PremiacaoController(IPremioRepositorio repPremio, IPreCadastroRepositorio repPreCadastro, PremioManipulador manipulador)
        {
            _repPremio = repPremio;
            _repPreCadastro = repPreCadastro;
            _manipulador = manipulador;
        }


        [HttpPost]
        [Route("v1/post")]
        //[Authorize(Policy = "Admin")]
        public async Task<IComandoResultado> PostAsync([FromBody]CadastraPremioComando comando)
        {
            var resultado = (ComandoResultado) await _manipulador.ManipularAsync(comando);
            return resultado;
        }


        [HttpPost]
        [Route("v1/deletar")]
        //[Authorize(Policy = "Admin")]
        public async Task<IComandoResultado> DeletarAsync([FromBody]DeletarPremioComando comando)
        {
           
            var deletar = (ComandoResultado)await _manipulador.ManipularAsync(comando);
            return deletar;
        }


        [HttpPut]
        [Route("v1/put")]
        [AllowAnonymous]
        public async Task<IComandoResultado> UpdateAsync([FromBody] EditarPremioComando comando)
        {
            var Editar = (ComandoResultado)await _manipulador.ManipularAsync(comando);
            return Editar;
        }


        [HttpGet]
        [Route("v1/lista/{idEmpresa}")]
        [AllowAnonymous]
        public async Task<IEnumerable<ListarPremiosConsulta>> ListaPremios(int idEmpresa)
        {
            return await _repPremio.listaPremios(idEmpresa);
        }



        [HttpGet]
        [Route("v1/detalhe/{Id}/{idEmpresa}")]
        [AllowAnonymous]
        public async Task<ObterDetalhePremioConsulta> Detalhe(int Id, int idEmpresa)
        {
          
                return await _repPremio.ObterDetalhePremio(Id, idEmpresa);
        
         
        }



        [HttpGet]
        [Route("v1/lista/{idEmpresa}/{contato}")]
        public async Task<IEnumerable<ListarPremiosPorClienteConsulta>> ListaPremiosPorCliente(int idEmpresa, string contato)
        {
            
            int idCliente = await ObterId(contato);
            return  await _repPremio.listaPremiosPorCliente(idEmpresa, idCliente);
        }


        protected async Task<int> ObterId(string contato)
        {
             int idPreCadastro =  await _repPreCadastro.ObterIdPreCadastro(contato);
             return idPreCadastro;
        }

    }
}
