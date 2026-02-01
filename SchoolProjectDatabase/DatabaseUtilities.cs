using Microsoft.EntityFrameworkCore;
using SchoolProjectDatabase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProjectDatabase
{ 
    internal class DatabaseUtilities
    {
        public static void AddStaff(SchoolDatabaseContext context)
        {
            Console.Clear();
            Console.WriteLine("--- Lägg till Personal ---");

            Console.Write("Förnamn: ");
            string firstName = Console.ReadLine();

            Console.Write("Efternamn: ");
            string lastName = Console.ReadLine();

            Console.Write("Yrke (Teacher/Principal/Admin): ");
            string role = Console.ReadLine();

            var newStaff = new Staff
            {
                FirstName = firstName,
                LastName = lastName,
                Occupation = role
            };

            try
            {
                context.Staff.Add(newStaff);
                context.SaveChanges();
                Console.WriteLine("Personal sparad!");
            }
            catch (Exception ex)
            {
                // DEN HÄR DELEN ÄR VIKTIG:
                Console.WriteLine("Huvudfel: " + ex.Message);
                if (ex.InnerException != null)
                {
                    Console.WriteLine("DETALJERAT FEL: " + ex.InnerException.Message);
                }
            }
        }

        public static void ShowStudentsByClass(SchoolDatabaseContext context)
        {
            Console.Clear();
            Console.WriteLine("Välj en klass:");

            var classes = context.Classes.ToList();
            foreach (var c in classes)
            {
                Console.WriteLine($"ID: {c.ClassId}, Namn: {c.ClassName}");
            }

            Console.Write("Ange Klass-ID: ");
            if (int.TryParse(Console.ReadLine(), out int classId))
            {
                var students = context.Students
                                      .Where(s => s.ClassId == classId)
                                      .OrderBy(s => s.FirstName) 
                                      .ToList();

                if (students.Count > 0)
                {
                    Console.WriteLine($"\nElever i klass {classId}:");
                    foreach (var s in students)
                    {
                        Console.WriteLine("- " + s.FirstName + " " + s.LastName);
                    }
                }
                else
                {
                    Console.WriteLine("Inga elever hittades i den klassen.");
                }
            }
            else
            {
                Console.WriteLine("Ogiltigt ID.");
            }
        }

        public static void ShowAllStudents(SchoolDatabaseContext context)
        {
            Console.Clear();
            Console.WriteLine("Sortera efter:");
            Console.WriteLine("1. Förnamn");
            Console.WriteLine("2. Efternamn");
            string sortCol = Console.ReadLine();

            Console.WriteLine("Ordning:");
            Console.WriteLine("1. Stigande (A-Ö)");
            Console.WriteLine("2. Fallande (Ö-A)");
            string sortOrder = Console.ReadLine();

            var query = context.Students.ToList();

            if (sortCol == "1" && sortOrder == "1")
            {
                query = query.OrderBy(s => s.FirstName).ToList();

            }    
            else if (sortCol == "1" && sortOrder == "2")
            {
                query = query.OrderByDescending(s => s.FirstName).ToList();
            }               
            else if (sortCol == "2" && sortOrder == "1")
            {
                query = query.OrderBy(s => s.LastName).ToList();
            }    
            else if (sortCol == "2" && sortOrder == "2")
            {
                query = query.OrderByDescending(s => s.LastName).ToList();
            }

            var student = query.ToList();


            Console.WriteLine("\n--- Elevlista ---");
            foreach (var s in student)
            {
                Console.WriteLine($"{s.FirstName} {s.LastName} (ID: {s.StudentId})");
            }
        }
    }
}
