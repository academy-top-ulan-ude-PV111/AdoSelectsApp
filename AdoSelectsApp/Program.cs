using System.Data;
using System.Data.SqlClient;

namespace AdoSelectsApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=LibraryDb;Integrated Security=True;Connect Timeout=10;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();

                string title = "Buratio";
                string author = "Aleksey Tolstoy";
                double price = 150.60;

                string cmdInsertText = $"INSERT INTO books (title, author, price)" +
                    $"VALUES (@title, @author, @price)";

                SqlCommand command = sqlConnection.CreateCommand();
                command.CommandText = cmdInsertText;

                //
                SqlParameter titleParam = new SqlParameter("@title", title);
                titleParam.SqlDbType = SqlDbType.NVarChar;
                titleParam.Size = 100;
                
                //
                SqlParameter authorParam = new("@author", SqlDbType.NVarChar, 100);
                authorParam.Value = author;

                //
                SqlParameter priceParam = new("@price", SqlDbType.Money);
                priceParam.Value = price;

                command.Parameters.AddRange(new[] { titleParam, authorParam, priceParam });

                int result = command.ExecuteNonQuery();
                Console.WriteLine($"Add rows: {result}");

                command.CommandText = "SELECT * FROM books";
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if(reader.HasRows)
                    {
                        while(reader.Read())
                        {
                            Console.WriteLine($"{reader.GetValue(0)}\t{reader.GetValue(1)}\t{reader.GetValue(2)}\t{reader.GetValue(3)}\t");
                        }
                    }
                }
            }
        }
    }
}