using System;
namespace HabitLogger
{
    internal class Menu
    {
        private bool ProgramOn = true;
        private HabitDbHelper dbHelper;

        
        // establish connection with database

        public Menu(HabitDbHelper dbHelper)
        {
            this.dbHelper = dbHelper;
        }
        internal void ShowMenu()
        {
            while (ProgramOn)
            {
                Console.WriteLine("MAIN MENU");
                Console.WriteLine(@"What would you like to do?" + "\n" +
                "Type '0' to Close Application\n" +
                "Type '1' To View All Your Previous Records\n" +
                "Type '2' To Insert a new Record\n" +
                "Type '3' To Delete a Record\n" +
                "Type '4' To Update an Existing Record\n");
                Console.WriteLine("--------------------");
                var chosen_option = Console.ReadLine();
                   
                switch (chosen_option)
                {
                    case "0":
                        Console.WriteLine("Thanks for using the application!");
                        ProgramOn = false;
                        break;
                    case "1":
                        Console.Clear();
                        Console.WriteLine("Checking your previous records...");
                        dbHelper.ViewAllHabits();
                        break;
                    case "2":
                        dbHelper.InsertToTable();
                        // call insert method from HabitDbHelper.cs
                        break;
                    case "3":
                        Console.WriteLine("Deleting a habit! Please enter the id of the habit you would like to remove.");
                        dbHelper.DeleteHabit();
                       
                        break;

                    case "4":
                        dbHelper.UpdateHabit();
                        break;
                }

            }

        }
    }
}
