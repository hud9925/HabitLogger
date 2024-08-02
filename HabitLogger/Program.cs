using System.Data.SQLite;


namespace HabitLogger
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to your own personal habitlogger\n");

            // Initalize and connect to SQLite database; create tables
            HabitDbHelper dbHelper = new HabitDbHelper("habitDatabase.db");
            dbHelper.CreateTable();

            // show the menu
            var menu = new Menu(dbHelper);
            menu.ShowMenu();

        }
        
    }
}
