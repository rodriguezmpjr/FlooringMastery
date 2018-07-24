using SWCCorp.Models;
using SWCCorp.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCCorp.BLL
{
    public class ProductManager
    {
        private IProductsRepository _productsRepository;

        public ProductManager(IProductsRepository productsRepository)
        {
            _productsRepository = productsRepository;
        }

        public bool CheckIfProductIsAvailable(string productType)
        {
            return _productsRepository.LoadProducts().Any(p => p.ProductType == productType);
        }

        public decimal GetLaborCostPerSquareFoot(string productType)
        {
            var laborCost = _productsRepository.LoadProducts().Where(p => p.ProductType == productType).Select(l => l.LaborCostPerSquareFoot).First(); //.First??? Need to research this more. How can you return a regular decimal without .First()?

            return laborCost;
        }

        public List<Products> LoadProducts()
        {
            return _productsRepository.LoadProducts();
        }

        public decimal GetMaterialCostPerSquareFoot(string productType)
        {
            var materialCost = _productsRepository.LoadProducts().Where(p => p.ProductType == productType).Select(c => c.CostPerSquareFoot).First(); //.First??? Need to research this more

            return materialCost;
        }
    }
} 
