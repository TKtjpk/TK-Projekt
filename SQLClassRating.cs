using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace TK_Projekt
{
    internal class SQLClassRating
    {
        private int Ret;
        public int Connection(int id = 1)
        {
            string connString = "Data Source=(localdb)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\tomaszkubicki\\source\\repos\\TK Projekt\\Database1.mdf; Integrated Security=True";

            SqlConnection conn = new SqlConnection(connString);

            Console.WriteLine("Connected !!!");

            string comand = $"SELECT AVG(Rating) FROM Rating WHERE CarId = {id};";

            SqlCommand cmd = new SqlCommand(comand, conn);
            
            SqlDataReader dataReader;

            cmd.CommandType = CommandType.Text;

            cmd.CommandText = comand;

            conn.Open();

            dataReader = cmd.ExecuteReader();

            if (dataReader != null)
            {
                while (dataReader.Read())
                {
                    Ret = dataReader.GetInt32(0);
                }
            }
            conn.Close();

            return Ret;
        }
        public void Rate(int id, int rating)
        {
            string connString = "Data Source=(localdb)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\tomaszkubicki\\source\\repos\\TK Projekt\\Database1.mdf; Integrated Security=True";

            SqlConnection conn = new SqlConnection(connString);

            Console.WriteLine("Connected !!!");

            string comand = $"INSERT INTO Rating VALUES ({id}, {rating});";

            SqlCommand cmd = new SqlCommand(comand, conn);

            conn.Open();

            cmd.ExecuteNonQuery();

            conn.Close();
        }
    }
}
