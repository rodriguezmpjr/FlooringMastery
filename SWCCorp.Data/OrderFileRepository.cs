using SWCCorp.Models;
using SWCCorp.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SWCCorp.Data
{
    public class OrderFileRepository : IOrderRepository
    {
        private string _filepath;
        public List<Order> OrdersByDate { get; set; }

        public OrderFileRepository(string filepath)
        {
            _filepath = filepath;
        }

        public void AddOrder(Order order)
        {
            string completeFilePath = ($@"C:\Data\Flooring\Orders_{order.Date.ToString("MMddyyyy")}.txt");
            if (!File.Exists(completeFilePath))
            {
                List<Order> newList = new List<Order>();
                newList.Add(order);                
                using (StreamWriter sw = new StreamWriter(completeFilePath))
                {
                    sw.WriteLine("OrderNumber, CustomerName, State, TaxRate, ProductType, Area, CostPerSquareFoot, LaborCostPerSquareFoot, MaterialCost, LaborCost, Tax, Total");
                    sw.WriteLine($"{order.OrderNumber},{order.CustomerName},{order.State},{order.TaxRate},{order.ProductType},{order.Area},{order.CostPerSquareFoot:F},{order.LaborCostPerSquareFoot:F},{order.MaterialCost:F},{order.LaborCost:F},{order.Tax:F},{order.Total:F}");
                }
            }
            else
            {
                File.AppendAllText(completeFilePath, $"{order.OrderNumber},{order.CustomerName},{order.State},{order.TaxRate},{order.ProductType},{order.Area},{order.CostPerSquareFoot:F},{order.LaborCostPerSquareFoot:F},{order.MaterialCost:F},{order.LaborCost:F},{order.Tax:F},{order.Total:F}" + Environment.NewLine);
            }
        }

        public void EditOrder(Order original, Order revised)
        {
            Remove(original);
            AddOrder(revised);
        }

        public List<Order> LoadAllOrdersByDate(DateTime date)
        {
            //Path.Combine(@"C:\Data\Flooring\SampleData\", $"Orders_{ date.ToString("MMddyyyy")}.txt")

            string completeFilePath = ($@"C:\Data\Flooring\Orders_{date.ToString("MMddyyyy")}.txt");

            if (File.Exists(completeFilePath))
            {
                List<Order> listfromFile = new List<Order>();
                using (StreamReader sr = new StreamReader($@"{_filepath}\Orders_{date.ToString("MMddyyyy")}.txt"))
                {
                    string headerLine = sr.ReadLine();
                    string line;
                    decimal taxRate, area, costPerSquareFoot, laborCostPerSquareFoot, materialCost, laborCost, tax, total;
                    
                    while ((line = sr.ReadLine()) != null)
                    {
                        Order orderFromFile = new Order();
                        
                        string[] fields = line.Split(',');

                        int orderNumber;

                        orderNumber = int.Parse(fields[0]);
                        orderFromFile.OrderNumber = orderNumber;

                        orderFromFile.CustomerName = fields[1]; //Regex.Split(fields[1], ",(?=(?:[^']*'[^']*')*[^']*$)").ToString();
                        orderFromFile.State = fields[2];

                        taxRate = decimal.Parse(fields[3]);
                        orderFromFile.TaxRate = taxRate;

                        orderFromFile.ProductType = fields[4];

                        area = decimal.Parse(fields[5]);
                        orderFromFile.Area = area;

                        costPerSquareFoot = decimal.Parse(fields[6]);
                        orderFromFile.CostPerSquareFoot = costPerSquareFoot;

                        laborCostPerSquareFoot = decimal.Parse(fields[7]);
                        orderFromFile.LaborCostPerSquareFoot = laborCostPerSquareFoot;

                        materialCost = decimal.Parse(fields[8]);
                        orderFromFile.MaterialCost = materialCost;

                        laborCost = decimal.Parse(fields[9]);
                        orderFromFile.LaborCost = laborCost;

                        tax = decimal.Parse(fields[10]);
                        orderFromFile.Tax = tax;

                        total = decimal.Parse(fields[11]);
                        orderFromFile.Total = total;

                        orderFromFile.Date = date;

                        listfromFile.Add(orderFromFile);                        
                    }
                }
                return listfromFile;
            }
            else
                return null;
        }

        public void Remove(Order order)
        {
            try
            {
                string completeFilePath = ($@"C:\Data\Flooring\Orders_{order.Date.ToString("MMddyyyy")}.txt");
                List<Order> newList = LoadAllOrdersByDate(order.Date);

                var orderToRemove = newList.SingleOrDefault(o => o.OrderNumber == order.OrderNumber);
                if (orderToRemove != null)
                    newList.Remove(orderToRemove);

                if (File.Exists(completeFilePath))
                    File.Delete(completeFilePath);
                using (StreamWriter sw = new StreamWriter(completeFilePath))
                {
                    sw.WriteLine("OrderNumber, CustomerName, State, TaxRate, ProductType, Area, CostPerSquareFoot, LaborCostPerSquareFoot, MaterialCost, LaborCost, Tax, Total");

                    foreach (var o in newList)
                    {
                        sw.WriteLine($"{o.OrderNumber},{o.CustomerName},{o.State},{o.TaxRate},{o.ProductType},{o.Area},{o.CostPerSquareFoot:F},{o.LaborCostPerSquareFoot:F},{o.MaterialCost:F},{o.LaborCost:F},{o.Tax:F},{o.Total:F}");
                    }
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine($"An error occured deleting the file: {ex.Message}");
                Console.WriteLine($"The error happened at: {ex.StackTrace}");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }            
        }
    }
}
