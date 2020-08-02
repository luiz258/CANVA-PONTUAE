using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PontuaAe.Dominio.FidelidadeContexto.Entidades
{
    public class RegistroResgate
    {
        public RegistroResgate()
        {

        }
        public RegistroResgate(int idPreCadastro, int idEmpresa, int idUsuario, decimal pontoResgatado)
        {
            IdPreCadastro = idPreCadastro;
            IdEmpresa = idEmpresa;
            IdUsuario = idUsuario;
            PontoResgatado = pontoResgatado;
            DataResgate = DateTime.Now;
        }

        public int Id { get; set; }
        public int IdPreCadastro { get; set; }
        public int IdEmpresa { get; set; }
        public int IdUsuario { get; set; }
        public decimal PontoResgatado { get; set; }
        public DateTime DataResgate { get; set; }
    }
}
