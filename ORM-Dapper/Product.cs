using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM_Dapper
{
    public class Product
    {
        internal object category;
        internal object name;

        public int ProductID { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public bool OnSale { get; set; }
        public int StockLevel { get; set; }
        public int CategoryID { get; set; }
    }
}
