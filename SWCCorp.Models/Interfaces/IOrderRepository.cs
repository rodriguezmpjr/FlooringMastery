using SWCCorp.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCCorp.Models.Interfaces
{
    public interface IOrderRepository
    {
        List<Order> LoadAllOrdersByDate(DateTime date);        
        void AddOrder(Order order);
        void EditOrder(Order original, Order revised);
        void Remove(Order order);
    }
}
