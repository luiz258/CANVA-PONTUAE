using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using PontuaAe.Compartilhado.Comandos;
using PontuaAe.Dominio.FidelidadeContexto.Comandos.ClienteComandos.Entradas;
using PontuaAe.Dominio.FidelidadeContexto.Comandos.ClienteComandos.Manipulador;
using PontuaAe.Dominio.FidelidadeContexto.Comandos.ClienteComandos.Resultados;
using PontuaAe.Dominio.FidelidadeContexto.Consulta.ClienteConsulta;
using PontuaAe.Dominio.FidelidadeContexto.Repositorios;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PontuaAe.Api.Controllers.Perfil
{
    
    [ApiController]  
    [Route("v1/cliente")]
    public class ClientePerfilController : Controller
    {
        private readonly ClienteManipulador _manipulador;
        private readonly IClienteRepositorio _repCliente;


        public ClientePerfilController(ClienteManipulador manipulador, IClienteRepositorio repCliente)
        {
            _manipulador = manipulador;
            _repCliente = repCliente;
        }

        //CHECAR CONTATO  AVERIGUAR SE POSE REMOVER
        
        [HttpGet]
        [Route("v1/contatos")]
        public async Task<int> VerificaContaUsuarioPorContato(string Contato)
        {
            int resultado;
            int idUsuario = await _repCliente.ChecarCampoIdUsuarioPorContato(Contato);
            if (idUsuario == 0)
            {
                resultado = 0;
            }
            else
            {
                resultado = 1;
            }


            return resultado;
        }

        //Cliente se cadastra
       
        [HttpPost]
        [Route("v1/post")]
        public async Task<IComandoResultado> PostAsync([FromBody] CadastroClienteComando comando)
        {
            try
            {
                var resultado = (ComandoClienteResultado)await _manipulador.ManipularAsync(comando);
                return resultado;
            }
            catch (Exception e)
            {

                throw;
            }

           
        }


        
        [HttpPut]
        [Route("v1/put")]
        public async Task<IComandoResultado> PutAsync([FromBody] EditarClienteComando comando)
        {
            try
            {
                var cliente = (ComandoClienteResultado)await _manipulador.ManipularAsync(comando);
                return cliente;
            }
            catch(Exception e)
            {
                throw;
            }
        }


        
        [HttpGet]
        [Route("v1/detalhe/{idUsuario}")]
        public async Task<ObterPerfilCliente> ObterDadosClientePorIdUsuario(int IdUsuario)
        {
            return await _repCliente.ObterClientePorIdUsuario(IdUsuario);
        }


        [HttpGet]
        [Route("v1/detalhe/{id}/{idEmpresa}")]
        public async Task<ObterDadosDoCliente> ObterDetalheCliente(int id, int idEmpresa)
        {
            //A empresa faz a solicitacao dos dados do cliente
            return await _repCliente.ObterDetalheCliente(idEmpresa, id);
        }


        [HttpGet]
        [Route("v1/lista/{IdEmpresa}")]
        //[Authorize(Policy = "Admin")]
        //[AllowAnonymous]
        public async Task<IEnumerable<ListaRankingClientesConsulta>> ListaRankingClientes(int IdEmpresa)
        {
            return await _repCliente.ListaRankingClientes(IdEmpresa);
        }

       
        [HttpGet]
        [Route("v1/clienteSaldo/{contato}")]
        public async Task<IEnumerable<ObterSaldoClienteConsulta>> ObterListaDeLocaisComSaldoDePontos(string Contato)
        {
            return await _repCliente.ListaDeSaldoPorEmpresa(Contato);
        }


        
        [HttpGet]
        [Route("v1/totalclientes/{idEmpresa}")]
        
        public async Task<int> ObterTotalClientes(int idEmpresa)
        {
            return  await _repCliente.ObterTotalCliente(idEmpresa);
          
        }

        
        [HttpGet]
        [Route("v1/recorrentes/{idEmpresa}")]
        
        public async Task<int> ObterRetidos(int idEmpresa)
        {
            return await _repCliente.ObterTotalClientesRetido(idEmpresa);
        }





    }
}
