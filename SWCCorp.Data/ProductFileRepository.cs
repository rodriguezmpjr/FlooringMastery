using SWCCorp.Models;
using SWCCorp.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCCorp.Data
{
    public class ProductFileRepository : IProductsRepository
    {
        private string _filepath;

        public ProductFileRepository(string filepath)
        {
            _filepath = filepath;
        }

        public List<Products> LoadProducts()
        {
            if (File.Exists(_filepath))
            {
                List<Products> productsList = new List<Products>();

                using (StreamReader sr = new StreamReader(_filepath))
                {
                    string headerLine = sr.ReadLine();
                    string line;

                    while ((line = sr.ReadLine()) != null)
                    {
                        Products product = new Products();

                        string[] fields = line.Split(',');

                        product.ProductType = (fields[0]);
                        product.CostPerSquareFoot = decimal.Parse(fields[1]);
                        product.LaborCostPerSquareFoot = decimal.Parse(fields[2]);

                        productsList.Add(product);
                    }

                }
                return productsList;
            }
            else
                return null;
        }
    }
}
