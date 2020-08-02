using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PontuaAe.Dominio.Pesquisa.Entidades
{
    public class NetPromoterScore
    {
        //deletar esse construtor
        public NetPromoterScore(int promotores = 0 , int detratros = 0, int neutros = 0)
        {
            Promotores = promotores;
            Detratores = detratros;
            Neutros = neutros;

        }
        public NetPromoterScore(int nota, string comentario, int idEmpresa, int idCliente)
        {
            Nota = nota;
            Data = DateTime.Now;
            IdCliente = idCliente;
            IdEmpresa = idEmpresa;
            Comentario = comentario;
        }

        public int IdEmpresa { get; private set; }
        public string Telefone { get; private set; }
        public int IdCliente { get; private set; }
        public DateTime Data { get; private set; }
        public int Nota { get; private set; }
        public int Promotores { get; private set; }
        public int Detratores { get; private set; }
        public int Neutros { get; private set; }
        public int TotalRespondentes { get; private set; }
        public int Nps { get; private set; }
        public string Comentario { get; private set; }


        //Função para calculo do NPS
        public int CalcularNPS()
        {
            TotalRespondentes = Detratores + Neutros + Promotores;
            return  Nps = (100 * (Promotores - Detratores) / TotalRespondentes); 
        }
    }
}
