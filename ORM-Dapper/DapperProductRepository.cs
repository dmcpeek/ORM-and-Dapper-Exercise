using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM_Dapper
{
    public class DapperProductRepository : IProductRepository
    {
        private readonly IDbConnection _conn;

        public DapperProductRepository(IDbConnection conn)
        {
            _conn = conn;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _conn.Query<Product>("SELECT * FROM products;");
        }

        public Product GetAllProducts(int id)
        {
            return _conn.QuerySingle<Product>("SELECT * FROM products WHERE ProductID = @id;", new { id = id });
        }

        public void UpdateProduct(Product product)
        {
            _conn.Execute("UPDATE products" +
                            " SET Name = @name," +
                            " Price = @price," +
                            " CategoryID = @categoryid," +
                            " OnSale = @onsale," +
                            " StockLevel = @stocklevel" +
                            " WHERE ProductID = @id;",
                            new
                            {
                                id = product.ProductID,
                                name = product.Name,
                                price = product.Price,
                                categoryid = product.CategoryID,
                                onsale = product.OnSale,
                                stocklevel = product.StockLevel
                            });
        }
    }
}