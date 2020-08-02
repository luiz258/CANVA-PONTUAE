using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PontuaAe.Dominio.FidelidadeContexto.Comandos.ClienteComandos.Manipulador;
using Quartz;
using System;
using System.Threading.Tasks;

namespace PontuaAe.Api.GereciamentoJobsTask.Jobs
{
    [DisallowConcurrentExecution]
    public class ClassificaTipoClienteJob : IJob
    {
        private readonly ClienteManipulador _manipulador;


        public ClassificaTipoClienteJob(ClienteManipulador manipulador)
        {
           
            _manipulador = manipulador;
        }

        public Task Execute(IJobExecutionContext context)
        {
           _manipulador.ClassificaRecorrencia();
            return Task.CompletedTask;
        }
    }
}
