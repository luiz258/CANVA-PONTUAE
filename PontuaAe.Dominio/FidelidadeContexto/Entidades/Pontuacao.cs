using FluentValidator;
using System;
using System.Threading.Tasks;

namespace PontuaAe.Dominio.FidelidadeContexto.Entidades
{
    public class Pontuacao : Notifiable
    {
        public Pontuacao(decimal saldo, int idEmpresa, int idPreCadastro)
        {
            IdPreCadastro = idPreCadastro;
            IdEmpresa = idEmpresa;
            Saldo = saldo;
            DataVisita = DateTime.Now;
            Segmentacao = "Novo";
            SegCustomizado = "Ativo";
        }
        public Pontuacao(int idPreCadastro, int idEmpresa)
        {
            IdPreCadastro = idPreCadastro;
            IdEmpresa = idEmpresa;
            DataVisita = DateTime.Now;

        }

        public Pontuacao(decimal saldo)
        {

            Saldo = saldo;
            DataVisita = DateTime.Now;

        }

        public Pontuacao()
        {

        }

        public int ID { get; set; }
        public int IdEmpresa { get; private set; }
        public int IdPreCadastro { get; private set; }
        public DateTime Validade { get; private set; }
        public decimal SaldoTransacao { get; private set; }
        public decimal Saldo { get; private set; }
        public DateTime DataVisita { get; private set; }
        public string Segmentacao { get; private set; }
        public string SegCustomizado { get; private set; }

        public void Pontuar(decimal valorGasto, decimal pontosFidelidade, decimal gastoNecessario, decimal saldo)
        {
            var _saldoTransacao = (valorGasto * pontosFidelidade) / gastoNecessario;
            SaldoTransacao = Math.Round(_saldoTransacao, 0);
            saldo += SaldoTransacao;
            Saldo = saldo;
            // Mudar esta regra,  para valida a data do programa, colocar uma data de expiração do programa 
            Validade = DateTime.Now.AddDays(180); //adiciono a validade exemplo   360 dias
        }

        //Este percentual é aplicado em contas do tipo CASH BACK quanto o saldo do cliente é igual a ZERO.Sua fórmula de bonificação é: bonificacao = valor_compra* (percentual_conta_zerada/100)
        public void PontuarPorCashBack(decimal percentual, decimal valorCompra, decimal saldo)
        {
            var bonificacao = valorCompra * (percentual / 100);
            SaldoTransacao = bonificacao;
            saldo += SaldoTransacao;
            Saldo = saldo;
        }



        public void Resgatar(decimal qtdPontos)
        {
            Saldo -= qtdPontos;
        }

        public void DebitarCashBack(decimal Valor)
        {
            Saldo -= Valor;
        }

        //estou subsstituindo o qtdVisitasUmMes para qtdVisitasClassificacaoOuro  assim cada empresa vai ter uma configuração pra qtdVistas um mes pra classifica 
        //o cliente como ouro , prata
        public void SeguimentarCliente(DateTime ultimaVisita, int tempoEmdiasClienteOuro, int tempoEmDiasClientePrata, int tempoEmDiasClienteBronze, int qtdVisitasClassificacaoOuro, int qtdVisitasClassificacaoPrata, int qtdVisitasClassificacaoBronze, int qtdRetornoOuro, int qtdRetornoPrata, int qtdRetornoBronze, int id, int idEmpresa)
        {
            TimeSpan data = DateTime.Now.Subtract(ultimaVisita);
            int TempoEmDias = data.Days;

            this.ID = id;
            this.IdEmpresa = idEmpresa;


            if (qtdRetornoBronze > 3) // o valor 3   determina o segmento customizado como ativo
            {

                if (tempoEmdiasClienteOuro == 0)
                {
                    tempoEmdiasClienteOuro = 30;

                    if (TempoEmDias <= tempoEmdiasClienteOuro)
                    {

                        if (qtdRetornoOuro == 2)
                        {
                            EstadoAtivo();

                        }

                        if (qtdRetornoOuro <= qtdVisitasClassificacaoBronze)
                        {
                            EstadoBronze();
                            PerfilVip();

                        }

                        if (qtdRetornoOuro > qtdVisitasClassificacaoBronze && qtdRetornoOuro <= qtdVisitasClassificacaoPrata)
                        {
                            EstadoPrata();
                            PerfilVip();
                        }

                        else if (qtdRetornoOuro > qtdVisitasClassificacaoPrata) //os numeros vao se substituido pela variavel qtdVisitasClassificacaoOuro...
                        {
                            EstadoOuro();
                            PerfilVip();
                        }
                    }
                }

                if (tempoEmDiasClientePrata == -1)
                {
                    tempoEmDiasClientePrata = 60;

                    if (TempoEmDias <= tempoEmDiasClientePrata)
                    {

                        if (qtdRetornoBronze > qtdVisitasClassificacaoBronze && qtdRetornoPrata <= qtdVisitasClassificacaoPrata)
                        {
                            EstadoPrata();
                            PerfilVip();
                        }
                    }

                    else if (qtdRetornoPrata > qtdVisitasClassificacaoPrata)
                    {
                        EstadoOuro();
                        PerfilVip();
                    }
                }


                if (tempoEmDiasClienteBronze == -3)
                {
                    tempoEmDiasClienteBronze = 90;

                    if (TempoEmDias <= tempoEmDiasClienteBronze)
                    {

                        if (qtdRetornoPrata >= qtdVisitasClassificacaoBronze && qtdRetornoPrata < qtdVisitasClassificacaoPrata)
                        {
                            EstadoBronze();
                            PerfilVip();
                        }
                        else if (qtdRetornoBronze > qtdVisitasClassificacaoOuro)
                        {
                            EstadoOuro();
                            PerfilVip();
                        }
                    }
                }
            }

        }
        public void ClassificaClientesNaoFrequentes(DateTime ultimaVisita, int tempoEmDiasClienteInativo, int tempoEmDiasClientePedido)
        {
            TimeSpan data = DateTime.Now.Subtract(ultimaVisita);
            int TempoEmDias = data.Days;

            //if (TempoEmDias >= tempoEmDiasClienteEmRisco)  // Ainda não sera aplicado
            //{
            //    EstadoEmRisco();

            //}

            if (TempoEmDias > tempoEmDiasClientePedido)
            {
                EstadoPerdido();

            }

            if (TempoEmDias >= tempoEmDiasClienteInativo)
            {
                EstadoInativo();

            }

        }

        //status da  classificação da Segmentação customizada
        public void EstadoAtivo() => SegCustomizado = "Ativo";
        public void EstadoOuro() => SegCustomizado = "Ouro";
        public void EstadoPrata() => SegCustomizado = "Prata";
        public void EstadoBronze() => SegCustomizado = "Bronze";
        //public void EstadoEmRisco() => SegCustomizado = "Em risco";
        public void EstadoPerdido() => SegCustomizado = "Perdido";
        public void EstadoInativo() => SegCustomizado = "Inativo";

        //status da Segmentação em  Perfil:
        public void PerfilNovo() => Segmentacao = "Novo";
        public void PerfilVip() => Segmentacao = "Vip";
        //public void PerfilEventual() => Segmentacao = "Eventual";
        public void PerfilFrequente() => Segmentacao = "Frequente";







    }
}
