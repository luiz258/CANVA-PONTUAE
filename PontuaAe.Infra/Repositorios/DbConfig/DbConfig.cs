using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PontuaAe.Infra.Repositorios
{
    public class DbConfig
    {
        public SqlConnection Connection { get; set; }

        public DbConfig()
        {
            Connection = new SqlConnection("Server=db-pontuaae.database.windows.net,1433;Database=Db-Pontuaae;User ID=pontuaae-sa;Password=1q2w3e4r!@#$;Trusted_Connection=False;Encrypt=True;");
            Connection.Open();
        }

        public void Dispose()
        {
            if (Connection.State != ConnectionState.Closed)
                Connection.Close();
        }
    }
}
