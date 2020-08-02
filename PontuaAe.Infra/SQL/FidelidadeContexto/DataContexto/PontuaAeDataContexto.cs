using PontuaAe.Compartilhado.DbConfig;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PontuaAe.Infra.FidelidadeContexto.DataContexto
{
    public class PontuaAeDataContexto : IDisposable
    {
        public SqlConnection Connection { get; set; }

        public PontuaAeDataContexto()
        {
            Connection = new SqlConnection("Server= den1.mssql7.gear.host; Database=pontuaedb; User ID=pontuaedb; Password=Gi3Q-?06D8XL;");
            Connection.Open();
        }

        public void Dispose()
        {
            if (Connection.State != ConnectionState.Closed)
                Connection.Close();
        }
    }
}

