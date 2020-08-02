using PontuaAe.Dominio.FidelidadeContexto.Comandos.AutomacaoComandos.Manipulador;
using Quartz;
using System.Threading.Tasks;

namespace PontuaAe.Api.GereciamentoJobsTask.Jobs
{
    [DisallowConcurrentExecution]
    public class AutomacaoAniversárioJob :IJob
    {

        private readonly AutomacaoManipulador _manipulador;

        public AutomacaoAniversárioJob(AutomacaoManipulador manipulador)
        {

            _manipulador = manipulador;
        }

        public Task Execute(IJobExecutionContext context)
        {
             //_manipulador.TipoAutomacaoAniversario();
            return  Task.CompletedTask;
        }
    }
}
