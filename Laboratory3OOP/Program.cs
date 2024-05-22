using System.Data;
using Microsoft.Data.SqlClient;
using System.Configuration;
#region User manual
/* User manual
  1. Выход Exit
*/
#endregion
namespace Laboratory3OOP
{
    class Program
    {
        
        private static string connectionString = ConfigurationManager.ConnectionStrings["ChampionatDB"].ConnectionString;

        private static SqlConnection sqlConnection = null;
        static void Main(string[] args)
        {
            sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            Console.WriteLine("ChampionatDB");

            SqlDataReader sqlDataReader = null;

            string command = "";

            while(true)
            {
                Console.Write(">");
                command = Console.ReadLine();

                SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);

                if (command.Split(' ')[0].ToLower().Equals("exit"))
                {
                    if (sqlConnection.State == ConnectionState.Open) 
                    {
                        sqlConnection.Close();
                    }
                    if (sqlDataReader != null)
                    {
                        sqlDataReader.Close();
                    }
                }

                switch (command.Split(' ')[0].ToLower())
                {
                    case "select":
                        sqlDataReader = sqlCommand.ExecuteReader();

                        while (sqlDataReader.Read())
                        {
                            Console.WriteLine($"{sqlDataReader["Id"]}  " +
                                $"{sqlDataReader["TeamName"]} " +
                                $" {sqlDataReader["CountGames"]}  " +
                                $"{sqlDataReader["CountPoints"]}");

                            Console.WriteLine(new string('-',30));

                           

                        }
                        if(sqlDataReader != null)
                        {
                            sqlDataReader.Close();
                        }

                        break;
                    case "update":
                        Console.WriteLine($"Изменено: {sqlCommand.ExecuteNonQuery()}");
                        break;
                    case "insert":
                        Console.WriteLine($"Добавлено: {sqlCommand.ExecuteNonQuery()}");
                        break;
                    case "delete":
                        Console.WriteLine($"Удалено: {sqlCommand.ExecuteNonQuery()}");
                        break;
                    default:
                        Console.WriteLine($"Данная {command} некорректна!");
                        break;
                }



                Console.WriteLine("Для продолжения нажмите любую клавишу...");
                Console.ReadKey();
            }
        }
    }
}