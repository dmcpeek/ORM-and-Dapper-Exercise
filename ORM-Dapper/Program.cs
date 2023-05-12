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

            #region Department   
            //var departmentRepo = new DapperDepartmentRepository(conn);
            //// GET Departments
            //var departments = departmentRepo.GetAllDepartments();

            //foreach (var department in departments)
            //{
            //    Console.WriteLine(department.DepartmentID);
            //    Console.WriteLine(department.Name);
            //    Console.WriteLine();
            //}
            //// END GET

            //// INSERT Department
            //Console.Write("Type a new department name: ");
            //var newDepartment = Console.ReadLine();
            //departmentRepo.InsertDepartment(newDepartment);
            //// END INSERT

            //// GET Departments
            //departments = departmentRepo.GetAllDepartments();

            //foreach (var department in departments)
            //{
            //    Console.WriteLine(department.DepartmentID);
            //    Console.WriteLine(department.Name);
            //    Console.WriteLine();
            //}
            //// END GET
            #endregion

            var productRepository = new DapperProductRepository(conn);

            var productToUpdate = productRepository.GetAllProducts(940);
            
            productRepository.UpdateProduct(productToUpdate);
            productToUpdate.Name = "UPDATED";
            productToUpdate.Price = 150.00;
            productToUpdate.StockLevel = 1;
            productToUpdate.CategoryID = 1;
            productToUpdate.OnSale = true;

            var products = productRepository.GetAllProducts();
            foreach (var product in products) 
            {
                Console.WriteLine(product.ProductID);
                Console.WriteLine(product.Name);
                Console.WriteLine(product.Price);
                Console.WriteLine(product.CategoryID);
                Console.WriteLine(product.OnSale);
                Console.WriteLine(product.StockLevel);
                Console.WriteLine();
            }

        }
    }
}
