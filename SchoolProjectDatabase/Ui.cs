using SchoolProjectDatabase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProjectDatabase
{
    internal class Ui
    {
        public static void UiLoop(SchoolDatabaseContext context)
        {
            Console.Clear();
            Console.WriteLine("--- SKOLMENY ---");
            Console.WriteLine("1. Hämta alla elever");
            Console.WriteLine("2. Hämta elever i en viss klass");
            Console.WriteLine("3. Lägg till ny personal");
            Console.WriteLine("4. Avsluta");
            Console.Write("Välj: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    DatabaseUtilities.ShowAllStudents(context);
                    break;
                case "2":
                    DatabaseUtilities.ShowStudentsByClass(context);
                    break;
                case "3":
                    DatabaseUtilities.AddStaff(context);
                    break;
                case "4":
                    Program.running = false;
                    break;
                default:
                    Console.WriteLine("Ogiltigt val.");
                    break;
            }
            if (Program.running)
            {
                Console.WriteLine("\nTryck enter för att återgå...");
                Console.ReadKey();
            }
        }

    }
}
