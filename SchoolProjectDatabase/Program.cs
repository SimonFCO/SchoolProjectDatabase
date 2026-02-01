using Microsoft.EntityFrameworkCore;
using SchoolProjectDatabase.Models;
using System;
using System.Linq;

namespace SchoolProjectDatabase
{
    internal class Program
    {       
        static SchoolDatabaseContext context = new SchoolDatabaseContext();

        public static bool running = true;
        static void Main(string[] args)
        {
            // This is just the main loop :)
            while (running)
            {
                Ui.UiLoop(context);
            }
        }
    }
}