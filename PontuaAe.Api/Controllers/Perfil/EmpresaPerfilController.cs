using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PontuaAe.Compartilhado.Comandos;
using PontuaAe.Dominio.FidelidadeContexto.Comandos.EmpresaComandos.Entradas;
using PontuaAe.Dominio.FidelidadeContexto.Comandos.EmpresaComandos.Manipulador;
using PontuaAe.Dominio.FidelidadeContexto.Comandos.EmpresaComandos.Resultados;
using PontuaAe.Dominio.FidelidadeContexto.Repositorios;
using System;
using System.Collections.Generic;
using PontuaAe.Dominio.FidelidadeContexto.Consulta.EmpresaConsulta;
using PontuaAe.Dominio.FidelidadeContexto.Entidades;
using System.Threading.Tasks;
using PontuaAe.Dominio.FidelidadeContexto.Consulta.ClienteConsulta;
using Microsoft.AspNetCore.Cors;

namespace PontuaAe.Api.Controllers.Perfil
{


    [ApiController]
    [Route("v1/empresa")]
    [AllowAnonymous]
    public class EmpresaPerfilController:Controller
    {

        private readonly EmpresaManipulador _manipulador;
        private readonly IEmpresaRepositorio _repEmp;
        private readonly IClienteRepositorio _repCliente;


        public EmpresaPerfilController(EmpresaManipulador manipulador, IEmpresaRepositorio repEmp, IClienteRepositorio repCliente)
        {
            _manipulador = manipulador;
            _repEmp = repEmp;
            _repCliente = repCliente;
        }



        [HttpPost]
        [Route("v1/post")]
        public async System.Threading.Tasks.Task<IComandoResultado> PostAsync([FromBody] _AddDadosEmpresaComando comando)
        {
            var resultado = (ComandoEmpresaResultado)await _manipulador.ManipularAsync(comando);
            return resultado;
        }

        [HttpPut]
        [Route("v1/put")]
 
        public async System.Threading.Tasks.Task<IComandoResultado> PutPefilAsync([FromBody] EditarPerfilEmpresaComando comando)
        {
            var resultado = (ComandoEmpresaResultado)await _manipulador.ManipularAsync(comando);
            return resultado;
        }  


        [HttpGet]
        [Route("v1/detalhe/{id}")]
        // [Authorize(Policy ="Admin")]
        public async Task<ObterDetalheEmpresa> DetalheEmpresa(int Id)
        {
            var resultado = await _repEmp.ObterDetalheEmpresa(Id);
            return resultado;
        }

        [HttpGet]
        [Route("v1/lista")]
        //[Authorize(Policy = "Admin")]
        public async Task<IEnumerable<ListarEmpresasConsulta>> ListaEmpresaAsync()
        {
            var resultado = await _repEmp.ListaEmpresa();
            return resultado;
        }






    }

}

