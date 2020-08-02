using PontuaAe.Compartilhado.Comandos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PontuaAe.Dominio.FidelidadeContexto.Consulta.EmpresaConsulta
{
    public class ObterDetalheEmpresa :IComando
    {
        public string Email { get; set; }
        public string NomeFantasia { get; set; }
        public string Descricao { get; set; }
        public string NomeResponsavel { get; set; }
        public string Telefone { get; set; }
        public string Documento { get; set; }
        public string Seguimento { get; set; }
        public string Horario { get; set; }
        public string Facebook { get; set; }
        public string Website { get; set; }
        public string Instagram { get; set; }
        public string Delivery { get; set; }
        public string Bairro { get; set; }
        public string Rua { get; set; }
        public string Numero { get; private set; }
        public string Cep { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string Logo { get; set; }
        public string Complemento { get; set; }


        public bool Valida()
        {
            throw new NotImplementedException();
        }
    }
}
