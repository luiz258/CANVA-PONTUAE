using FluentValidator;
using PontuaAe.Compartilhado.Comandos;
using PontuaAe.Dominio.Pesquisa.Comandos.Entradas;
using PontuaAe.Dominio.Pesquisa.Comandos.Resultados;
using PontuaAe.Dominio.Pesquisa.Entidades;
using PontuaAe.Dominio.Questionario.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PontuaAe.Dominio.Questionario.Comandos.Manipulador
{
    public class NetPromoterScoreManipulador : Notifiable,
        IComandoManipulador<SalvaAvaliacaoNps>

    {
        private readonly INetPromoterScoreRepositorio _repNPS;

        public NetPromoterScoreManipulador(INetPromoterScoreRepositorio repNPS)
        {
            _repNPS = repNPS;
        }

        public async Task<IComandoResultado> ManipularAsync(SalvaAvaliacaoNps comando)
        {
            //Salva avaliacao do cliente          
            var avaliacao = new NetPromoterScore(comando.Nota, comando.Comentario, comando.IdEmpresa, comando.IdCliente);
            _repNPS.salva(avaliacao);


            int p = _repNPS.obterNumeroPromotores(comando.IdEmpresa);
            int d = _repNPS.obterNumeroDetratores(comando.IdEmpresa);
            int n = _repNPS.obterNumeroTotalNeutro(comando.IdEmpresa);
            var netPromoterScore = new NetPromoterScore(p, d, n );
            _repNPS.salva(netPromoterScore);

            return new ComandoResultado(true, "ok", Notifications);       
 
        }

        //a pesquisa NPS e criada no modelo campanha de envio de sms
        //a pesquisa fica sera enviado via sms, tem um link que redireciona para uma pagina, assim que ele responder e salvo  
        // porém os dados seram computados apos 15 dias a 30 dias 

        public IComandoResultado Manipulador(SalvaConfiguracaoNps a)
        {
            return new ComandoResultado(true);
        }
    }
}
