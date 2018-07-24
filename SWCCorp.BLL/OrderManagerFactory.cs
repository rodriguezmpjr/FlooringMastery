using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using SWCCorp.Data;

namespace SWCCorp.BLL
{
    public static class OrderManagerFactory
    {
        public static OrderManager Create()
        {
            string mode = ConfigurationManager.AppSettings["Mode"].ToString();

            switch (mode)
            {
                case "TestData":
                    return new OrderManager(new OrderTestRepository());
                case "LiveData":
                    return new OrderManager(new OrderFileRepository(@"C:\Data\Flooring\"));
                case "TrainingMode":
                    return new OrderManager(new OrderFileRepository(@"C:\Data\Flooring\Training\"));
                default:
                    throw new Exception("move this out to consoleIO");  //here temporarily, message to be sent from front end.
            }
        }
    }
}
