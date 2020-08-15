using PontuaAe.Dominio.FidelidadeContexto.Comandos.AutomacaoComandos.Manipulador;
using Quartz;
using System.Threading.Tasks;

namespace PontuaAe.Api.GereciamentoJobsTask.Jobs
{
    [DisallowConcurrentExecution]
    public class AutomacaoAniversarioJob :IJob
    {

        private readonly AutomacaoManipulador _manipulador;

        public AutomacaoAniversarioJob(AutomacaoManipulador manipulador)
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
