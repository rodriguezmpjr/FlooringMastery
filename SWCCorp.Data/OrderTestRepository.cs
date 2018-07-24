using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SWCCorp.Models;
using SWCCorp.Models.Interfaces;
using SWCCorp.Models.Responses;

namespace SWCCorp.Data
{
    public class OrderTestRepository : IOrderRepository
    {
               
        public static Dictionary<DateTime, List<Order>> OrderFiles;

        public OrderTestRepository()
        {
            Order _order = new Order()
            {
                Date = new DateTime(1998, 04, 30),
                OrderNumber = 999,
                CustomerName = "McFly",
                State = "KY",
                TaxRate = 10.0m,
                ProductType = "Carpet",
                Area = 100m,
                CostPerSquareFoot = 3.0m,
                LaborCostPerSquareFoot = 4.0m,
                MaterialCost = 300m,
                LaborCost = 400m,
                Tax = 70m,
                Total = 770m
            };

            List<Order> _listOfOrders = new List<Order>() { _order };

            OrderFiles = new Dictionary<DateTime, List<Order>>()
            {
                { _order.Date, _listOfOrders }
            };

        }


        //OrderFiles.Add(_order.Date, _listOfOrders);
        public void AddOrder(Order order)
        {
            if (!OrderFiles.ContainsKey(order.Date))
            {                
                List<Order> newList = new List<Order>();
                newList.Add(order);
                OrderFiles.Add(order.Date, newList);
            }
            else
            {
                //modify
                OrderFiles[order.Date].Add(order);
                //List<Order> tempList = new List<Order>();
                //tempList.Add(order);
                //OrderFiles.Add(order.Date, tempList);
            }
                

            //}
            //_listOfOrders.Add(order);
            //OrderFiles.Add(order.Date, _listOfOrders);
        }

        public void EditOrder(Order original, Order revised)
        {
            Remove(original);
            AddOrder(revised);
        }

        public List<Order> LoadAllOrdersByDate(DateTime date)
        {
            if (OrderFiles.ContainsKey(date))
            {
                return OrderFiles[date];
            }
            else
                return null;
        }

        public void Remove(Order order)
        {
            List<Order> newList = LoadAllOrdersByDate(order.Date);
            newList.Remove(order);            
        }

        //public Order LoadOrder(int OrderNumber)
        //{
        //    return _order;
        //    //if (OrderNumber == _order.OrderNumber)
        //    //    return _order;
        //    //else
        //    //    return null;                       
        //}

    }
}
