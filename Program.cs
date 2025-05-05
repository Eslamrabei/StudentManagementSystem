using System;
using System.Data.SqlClient;
using System.Data;

namespace StudentManagementSystem
{
    class Program
    {
        static string connectionString = "Server=localhost;Database=StudentDB;Trusted_Connection=True;";

        static void Main(string[] args)
        {
            while (true)
            {
               
                Console.WriteLine("Student Management System");
                Console.WriteLine("1. Add Student");
                Console.WriteLine("2. View Students");
                Console.WriteLine("3. Update Student");
                Console.WriteLine("4. Delete Student");
                Console.WriteLine("5. Exit");
                Console.Write("Choose an option: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddStudent();
                        break;
                    case "2":
                        ViewStudents();
                        break;
                    case "3":
                        UpdateStudent();
                        break;
                    case "4":
                        DeleteStudent();
                        break;
                    case "5":
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Try again.");
                        break;
                }
            }
        }

        static void AddStudent()
        {
            Console.Write("Enter name: ");
            string name = Console.ReadLine();
            Console.Write("Enter age: ");
            int age = int.Parse(Console.ReadLine());
            Console.Write("Enter grade: ");
            string grade = Console.ReadLine();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "INSERT INTO Students (Name, Age, Grade) VALUES (@Name, @Age, @Grade)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Parameters.AddWithValue("@Age", age);
                cmd.Parameters.AddWithValue("@Grade", grade);
                cmd.ExecuteNonQuery();
                Console.WriteLine("Student added successfully.");
            }
        }

        static void ViewStudents()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM Students";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                Console.WriteLine("ID\tName\tAge\tGrade");
                while (reader.Read())
                {
                    Console.WriteLine($"{reader["ID"]}\t{reader["Name"]}\t{reader["Age"]}\t{reader["Grade"]}");
                }
            }
        }

        static void UpdateStudent()
        {
            Console.Write("Enter ID of student to update: ");
            int id = int.Parse(Console.ReadLine());

            Console.Write("Enter new name: ");
            string name = Console.ReadLine();
            Console.Write("Enter new age: ");
            int age = int.Parse(Console.ReadLine());
            Console.Write("Enter new grade: ");
            string grade = Console.ReadLine();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "UPDATE Students SET Name = @Name, Age = @Age, Grade = @Grade WHERE ID = @ID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ID", id);
                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Parameters.AddWithValue("@Age", age);
                cmd.Parameters.AddWithValue("@Grade", grade);
                cmd.ExecuteNonQuery();
                Console.WriteLine("Student updated successfully.");
            }
        }

        static void DeleteStudent()
        {
            Console.Write("Enter ID of student to delete: ");
            int id = int.Parse(Console.ReadLine());

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "DELETE FROM Students WHERE ID = @ID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ID", id);
                cmd.ExecuteNonQuery();
                Console.WriteLine("Student deleted successfully.");
            }
        }
    }
    
}
