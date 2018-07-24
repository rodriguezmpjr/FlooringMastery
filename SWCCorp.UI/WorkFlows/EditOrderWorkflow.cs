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
    public class EditOrderWorkflow
    {
        public void Execute()
        {
            OrderManager manager = OrderManagerFactory.Create();
            Order originalOrder = ConsoleIO.GetOrderToBeRemovedOrEditedInfo();
            Order editedOrder = new Order();
            OrderAddResponse response = manager.FindOrder(originalOrder);   //Could be refactored by using the ConsoleIO request creating originalOrder as the parameter.  ****Ask Alan his thoughts?****  AddOrderWorkflow is refactor a little more.

            if (!response.Success)
                ConsoleIO.DisplayNegativeResponseMesssage(response.Message);            
            else
            {
                if (ConsoleIO.DisplayOrderToBeConfirmed(response))
                {
                    originalOrder = response.Order;
                    editedOrder = ConsoleIO.GetOrderInfoFromCustomer(true, originalOrder);
                    manager.EditOrder(originalOrder, editedOrder);
                    ConsoleIO.DisplayOrderAddedSuccessfully();
                }
                else
                    ConsoleIO.DisplayOrderEditCancelled();
            }
        }
    }
}
