using SWCCorp.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SWCCorp.Models.Responses;
using SWCCorp.Models;
using System.Configuration;

namespace SWCCorp.UI
{
    public class AddOrderWorkflow
    {
        public void Execute()
        {
            OrderManager manager = OrderManagerFactory.Create();            
            ConsoleIO.DisplayListOfProducts(manager.ListAllProducts());
            ConsoleIO.DisplayListofStates(manager.ListAllStates());                       

            OrderAddResponse response = manager.CreateNewOrder(ConsoleIO.GetOrderInfoFromCustomer(false));            

            if (!response.Success)                          
                ConsoleIO.DisplayNegativeResponseMesssage(response.Message);
            else
            {
                if (ConsoleIO.DisplayOrderToBeConfirmed(response))
                {
                    ConsoleIO.DisplayPositiveResponseMesssage(response.Message);
                    manager.SaveOrderToRepo(response);                
                }

            }            
        }
    }
}
