using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using PontuaAe.Api.GereciamentoJobsTask.Jobs;
using PontuaAe.Api.Services.Email;
using PontuaAe.Dominio.FidelidadeContexto.Comandos.AutenticaComandos.Manipulador;
using PontuaAe.Dominio.FidelidadeContexto.Comandos.ClienteComandos.Manipulador;
using PontuaAe.Dominio.FidelidadeContexto.Comandos.EmpresaComandos.Manipulador;
using PontuaAe.Dominio.FidelidadeContexto.Comandos.FuncionarioComandos.Manipulador;
using PontuaAe.Dominio.FidelidadeContexto.Comandos.MarketingComandos.Manipulador;
using PontuaAe.Dominio.FidelidadeContexto.Comandos.PontuacaoComandos.Manipulador;
using PontuaAe.Dominio.FidelidadeContexto.Comandos.PremioComandos.Manipulador;
using PontuaAe.Dominio.FidelidadeContexto.Repositorios;
using PontuaAe.Dominio.FidelidadeContexto.Repositorios.Servicos.LocaSMS;
using PontuaAe.Infra.Repositorios;
using PontuaAe.Infra.Repositorios.RepositorioAvaliacao;
using PontuaAe.Infra.Repositorios.RepositorioFidelidade;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using Hangfire;
using System.Diagnostics;

using PontuaAe.Api.Controllers;
using PontuaAe.Dominio.FidelidadeContexto.Comandos.AutomacaoComandos.Manipulador;
using System.Threading.Tasks;
using PontuaAe.Api.Configuration;

