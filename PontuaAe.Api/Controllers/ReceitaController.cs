using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PontuaAe.Dominio.FidelidadeContexto.Repositorios;

namespace PontuaAe.Api.Controllers
{
    //INDICADORES

    [ApiController]
    [Route("v1/receita")]
    [AllowAnonymous]
    public class ReceitaController : Controller
    {

        private readonly IReceitaRepositorio _repReceita;

        public ReceitaController(IReceitaRepositorio repReceita)
        {
            _repReceita = repReceita;
          
        }

    

        [HttpGet]
        [Route("v1/totalVendas/{idEmpresa}")]   
        public async Task<string> ObterTotalVendasMes(int idEmpresa)
        {
            var valorOriginal = await _repReceita.ObterTotalVendasMes(idEmpresa);
            var valorDoTicketMedio = valorOriginal.ToString("#,0.00", new CultureInfo("pt-BR"));
            var totalVendasMes = JsonConvert.SerializeObject(valorDoTicketMedio);

            return totalVendasMes;
        }

        [HttpGet]
        [Route("v1/ticketMedioMes/{idEmpresa}")]
        public async Task<string> ObterTicketMedio(int idEmpresa)
        {
            var valorOriginal = await _repReceita.ObterTicketMedio(idEmpresa);
            var valorDoTicketMedio =   valorOriginal.ToString("#,0.00", new CultureInfo("pt-BR"));
            var ticketMedioMes= JsonConvert.SerializeObject(valorDoTicketMedio);
            
            return ticketMedioMes;
        }


        //[HttpGet]
        //[Route("v1/RetidosMes/{idEmpresa}")]
        //public async Task<decimal> ReceitaRetidosMes(int idEmpresa)
        //{
        //    return await _repReceita.ObterReceitaRetidosMes(idEmpresa);
        //}


        //[HttpGet]
        //[Route("v1/RetidosSemana/{idEmpresa}")]
        //public async Task<decimal> ObterReceitaRetidosSemana(int idEmpresa)
        //{
        //    return await _repReceita.ObterReceitaRetidosSemana(idEmpresa);
        //}


        //[HttpGet]
        //[Route("v1/RetidosDia/{idEmpresa}")]
        //public async Task<decimal> ObterReceitaRetidosDia(int idEmpresa)
        //{
        //    return await _repReceita.ObterReceitaRetidosDia(idEmpresa);
        //}
    }
}