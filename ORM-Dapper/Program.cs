using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic;
using MySql.Data.MySqlClient;
using System.ComponentModel;
using System.Data;

namespace ORM_Dapper
{
    public class Program
    {
        private static object repo;
        private static object instance;

        static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            string connString = config.GetConnectionString("DefaultConnection");

            IDbConnection conn = new MySqlConnection(connString);

            #region Department   
            var departmentRepo = new DapperDepartmentRepository(conn);
            // GET Departments
            var departments = departmentRepo.GetAllDepartments();

            foreach (var department in departments)
            {
                Console.Write($"{department.DepartmentID} {department.Name}");
                Console.WriteLine();
            }
            // END GET

            // INSERT Department
            Console.Write("Type a new department name: ");
            var newDepartment = Console.ReadLine();
            departmentRepo.InsertDepartment(newDepartment);
            // END INSERT

            // GET Departments
            departments = departmentRepo.GetAllDepartments();

            foreach (var department in departments)
            {
                Console.Write($"{department.DepartmentID} {department.Name}");
                Console.WriteLine();
            }
            // END GET
            #endregion

            #region Product
            var productRepository = new DapperProductRepository(conn);

            Console.WriteLine("Add some new stuff to sell");
            Console.WriteLine("--------------------------");
            Console.Write("Enter product name: ");
            var newName = Console.ReadLine();
            Console.Write("Enter product price: ");
            var newPrice = double.Parse(Console.ReadLine());
            Console.Write("Enter category (1 - 4): ");
            var newCategory = int.Parse(Console.ReadLine());
            productRepository.CreateProduct(newName, newPrice, newCategory);

            var collection = productRepository.GetAllProducts();
            foreach (var item in collection)
            {
                Console.WriteLine(item.ProductID);
                Console.WriteLine(item.Name);
                Console.WriteLine(item.Price);
                Console.WriteLine(item.CategoryID);
            }

            var productToUpdate = productRepository.GetProduct(1);
            productToUpdate.Name = "Nikon Z12";
            productToUpdate.Price = 10000;

            productRepository.UpdateProduct(productToUpdate);

            var prodCollection = productRepository.GetAllProducts();
            foreach (var item in prodCollection)
            {
                Console.WriteLine(item.ProductID);
                Console.WriteLine(item.Name);
                Console.WriteLine(item.Price);
                Console.WriteLine();
            }

            productRepository.DeleteProduct(1);
            #endregion

        }
    }
}
