using NUnit.Framework;
using SWCCorp.BLL;
using SWCCorp.Models;
using SWCCorp.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCCorp.Tests
{
    [TestFixture]
    public class FileFlooringTests
    {
        [Test]
        public void CanAddOrder()
        {
            OrderManager manager = OrderManagerFactory.Create();
            Order cleanInputs = new Order() { CustomerName = "Doyle Hargrove", State =  "TN", ProductType = "Wood", Area = 100};
            Order badStateInput = new Order() { CustomerName = "Doyle Hargrove", State = "FR", ProductType = "Wood", Area = 100 };
            Order badProductInput = new Order() { CustomerName = "Doyle Hargrove", State = "TN", ProductType = "Gold", Area = 100 };
            Order badAreaInput = new Order() { CustomerName = "Doyle Hargrove", State = "TN", ProductType = "Wood", Area = 50 };

            OrderAddResponse response = manager.CreateNewOrder(cleanInputs);
            Assert.AreEqual(true, response.Success);
            OrderAddResponse responseBadState = manager.CreateNewOrder(badStateInput);
            Assert.AreEqual(false, responseBadState.Success);
            OrderAddResponse responseBadProduct = manager.CreateNewOrder(badProductInput);
            Assert.AreEqual(false, responseBadProduct.Success);
            OrderAddResponse responseBadArea = manager.CreateNewOrder(badAreaInput);
            Assert.AreEqual(false, responseBadArea.Success);
        }         
    }
}