namespace PontuaAe.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddApiConfig();


            services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                                  builder =>
                                  
                                  builder
                                  .AllowAnyOrigin()
                                  .AllowAnyMethod()
                                  .AllowAnyHeader()
                                 
                                      );
            });


            services.AddControllers();

            //Cofiguração Gerenciamento de Jobs/ Task  Quartz services
            //// documentação: https://www.quartz-scheduler.net/documentation/quartz-3.x/tutorial/crontriggers.html
            //// documentação atualizada: https://www.quartz-scheduler.net/documentation/quartz-3.x/tutorial/crontriggers.html#example-cron-expressions
            services.AddSingleton<IJobFactory, JobFactory>();
            services.AddSingleton<QuartzJobRunner>();
            services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();
            services.AddHostedService<QuartzHostedService>();

            //--------JOB COMPUTAR CLASSIFICAÇÃO CLIENTES
            services.AddScoped<ClassificaTipoClienteJob>();
            services.AddSingleton(new JobSchedule(
            jobType: typeof(ClassificaTipoClienteJob),
            cronExpression: "0 05 00 ? * MON-TUE,WED-THU,FRI,SAT-SUN"));

            //--------JOB Automação-SMS Aniversariantes
            services.AddScoped<AutomacaoAniversarioJob>();
            services.AddSingleton(new JobSchedule(
            jobType: typeof(AutomacaoAniversarioJob),
            cronExpression: "0 10 00 ? * MON-TUE,WED-THU,FRI,SAT-SUN"));

            //--------JOB Automação-SMS DIA DA SEMANA
            services.AddScoped<AutomacaoDiaDaSemanaJob>();
            services.AddSingleton(new JobSchedule(
            jobType: typeof(AutomacaoDiaDaSemanaJob),
            cronExpression: "0 15 00 ? * MON-TUE,WED-THU,FRI,SAT-SUN"));

            //--------JOB Automação-SMS 15 DIAS SEM RETORNO
            services.AddScoped<AutomacaoQuinzeDiasSemRetornoJob>();
            services.AddSingleton(new JobSchedule(
            jobType: typeof(AutomacaoQuinzeDiasSemRetornoJob),
            cronExpression: "0 20 00 ? * MON-TUE,WED-THU,FRI,SAT-SUN"));

            //--------JOB Automação-SMS  30 DIAS SEM RETORNO
            services.AddScoped<AutomacaoTrintaDiasRetornoJob>();
            services.AddSingleton(new JobSchedule(
            jobType: typeof(AutomacaoTrintaDiasRetornoJob),
            cronExpression: "0 25 00 ? * MON-TUE,WED-THU,FRI,SAT-SUN"));


            //--------JOB Automação-SMS APOS ULTIMA PONTUACAO
            services.AddScoped<AutomacaoUltimaFidelizacaoJob>();
            services.AddSingleton(new JobSchedule(
            jobType: typeof(AutomacaoUltimaFidelizacaoJob),
            cronExpression: "0 30 00 ? * MON-TUE,WED-THU,FRI,SAT-SUN"));


            //Cofiguração Gerenciamento de Jobs/ Task com HangFire
            //services.AddHangfire(x => x.UseSqlServerStorage("Server=den1.mssql7.gear.host; Database=pontuaae; User ID=pontuaae; Password=Lz8Nt8mPL~!5;"));

            //Configuração EmailSend
            services.Configure<EmailSetting>(Configuration.GetSection("EmailSetting"));
            services.AddTransient<Services.Email.IEmailSender, AuthMessageSender>();

            // Congiguração Documentação da API com Swagger
            services.AddSwaggerGen(c =>
            {
                
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Pontua Aê v2, MKT e Pos venda", Version = "v1" });
            });

            services.AddResponseCompression();
            services.AddScoped<DbConfig, DbConfig>();
           

            services.AddTransient<IUsuarioRepositorio, UsuarioRepositorio>();
            services.AddTransient<IPontuacaoRepositorio, PontuacaoRepositorio>();
            services.AddTransient<IClienteRepositorio, ClienteRepositorio>();
            services.AddTransient<IEmpresaRepositorio, EmpresaRepositorio>();
            services.AddTransient<IPremioRepositorio, PremioRepositorio>();
            services.AddTransient<IReceitaRepositorio, ReceitaRepositorio>();
            services.AddTransient<IConfigPontosRepositorio, ConfigPontosRepositorio>();
            services.AddTransient<IAutomacaoMSGRepositorio, AutomacaoSMSReposiorio>();
            services.AddTransient<ICampanhaMSGRepositorio, CampanhaMSGRepositorio>();
            services.AddTransient<IContaSMSRepositorio, ContaSMSRepositorio>();
            services.AddTransient<ISituacaoRepositorio, SituacaoRepositorio>();
            services.AddTransient<IEnviarSMS, EnviarSMS>();
            services.AddTransient<IContaSMSRepositorio, ContaSMSRepositorio>();
            services.AddTransient<IFuncionarioRepositorio, FuncionarioRepositorio>();
            services.AddTransient<IPreCadastroRepositorio, PreCadastroRepositorio>();
            services.AddTransient<IConfigClassificacaoClienteRepositorio, ConfigClassificacaoClienteRepositorio>();
            services.AddTransient<IConfiguracaoCashBackRepositorio, ConfiguracaoCashBackRepositorio>();
            services.AddTransient<UsuarioManipulador, UsuarioManipulador>();
            services.AddTransient<PontuacaoManipulador, PontuacaoManipulador>();
            services.AddTransient<ClienteManipulador, ClienteManipulador>();
            services.AddTransient<PremioManipulador, PremioManipulador>();
            services.AddTransient<EmpresaManipulador, EmpresaManipulador>();
            services.AddTransient<FuncionarioComandoManipulador, FuncionarioComandoManipulador>();
            services.AddTransient<CampanhaManipulado, CampanhaManipulado>();
            services.AddTransient<AutomacaoManipulador, AutomacaoManipulador>();        
       
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = string.Empty;
            });

            //não deleta este comentário
            //app.UseHangfireServer();
            //app.UseHangfireDashboard();

            app.UseRouting();
            app.UseCors(MyAllowSpecificOrigins);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            //não deleta este comentário
            //InitProcess();

        }
        private void InitProcess()
        {
            //não deleta este comentário
            // AutomacaoAniversarioJob aniversario = new AutomacaoAniversarioJob();
            // RecurringJob.AddOrUpdate<AutomacaoManipulador>(x =>  x.AutomacaoTipoDiaDaSemanaAsync(), Cron.Minutely());
        }
    }
}
