using PontuaAe.Dominio.FidelidadeContexto.Comandos.AutomacaoComandos.Manipulador;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PontuaAe.Api.GereciamentoJobsTask.Jobs
{
    [DisallowConcurrentExecution]
    public class AutomacaoTrintaDiasRetornoJob : IJob
    {
        private readonly AutomacaoManipulador _manipulador;


        public AutomacaoTrintaDiasRetornoJob(AutomacaoManipulador manipulador)
        {

            _manipulador = manipulador;
        }

        public Task Execute(IJobExecutionContext context)
        {
            // _manipulador.AutomacaoTipoTrintaDias();
            return Task.CompletedTask;
        }
    }
}
