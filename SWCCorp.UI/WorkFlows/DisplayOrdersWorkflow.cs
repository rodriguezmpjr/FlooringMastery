using SWCCorp.BLL;
using SWCCorp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCCorp.UI
{
    public class DisplayOrdersWorkflow
    {
        public void Execute()
        {
            OrderManager manager = OrderManagerFactory.Create();

            DateTime installDate = ConsoleIO.GetInstallDateFromCustomer();

            List<Order> listOfOrders = manager.ListAllOrdersByDate(installDate);
            ConsoleIO.DisplayListOfOrders(listOfOrders);
            Console.ReadLine();
        }
        
    }
}
