using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PontuaAe.Compartilhado.DbConfig
{
    public static class Config
    {
        public static string ConnectionString = ("Server=tcp:db-pontuaae.database.windows.net,1433;Initial Catalog=Db-Pontuaae;Persist Security Info=False;User ID=pontuaae-sa;Password=1q2w3e4r!@#$;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
    }
}
