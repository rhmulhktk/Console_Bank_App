using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Bank_App
{
    internal static class DbConnection
    {
        public static string ConnectionString()
        {
            var datasource = @"workstation id=CargoProject.mssql.somee.com;packet size=4096;user id=rhmulhktk_SQLLogin_1;pwd=SqlServer@010101;data source=CargoProject.mssql.somee.com;persist security info=False;initial catalog=CargoProject";//your server
            var database = "CargoProject"; //your database name
            var username = "rhmulhktk_SQLLogin_1"; //username of server to connect
            var password = "SqlServer@010101"; //password

            //your connection string 
            string connString = @"Data Source=" + datasource + ";Initial Catalog="
                        + database + ";Persist Security Info=True;User ID=" + username + ";Password=" + password;

            return connString;
        }
    }
}
