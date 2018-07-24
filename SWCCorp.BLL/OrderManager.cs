using SWCCorp.Models;
using SWCCorp.Models.Interfaces;
using SWCCorp.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCCorp.BLL
{
    public class OrderManager
    {
        private IOrderRepository _orderRepository;       

        ProductManager productManager = ProductManagerFactory.Create();
        StateTaxManager stateTaxManager = StateTaxManagerFactory.Create();

        public OrderManager(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;            
        }

        public List<StateTaxes> ListAllStates()
        {
            return stateTaxManager.LoadStates();
        }

        public List<Products> ListAllProducts()
        {
            return productManager.LoadProducts();
        }

        public OrderAddResponse FindOrder(Order order)
        {
            int orderNumber = order.OrderNumber;
            OrderAddResponse response = new OrderAddResponse();
            List<Order> newList = new List<Order>();
            newList = _orderRepository.LoadAllOrdersByDate(order.Date);
            response.Order = newList.Find(o => o.OrderNumber == orderNumber);

            if (response.Order == null)
            {
                response.Success = false;
                response.Message = $"Order not found.";
                return response;
            }
            else
                response.Success = true;
            return response;
        }

        public void EditOrder(Order original, Order edited)
        {
            OrderAddResponse response = CreateNewOrder(edited);

            var editedOrder = response.Order;

            _orderRepository.EditOrder(original, editedOrder);
        }

        public void RemoveOrder(Order orderToBeRemoved)
        {
            _orderRepository.Remove(orderToBeRemoved);
        }        

        public void SaveOrderToRepo(OrderAddResponse response)
        {
            _orderRepository.AddOrder(response.Order);
        }

        public OrderAddResponse CreateNewOrder(Order order)
        {
            OrderAddResponse response = new OrderAddResponse();

            response.Order = order;
            
            if (stateTaxManager.CheckIfStateIsServiced(order.State))
            {
                response.Success = true;
                response.Order.State = order.State;                
            }            
            else
            {
                response.Success = false;
                response.Message = $"We do not service {order.State} at this time.";
                return response;
            }
            
            response.Order.TaxRate = stateTaxManager.GetTaxRate(order.State);
            
            if (productManager.CheckIfProductIsAvailable(order.ProductType))
            {
                response.Success = true;
                response.Order.ProductType = order.ProductType;
            }            
            else
            {
                response.Success = false;
                response.Message = $"{order.ProductType} is not an available product. Please reselect.";
                return response;
            }
            
            response.Order.CostPerSquareFoot = productManager.GetMaterialCostPerSquareFoot(order.ProductType);

            response.Order.LaborCostPerSquareFoot = productManager.GetLaborCostPerSquareFoot(order.ProductType);
                        
            if (order.Area >= 100)
            {
                response.Success = true;
                response.Order.Area = order.Area;
            }   
            else
            {
                response.Success = false;
                response.Message = "100 square feet is the minimum";
                return response;
            }

            response.Order.MaterialCost = response.Order.Area * response.Order.CostPerSquareFoot;
            response.Order.LaborCost = response.Order.Area * response.Order.LaborCostPerSquareFoot;

            response.Order.Tax = response.Order.MaterialCost + response.Order.LaborCost * (response.Order.TaxRate / 100);
            response.Order.Total = response.Order.MaterialCost + response.Order.LaborCost + response.Order.Tax;

            if (_orderRepository.LoadAllOrdersByDate(response.Order.Date) == null)
            {
                response.Order.OrderNumber = 101;
                
            }
            else
            {
                if (order.OrderNumber != 0)
                    response.Order.OrderNumber = order.OrderNumber;
                else
                {
                    response.Order.OrderNumber = _orderRepository.LoadAllOrdersByDate(response.Order.Date).Select(n => n.OrderNumber).Max() + 1;
                }                
            }
            response.Success = true;
            response.Message = "The order has been created/edited. ";
            return response;            
        }

        public List<Order> ListAllOrdersByDate(DateTime date)
        {
            return _orderRepository.LoadAllOrdersByDate(date);   
        }
    }
}
