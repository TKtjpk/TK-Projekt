using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Windows;

namespace TK_Projekt
{
    internal class SQLClassLogin
    {
        string Ret;
        public string Connection(string user)
        {
            string connString = "Data Source=(localdb)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\tomaszkubicki\\source\\repos\\TK Projekt\\Database1.mdf; Integrated Security=True";

            SqlConnection conn = new SqlConnection(connString);

            Console.WriteLine("Connected !!!");

            string comand = $"SELECT Hash FROM Admin WHERE [Admin].[User] = '{user}';";

            SqlCommand cmd = new SqlCommand(comand, conn);

            SqlDataReader dataReader;

            cmd.CommandType = CommandType.Text;

            cmd.CommandText = comand;

            conn.Open();

            dataReader = cmd.ExecuteReader();

            if (dataReader.HasRows)
            {
                while (dataReader.Read())
                {
                    Ret = dataReader.GetString(0).Trim();
                }
            }
            conn.Close();

            return Ret;
        }
    }
}
