using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM_Dapper
{
    public interface IProductRepository
    {
        IEnumerable <Product> GetAllProducts();
        public Product GetProduct(int id);
        public void CreateProduct(string name, double price, int category);
        public void UpdateProduct(Product product);
        public void DeleteProduct(int id);

    }
}
