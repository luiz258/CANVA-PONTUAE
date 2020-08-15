using PontuaAe.Dominio.FidelidadeContexto.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PontuaAe.Dominio.FidelidadeContexto.Repositorios
{
    public interface IConfigClassificacaoClienteRepositorio
    {
        Task Salvar(TemplateClassificacaoCliente model);
        Task Editar(TemplateClassificacaoCliente model);
        Task<ConsultaTemplateClassificacaoCliente> ObterConfig(int IdEmpresa);
    }
}
