using SWCCorp.BLL;
using SWCCorp.Models;
using SWCCorp.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCCorp.UI.WorkFlows
{
    public class RemoveOrderWorkflow
    {
        public void Execute()
        {
            OrderManager manager = OrderManagerFactory.Create();            

            OrderAddResponse response = manager.FindOrder(ConsoleIO.GetOrderToBeRemovedOrEditedInfo());

            if (!response.Success)
                ConsoleIO.DisplayNegativeResponseMesssage(response.Message);

            //orderConfirmed = ;

            if (ConsoleIO.DisplayOrderToBeConfirmed(response))
            {
                manager.RemoveOrder(response.Order);
                ConsoleIO.DisplayOrderRemovedSuccessfully();
            }
                
        }
    }
}
