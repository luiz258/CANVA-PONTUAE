using PontuaAe.Dominio.FidelidadeContexto.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PontuaAe.Dominio.FidelidadeContexto.Repositorios
{
    public interface IMensagem<Entity>
    {
        Task Salvar(Entity model);
        Task Editar(Entity model);
        Task Desativar(int IdEmpresa, int ID);
        Task Deletar(int IdEmpresa, int ID);



    }
}
