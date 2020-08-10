using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PontuaAe.Compartilhado.Comandos;
using PontuaAe.Dominio.FidelidadeContexto.Comandos.PontuacaoComandos.Entradas;
using PontuaAe.Dominio.FidelidadeContexto.Comandos.PontuacaoComandos.Manipulador;
using PontuaAe.Dominio.FidelidadeContexto.Comandos.PontuacaoComandos.Resultados;
using PontuaAe.Dominio.FidelidadeContexto.Repositorios;
using System;
using System.Threading.Tasks;

namespace PontuaAe.Api.Controllers
{

    [ApiController]
    [Route("v1/ponto")]
    public class PontoController : Controller
    {
        private readonly PontuacaoManipulador _manipulador;
        private readonly IPreCadastroRepositorio _repPreCadastro;
        private readonly IPontuacaoRepositorio _repPontuacao;


        public PontoController(PontuacaoManipulador manipulador, IPontuacaoRepositorio repPontuacao, IPreCadastroRepositorio repPreCadastro)
        {
            _manipulador = manipulador;
            _repPontuacao = repPontuacao;
            _repPreCadastro = repPreCadastro;
        }


        [HttpPost]
        [Route("v1/preregistro-pontuar")]
        //[Authorize(Policy = "Admin")]
        //[Authorize(Policy = "Funcionario")]
        public async Task<IComandoResultado> PreRegistroOuPontuar([FromBody] PontuarClienteComando comando)
        {
            //esse metodo consulta  o idCliente  na tabela CLIENTES para identifica se existe na base de Cliente
            int _idCliente = await ObterIdClienteNaBasePreCadastroAsync( comando.Contato);
            var _comando = new PontuarClienteComando {IdPreCadastro = _idCliente, IdEmpresa = comando.IdEmpresa, Id = comando.Id, Contato = comando.Contato, ValorInfor = comando.ValorInfor };

            return  (ComandoResultado) await _manipulador.ManipularAsync(_comando);
            
        }

        protected async Task<int> ObterIdClienteNaBasePreCadastroAsync(string Contato)
        {
            int _idPreCadastroDoContato;   
            var checagemDoContato =  await _repPreCadastro.ChecarContato(Contato); 

            if (checagemDoContato == false)
            {
                //se não encontrar, então gera contato em preCadastro  
               await _repPreCadastro.Salvar(Contato);
                //agora busca o ID na base cliente por parametro contato
                _idPreCadastroDoContato = await _repPreCadastro.ObterIdPreCadastro(Contato);
            }
            else
            {
                _idPreCadastroDoContato = await _repPreCadastro.ObterIdPreCadastro(Contato);
            }

            return _idPreCadastroDoContato;
        }


        [HttpPut]
        [Route("v1/resgatarPontos")]
        public async Task<IComandoResultado> ResgatarPontosAsync([FromBody]ResgatarPontosComando comando)
        {
            var resultado = (ComandoResultado)await _manipulador.ManipularAsync(comando);
            return resultado;
        }


        //[HttpPut]
        //[Route("v1/resgatarCashbacks")]
        //public async Task<IComandoResultado> ResgatarCashBackAsync([FromBody]ResgatarCashBackComando comando)
        //{
        //    var resultado = (ComandoResultado) await _manipulador.ManipularAsync(comando);
        //    return resultado;
        //}

    }
}


//[Authorize(Policy = "Admin")]
//[Authorize(Policy = "Funcionario")]