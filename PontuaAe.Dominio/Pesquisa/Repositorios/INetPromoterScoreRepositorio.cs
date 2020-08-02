using PontuaAe.Dominio.Pesquisa.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PontuaAe.Dominio.Questionario.Repositorios
{
    public interface INetPromoterScoreRepositorio 
    {
        void salva(NetPromoterScore nps);
        void salvaAvaliacaoNps(NetPromoterScore avaliacao);
        //Fazer Select de duas tabelas, vou precis do nome do cliente, a nota que deu e a resposta,  as TABELAS SERAM  AVALIACAO, CLIENTE
        int obterNumeroPromotores(int IdEmpresa);
        int obterNumeroDetratores(int IdEmpresa);
        int obterNumeroTotalRespondentes(int IdEmpresa);
        int obterNumeroTotalNeutro(int IdEmpresa);
        int obterNPS(int IdEmpresa);

    }
}
