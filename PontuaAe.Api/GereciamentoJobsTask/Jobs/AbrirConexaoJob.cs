using PontuaAe.Infra.Repositorios;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PontuaAe.Api.GereciamentoJobsTask.Jobs
{
    public class AbrirConexaoJob : IJob
    {
        private readonly DbConfig _db;


        public AbrirConexaoJob(DbConfig db)
        {

            _db = db;
        }

        public Task Execute(IJobExecutionContext context)
        {
            var a = _db.Connection;
            return Task.CompletedTask;
        }
    }
}