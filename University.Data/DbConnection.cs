using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University.Data
{
    public class DbConnection
    {
        public static string GetConnectionString()
        {
            string connectionString = "Data Source=DESKTOP-M1MKH53\\SQLEXPRESS;Initial Catalog=IPSU_DEV_V5;Integrated Security=True";

            //string connectionString = "Data Source=43.255.152.26;Initial Catalog=DemoDB;Integrated Security=False;User ID=wdipl;Password=wdipl@1234;Encrypt=False;Packet Size=4096";

            //string connectionString = "Data Source=tcp:s08.everleap.com;Initial Catalog=DB_4287_emedsuredb;User ID=DB_4287_emedsuredb_user;Password=wdipl@123;Integrated Security=False;";

            return connectionString;
        }
    }
}
