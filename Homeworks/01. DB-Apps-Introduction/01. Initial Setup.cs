using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_1_1
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "Server=.\\SQLEXPRESS; Database=master; Trusted_Connection=True";
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            string createDataBaseCommand = "CREATE DATABASE Minions";

            string useMinions = "USE Minions ";

            string createTableTownsCommand = "CREATE TABLE Towns " +
                                            "(" + "TownID INT PRIMARY KEY IDENTITY, "
                                            + "TownName Varchar(50), "
                                            + "Country VARCHAR(50) " + ") ";


            string createTableMinionsCommand = "CREATE TABLE Minions " +
                                        "(" + "MinionID INT PRIMARY KEY IDENTITY, "
                                        + "Name Varchar(50), "
                                        + "Age INT, "
                                        + "TownID INT, "
                                        + "FOREIGN KEY(TownID) REFERENCES Towns(TownID) " + ") ";

            string createTableVillainsCommand = "CREATE TABLE Villains " +
                                    "(" + "VillainID INT PRIMARY KEY IDENTITY, "
                                    + "Name Varchar(50), "
                                    + "Age INT, "
                                    + "EvilnessFactor VARCHAR(50) "
                                    + ") ";

            string createTableMinionsVilions = "CREATE TABLE MinionsVillains " +
                                       "(" + "MinionID INT, "
                                       + "VillainID INT, "
                                       + "PRIMARY KEY(MinionID, VillainID), "
                                       + "FOREIGN KEY(MinionID) REFERENCES Minions(MinionID), "
                                       + "FOREIGN KEY(VillainID) REFERENCES Villains(VillainID)");

            string insertIntoTowns = "INSERT INTO Towns (TownName, Country)" + "VALUES "
                                       + "('Sofia', 'Bulgaria' ), ('London', 'England' ), " + "('Berlin', 'Germany'), ('Plovdiv', 'Bulgaria' ), ('Burgas', 'Bulgaria' )";

            string insertIntoMinions = "INSERT INTO Minions (Name, Age, TownID) VALUES ('Bob', 10, 1 ), ('Kevin', 2, 2 ), ('Stuart', 7, 3), ('Siu', 4, 4 ), ('Pepi', 8, 5 )";

            string insertIntoVillions = "INSERT INTO Villains (Name, Age, EvilnessFactor)" + "VALUES ('Bobi', 10, 'good' ), ('Kev', 2, 'good' ), ('Student', 7, 'bad'), ('Sius', 4, 'bad' ), " + "('Pepina', 8, 'evil' ) ";

            string insertMinionsVillansCommandString = "INSERT INTO MinionsVillains (MinionID, VillainID) VALUES (1, 1), (2, 1), (3, 1), (4, 5), (5, 4) ";

            SqlCommand command = new SqlCommand(createDataBaseCommand, connection);

            using (connection)
            {
                command.ExecuteNonQuery();
                command.CommandText = useMinions;
                command.ExecuteNonQuery();
                command.CommandText = createTableTownsCommand;
                command.ExecuteNonQuery();
                command.CommandText = createTableMinionsCommand;
                command.ExecuteNonQuery();
                command.CommandText = createTableVillainsCommand;
                command.ExecuteNonQuery();
                command.CommandText = createTableMinionsVilions;
                command.ExecuteNonQuery();
                command.CommandText = insertIntoTowns;
                command.ExecuteNonQuery();
                command.CommandText = insertIntoMinions;
                command.ExecuteNonQuery();
                command.CommandText = insertIntoVillions;
                command.ExecuteNonQuery();
                command.CommandText = insertMinionsVillansCommandString;
                command.ExecuteNonQuery();

            }
            connection.Close();
        }
    }
}
