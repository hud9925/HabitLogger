

namespace HabitLogger
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // check if a sqLite DB exists 
            Console.WriteLine("Welcome to your own personal habitlogger\n");
            var menu = new Menu();
            menu.ShowMenu();

        }

    }
}
