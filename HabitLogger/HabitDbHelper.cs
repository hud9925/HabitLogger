using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace HabitLogger
{
    public class HabitDbHelper
    {
        private SQLiteConnection conn;

        public HabitDbHelper(string dbPath)
        {
            string connectionString = "Data Source=habitDataBase.db; version=3; New=True; compress=True;";
            conn = new SQLiteConnection(connectionString);
            conn.Open();

        }
        //creates a table if it does not exist yet 
        public void CreateTable()
        {
            string sql = @"CREATE TABLE IF NOT EXISTS Habits 
                            (ID INTEGER PRIMARY KEY AUTOINCREMENT,
                            Name TEXT NOT NULL,
                            Date TEXT NOT NULL,
                            Quantity INTEGER NOT NULL);";
            using (var cmd = new SQLiteCommand(sql, conn))
            {
                cmd.ExecuteNonQuery();
            }
        }
        public void ViewAllHabits()
        {
            string sql = "SELECT * FROM Habits";
            using (var cmd = new SQLiteCommand(sql, conn))
            {
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        
                        Console.WriteLine($"ID: {reader["ID"]}, Name: {reader["Name"]}, Quantity: {reader["Quantity"]}, Date Performed: {reader["Date"]}");
                    }
                }
            }
            Console.WriteLine("Please press any key to return to the main menu");
            Console.ReadLine();
        }

        public void InsertToTable()
        {
           
            string dateInput = GetDateInput();

            string HabitName = GetNameInput();

            int quantity = GetNumberInput();

            // once it has been verified, insert into database
            string sql = "INSERT INTO Habits(Name, Date, Quantity) VALUES(@Name, @Date, @Quantity)";
            using (var cmd = new SQLiteCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@Name", HabitName);
                cmd.Parameters.AddWithValue("@Date", dateInput);
                cmd.Parameters.AddWithValue("@Quantity", quantity);
                cmd.ExecuteNonQuery();
            }
            Console.WriteLine("Habit Entered! Press any key to return to the main menu");
            Console.ReadLine();
            Console.Clear();
        }
        public string GetNameInput()
        {
            Console.WriteLine("\n\nPlease enter the name of the habit you are adding!");
            string nameInput = Console.ReadLine();
            

            return nameInput;
            // returns to main menu if 0
        }
        public string GetDateInput()
        {
            Console.WriteLine("\n\nPlease insert the date of the habit in this format: (dd-mm-yy).");
            string dateInput = Console.ReadLine();
            //if (dateInput == "0") GetUserInput();

            return dateInput;
        }
        public int GetNumberInput()
        {
            Console.WriteLine("\n\nPlease insert the quantity of the habit you have completed (e.g number of glasses of water drank, km ran, etc.) Please enter a non-negative whole number\n\n");
            string numberInput = Console.ReadLine();
            if (int.TryParse(numberInput, out int number) && number >=0)
            {
                return number;
            }
            else
            {
                Console.WriteLine("Invalid input, please enter a non-negative whole number.");
                return GetNumberInput(); // Recursive call until valid input
            }
        }

        public bool SelectHabit(int id)
        {
            string sql = "SELECT * FROM Habits WHERE ID = @ID";
            using (var cmd = new SQLiteCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@ID", id);
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        // habit with that ID exists
                        return true;

                    }
                    else
                    {
                        Console.WriteLine("No habit exists with that id. Please try again, or exit to the main menu");
                        return false;

                    }

                }

            }
        }


        public void UpdateHabit()
         {
            Console.WriteLine("For your Reference, here are all your habits that have been tracked: \n");

            //view all previous records
            string sql = "SELECT * FROM Habits";
            using (var cmd = new SQLiteCommand(sql, conn))
            {
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        Console.WriteLine($"ID: {reader["ID"]}, Name: {reader["Name"]}, Quantity: {reader["Quantity"]}, Date Performed: {reader["Date"]}");
                    }
                }
            }
            Console.WriteLine("Please enter the id of the habit you would like to Update!");
            string input = Console.ReadLine();
            if (!int.TryParse(input, out int id) || id <= 0)
            {
                // invalid id; user entered a numeric, but is a negative
                Console.WriteLine("Please enter a valid non-negative id number");
                return;

            }
            else
            {
                //id appears to be ok; now make sure it is actually present in the database
                if (!SelectHabit(id))
                {
                    return;
                }
                else
                {
                    // update the habit
                    string dateInput = GetDateInput();
                    int quantity = GetNumberInput();

                    string sql2 = "UPDATE Habits SET Date = @Date, Quantity = @Quantity WHERE ID = @ID";
                    using (var cmd = new SQLiteCommand(sql2, conn))
                    {
                        //execute Delete 
                        cmd.Parameters.AddWithValue("@ID", id);
                        cmd.Parameters.AddWithValue("@Date", dateInput);
                        cmd.Parameters.AddWithValue("@Quantity", quantity);
                        cmd.ExecuteNonQuery();
                    }
                    Console.WriteLine("Habit succesfully updated! Press any key to return to the main menu");
                    Console.ReadLine();
                    Console.Clear();
                }

            }


        }

        // public FindHabit() {}

        public void DeleteHabit()
        {
            Console.WriteLine("Here are all your habits that have been tracked: ");

            //view all previous records
            string sql = "SELECT * FROM Habits";
            using (var cmd = new SQLiteCommand(sql, conn))
            {
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        Console.WriteLine($"ID: {reader["ID"]}, Name: {reader["Name"]}, Quantity: {reader["Quantity"]}, Date Performed: {reader["Date"]}");
                    }
                }
            }
            Console.WriteLine("Please enter the id of the habit you would like to delete: ");
            string input = Console.ReadLine();
            if (!int.TryParse(input, out int id) || id <= 0)
            {
                // invalid id; user entered a numeric, but is a negative
                Console.WriteLine("Please enter a valid non-negative id number");
                return;

            }
            else
            {
                //id appears to be ok; now make sure it is actually present in the database
                if (!SelectHabit(id))
                {
                    return;
                }
                else
                {
                    string sql2 = "DELETE FROM Habits WHERE ID = @ID";
                    using (var cmd = new SQLiteCommand(sql2, conn))
                    {
                        //execute Delete 
                        cmd.Parameters.AddWithValue("@ID", id);
                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            Console.WriteLine("habit Successfully deleted.");
                        }
                        else
                        {
                            Console.WriteLine("An error has occured when trying to delete this habit");
                        }
                    }
                }
               
            }

         }
           


        public void CloseConnection()
        {
            conn.Close();
        }
    }
}
