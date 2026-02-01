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

            Console.Write("Ange ID (Siffra): ");
            int id = int.Parse(Console.ReadLine());

            Console.Write("Förnamn: ");
            string firstName = Console.ReadLine();

            Console.Write("Efternamn: ");
            string lastName = Console.ReadLine();

            Console.Write("Yrke (Teacher/Principal/Admin): ");
            string role = Console.ReadLine();

            var newStaff = new Staff
            {
                StaffId = id,
                FirstName = firstName,
                LastName = lastName,
                Role = role
            };

            try
            {
                context.Staff.Add(newStaff);
                context.SaveChanges();
                Console.WriteLine("Personal sparad!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Fel vid sparning: " + ex.Message);
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

            var query = context.Students.AsQueryable();

            if (sortCol == "1" && sortOrder == "1")
            {
                query = query.OrderBy(s => s.FirstName);
            }    
            else if (sortCol == "1" && sortOrder == "2")
            {
                query = query.OrderByDescending(s => s.FirstName);
            }               
            else if (sortCol == "2" && sortOrder == "1")
            {
                query = query.OrderBy(s => s.LastName);
            }    
            else if (sortCol == "2" && sortOrder == "2")
            {
                query = query.OrderByDescending(s => s.LastName);
            }
               

            Console.WriteLine("\n--- Elevlista ---");
            foreach (var student in query.ToList())
            {
                Console.WriteLine($"{student.FirstName} {student.LastName} (ID: {student.StudentId})");
            }
        }
    }
}
