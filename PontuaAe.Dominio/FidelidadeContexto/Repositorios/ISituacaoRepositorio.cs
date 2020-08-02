using PontuaAe.Dominio.FidelidadeContexto.Entidades;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PontuaAe.Dominio.FidelidadeContexto.Repositorios
{
    public interface ISituacaoRepositorio
    {
       // Task atualizarSituacaoSMS(SituacaoSMS model);
        Task SalvarSituacao(SituacaoSMS model);
        //void EditarStatusVerificado(int IdEmpresa, int IdSMS);
        //Task<int> ObterQtdRetorno(int IdEmpresa, int ID);
        //SituacaoSMS obterId(int IdEmpresa, int IdCampanha);  remover
        Task<IEnumerable<SituacaoSMS>> ListaSituacaoSMS(int IdEmpresa);
    }
}
