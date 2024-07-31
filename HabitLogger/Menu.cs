using System;
namespace HabitLogger
{
    internal class Menu
    {
        bool ProgramOn = true;
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
                //int value;
                //if (int.TryParse(chosen_option, value))
                //{


                // }    
                switch (chosen_option)
                {
                    case "0":
                        Console.WriteLine("Thanks for using the application!");
                        ProgramOn = false;
                        Environment.Exit(0);
                        break;
                    case "1":
                        Console.WriteLine("Checking your previous records...");
                        // implement soon
                        break;
                    case "2":
                        Console.WriteLine("Creating a new record!");
                        Console.WriteLine("What Habit Would you like to add? Please enter here: \n");
                        string habitName = Console.ReadLine();

                        Console.WriteLine("Thanks! How many times of this habit did you complete today? Please enter a numerical number \n");
                        var result = Console.ReadLine();
                        int quantity;
                        while (!int.TryParse(result, out quantity) || quantity < 0)
                        {
                            Console.WriteLine("Please enter a valid positive number!");
                            Console.WriteLine("How many times of this habit did you complete today? Please enter a numerical number \n");
                            result = Console.ReadLine();
                        }
                        var date = DateTime.UtcNow;
                        Console.WriteLine("Thank you! The habit has been added to your record!");
                        var NewHabit = new Habit(habitName, date, quantity);
                        NewHabit.ShowHabitDetails();
                        break;

                    case "3":
                        Console.WriteLine("Deleting a habit! Please enter the name of the habit you would like to remove.");
                        break;

                    case "4":
                        Console.WriteLine("Updating a habit! Please enter the name of the habit you would like to update!");
                        break;
                }

            }

        }
    }
}
