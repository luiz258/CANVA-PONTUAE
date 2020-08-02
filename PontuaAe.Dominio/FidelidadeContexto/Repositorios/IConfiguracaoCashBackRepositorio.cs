using PontuaAe.Dominio.FidelidadeContexto.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PontuaAe.Dominio.FidelidadeContexto.Repositorios
{
    public interface IConfiguracaoCashBackRepositorio
    {
        Task Salvar(ConfiguracaoCashBack model);
        Task Editar(ConfiguracaoCashBack model);
        Task Desativar(int IdEmpresa);
        Task<int> ChecarConfigCashBack();
    }
}
