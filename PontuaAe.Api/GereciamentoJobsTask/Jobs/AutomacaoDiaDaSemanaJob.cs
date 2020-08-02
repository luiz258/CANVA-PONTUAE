using PontuaAe.Dominio.FidelidadeContexto.Comandos.AutomacaoComandos.Manipulador;
using PontuaAe.Infra.Repositorios;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PontuaAe.Api.GereciamentoJobsTask.Jobs
{
    [DisallowConcurrentExecution]
    public class AutomacaoDiaDaSemanaJob : IJob
    {

     
        private readonly AutomacaoManipulador _manipulador;


        public AutomacaoDiaDaSemanaJob(AutomacaoManipulador manipulador)
        {

            _manipulador = manipulador;
            
        }

        public  Task Execute(IJobExecutionContext context)
        {
            
            _manipulador.AutomacaoTipoDiaDaSemana();
            return Task.CompletedTask;
        }
    }
}
