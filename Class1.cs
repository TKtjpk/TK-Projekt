using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;

namespace TK_Projekt
{
    class SQLConn
    {
        public List<string> Connection()
        {
            //Create the object of SqlConnection class to connect with database sql server
            using (SqlConnection conn = new SqlConnection())
            {
                var marki = new List<string>();
                //prepare conection string
                conn.ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\tomaszkubicki\\source\\repos\\TK Projekt\\Database1.mdf; Integrated Security=True";
                try
                {
                    //Prepare SQL command that we want to query
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT DISTINCT MarkaSamochodu FROM Pojazd";
                    cmd.Connection = conn;

                    // open database connection.
                    conn.Open();
                    Console.WriteLine("Connection Open ! ");

                    //Execute the query 
                    SqlDataReader sdr = cmd.ExecuteReader();

                    ////Retrieve data from table and Display result
                    while (sdr.Read())
                    {
                        string marka = (string)sdr["MarkaSamochodu"];
                        marki.Add(marka);
                    }

                    //Close the connection
                    conn.Close();
                    return marki;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Can not open connection !");
                    return marki;
                }
            }
        }
    }
}
