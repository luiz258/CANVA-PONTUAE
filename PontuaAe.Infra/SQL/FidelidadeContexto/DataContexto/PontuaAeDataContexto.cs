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
        //classe nao utilizada
        public PontuaAeDataContexto()
        {
            Connection = new SqlConnection("Server=tcp:db-pontuaae.database.windows.net,1433;Initial Catalog=Db-Pontuaae;Persist Security Info=False;User ID=pontuaae-sa;Password=1q2w3e4r!@#$;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            Connection.Open();
        }

        public void Dispose()
        {
            if (Connection.State != ConnectionState.Closed)
                Connection.Close();
        }
    }
}

