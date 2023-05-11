using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System.Data;

namespace ORM_Dapper
{
    public class Program
    {
        private static object repo;

        static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            string connString = config.GetConnectionString("DefaultConnection");

            IDbConnection conn = new MySqlConnection(connString);
            var departmentRepo = new DapperDepartmentRepository(conn);

            // GET Departments
            var departments = departmentRepo.GetAllDepartments();

            foreach (var department in departments)
            {
                Console.WriteLine(department.DepartmentID);
                Console.WriteLine(department.Name);
                Console.WriteLine();
            }
            // END GET

            // INSERT Deparment
            Console.Write("Type a new department name: ");
            var newDepartment = Console.ReadLine();
            departmentRepo.InsertDepartment(newDepartment);
            // END INSERT

            foreach (var department in departments)
            {
                Console.WriteLine(department.Name);
            }
        }
    }
}
