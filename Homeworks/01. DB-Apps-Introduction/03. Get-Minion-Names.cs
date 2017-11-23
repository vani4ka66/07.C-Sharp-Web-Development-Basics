using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_1_3
{
    class Program
    {
        static void Main(string[] args)
        {
            int input = int.Parse(Console.ReadLine());

            string connectionString = "Server=.\\SQLEXPRESS; Database=Minions; Trusted_Connection=True;";
            SqlConnection connection = new SqlConnection(connectionString);
            //connection.Open();

            GetVillainName(input, connection);
            GetMinions(input, connection);
        }

        static void GetMinions(int vilainID, SqlConnection connection)
        {
            connection.Open();

            string selectionCommand = "SELECT m.Name, " +
                                      "m.Age " +
                                      "FROM Minions m " +
                                      "INNER JOIN MinionsVillains mv " +
                                      "ON mv.MinionID = m.MinionID " +
                                      "INNER JOIN Villains v " +
                                      "ON v.VillainID = mv.VillainID " +
                                      "WHERE v.VillainID = @villainID";

            SqlCommand command = new SqlCommand(selectionCommand, connection);
            command.Parameters.AddWithValue("@villainID", vilainID);

            SqlDataReader reader = command.ExecuteReader();
            int count = 1;

            using (reader)
            {

                if (!reader.Read())
                {
                    Console.WriteLine("<no minions>");
                }

                else
                {
                    while (reader.Read())
                    {
                        for (int i = 0; i < reader.FieldCount; i+=2)
                        {
                            Console.Write("{1}. {0} ", reader[i], count);
                            for (int j = i+1; j < reader.FieldCount; j+=2)
                            {
                                Console.Write(" {0} ", reader[j]);

                            }
                        }

                        Console.WriteLine();
                        count++;
                    }
                }
            }

            connection.Close();
        }

        static void GetVillainName(int villainID, SqlConnection connection)
        {
            connection.Open();

            string selectionCommand = "SELECT v.Name " +
                                      "FROM Villains v " +
                                      "WHERE v.VillainID = @villainID";

            SqlCommand command = new SqlCommand(selectionCommand, connection);
            command.Parameters.AddWithValue("@villainID", villainID);

            SqlDataReader reader = command.ExecuteReader();

            using (reader)
            {
                if (!reader.Read())
                {
                    Console.WriteLine("No villain with ID {0} exists in the database.", villainID);
                }

                else
                {
                    Console.WriteLine("Villain: {0}", reader[0]);
                }
            }

            connection.Close();
        }
    }
}
