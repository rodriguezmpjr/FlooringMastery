using SWCCorp.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCCorp.BLL
{
    public static class ProductManagerFactory
    {
        public static ProductManager Create()
        {
            string mode = ConfigurationManager.AppSettings["ModeP"].ToString();

            switch (mode)
            {
                case "TestData":
                    return new ProductManager(new ProductTestRepository());
                case "LiveData":
                    return new ProductManager(new ProductFileRepository(@"C:\Data\Flooring\\Products.txt"));
                case "TrainingMode":
                    return new ProductManager(new ProductFileRepository(@"C:\Data\Flooring\Training\Products.txt"));
                default:
                    throw new Exception("move this out to consoleIO");  //here temporarily, message to be sent from front end.
            }
        }
    }
}
