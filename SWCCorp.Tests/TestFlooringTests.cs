using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using SWCCorp.BLL;
using SWCCorp.Models;
using SWCCorp.Models.Responses;

namespace SWCCorp.Tests
{
    [TestFixture]
    public class TestFlooringTests
    {
        [Test]
        public void CanAddOrder()
        {
            OrderManager manager = OrderManagerFactory.Create();
            Order testOrder = new Order()
            {
                Date = new DateTime(2019, 04, 30),
                OrderNumber = 0,
                CustomerName = "McFly",
                State = "KY",
                TaxRate = 0,
                ProductType = "Carpet",
                Area = 100m,
                CostPerSquareFoot = 0,
                LaborCostPerSquareFoot = 0,
                MaterialCost = 0,
                LaborCost = 0,
                Tax = 0,
                Total = 0
            };
            OrderAddResponse response = manager.CreateNewOrder(testOrder);
            manager.SaveOrderToRepo(response);
            Order responseOrder = response.Order;
            Assert.NotNull(manager.ListAllOrdersByDate(testOrder.Date));
            //Assert.AreEqual(manager.ListAllOrdersByDate(testOrder.Date).Where(o => o.CustomerName == "McFly"), response.Order);
            Order findOrder = new Order() { Date = new DateTime(2019, 04, 30), OrderNumber = 101 };
            var tempOrder = manager.FindOrder(findOrder);
            Order orderInRepo = tempOrder.Order;
            Assert.AreEqual(orderInRepo, responseOrder);
        }
    }
}
