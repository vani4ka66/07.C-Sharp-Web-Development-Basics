using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_1_2
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "Server=.\\SQLEXPRESS; Database=Minions; Trusted_Connection=True";
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            using (connection)
            {
                string selectionCommandString = "select v.Name, count(*) " +
                                         "from Villains v" +
                                         " join MinionsVillains mv on mv.VillainID = v.VillainID "+
                                          "join Minions m on m.MinionID = mv.MinionID " +
                                          "group by v.Name having count(*) >= 3 " +
                                          "order by count(*) desc";
                SqlCommand command = new SqlCommand(selectionCommandString, connection);
                SqlDataReader reader = command.ExecuteReader();
                using (reader)
                {
                    while (reader.Read())
                    {
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            Console.Write($"{reader[i]} ");
                        }
                        Console.WriteLine();
                    }
                }

            }
        }
    }
}
