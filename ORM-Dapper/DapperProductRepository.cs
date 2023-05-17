using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ORM_Dapper
{
    public class DapperProductRepository : IProductRepository
    {
        private readonly IDbConnection _connection;
        private object category;

        public DapperProductRepository(IDbConnection connection)
        {
            _connection = connection;
        }
        public void CreateProduct(string name, double price, int category)
        {
            _connection.Execute("INSERT INTO products (Name, Price, categoryID) VALUES (@name, @price, @category);",
            new { name = name, price = price, category = category });
        }

        public void DeleteProduct(int id)
        {
            _connection.Execute("DELETE from products WHERE productId = @id;", new { id = id });
            _connection.Execute("DELETE from sales WHERE productId = @id;", new { id = id });
            _connection.Execute("DELETE from reviews WHERE productId = @id;", new { id = id });
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _connection.Query<Product>("SELECT * FROM products;");
        }

        public Product GetProduct(int id)
        {
            return _connection.QuerySingle<Product>("SELECT * FROM products WHERE ProductID = @id;", new { id = id });
        }

        public void UpdateProduct(Product product)
        {
            _connection.Execute("UPDATE products SET Name = @name, Price = @price WHERE ProductID = @id;",
            new { name = product.Name, price = product.Price, id = product.ProductID });
        }
    }
}