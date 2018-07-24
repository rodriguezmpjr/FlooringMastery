using SWCCorp.Models;
using SWCCorp.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCCorp.Data
{
    public class ProductTestRepository : IProductsRepository
    {
        private List<Products> _products = new List<Products>()
        {
            new Products() { ProductType = "Carpet", CostPerSquareFoot = 2.25m, LaborCostPerSquareFoot = 2.10m },
            new Products() { ProductType = "Tile", CostPerSquareFoot = 3.50m, LaborCostPerSquareFoot = 4.15m },
            new Products() { ProductType = "Gold", CostPerSquareFoot = 999.99m, LaborCostPerSquareFoot = 99.99m },
        };

        public List<Products> LoadProducts()
        {
            return _products;
        }
    }
}
