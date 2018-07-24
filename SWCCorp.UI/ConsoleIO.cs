using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SWCCorp.Models;
using SWCCorp.Models.Responses;

namespace SWCCorp.UI
{
    public class ConsoleIO
    {
        public static Order GetOrderInfoFromCustomer(bool isEdit, Order original = null)
        {
            Order newOrder = new Order();
            bool editComplete = false;

            while (!editComplete)
            {
                if (isEdit)
                {
                    newOrder.OrderNumber = original.OrderNumber;
                    Console.Write($"Editing Name: Current name is {original.CustomerName}. Please enter new Name. ");
                }
                Console.Write("Enter customer name: ");
                string tempName = Console.ReadLine();
                tempName = tempName.Replace(",", "");
                tempName = tempName.Replace(".", "");
                newOrder.CustomerName = tempName;
                if (isEdit)
                {
                    if (newOrder.CustomerName == "")
                    {
                        newOrder.CustomerName = original.CustomerName;
                    }
                }

                // Edit name not part of spec but code is below
                //if (isEdit)
                //{
                //    Console.WriteLine($"Editing install date: Current date is {original.Date}. Please enter new Install Date. ");
                //}
                if (isEdit)
                {
                    newOrder.Date = original.Date;
                }
                else
                {
                    do
                    {
                        string inputDate;
                        DateTime date;
                        do
                        {
                            Console.Write("Enter desired install date: ");
                            inputDate = Console.ReadLine();
                            if (!DateTime.TryParse(inputDate, out date) || date < DateTime.Now)
                            {
                                Console.WriteLine();
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Date must be today or in the future. (mm/dd/yy)");
                                Console.ResetColor();
                            }
                        } while (!DateTime.TryParse(inputDate, out date)); // && date < DateTime.Now); <--why didn't this work?

                        newOrder.Date = date;

                    } while (newOrder.Date < DateTime.Now);
                    

                    //if (date < DateTime.Now)
                    //{
                    //    Console.WriteLine("Can't Time Travel, Sorry!");
                    //    Console.ReadLine();
                    //}
                    //newOrder.Date = date;
                }
                

                if (isEdit)
                {
                    Console.WriteLine($"Editing State: Current state is {original.State}. Please enter new State. ");
                }
                Console.Write("Please provide the state of project location: ");
                newOrder.State = Console.ReadLine().ToUpper();
                if (isEdit)
                {
                    if (newOrder.State == "")
                    {
                        newOrder.State = original.State;
                    }
                }

                if (isEdit)
                {
                    Console.WriteLine($"Editing Product Type: Current product is {original.ProductType}. Please enter new product type. ");
                }
                Console.Write("Please provide the desired flooring material: ");
                newOrder.ProductType = Console.ReadLine();
                if (isEdit)
                {
                    if (newOrder.ProductType == "")
                    {
                        newOrder.ProductType = original.ProductType;
                    }
                }

                if (isEdit)
                {
                    string inputEdit;
                    decimal outputEdit;
                    Console.WriteLine($"Editing Area: Current Area is {original.Area}sq/ft. Please enter new area. ");
                    Console.Write("Please provide the area: ");
                    inputEdit = Console.ReadLine();
                    if (decimal.TryParse(inputEdit, out outputEdit))
                    {
                        newOrder.Area = outputEdit;
                        editComplete = true;
                    }
                    else
                    {
                        newOrder.Area = original.Area;
                    }
                    return newOrder;
                }                
                else
                {
                    string input;
                    decimal output;
                    do
                    {
                        Console.Write("Please provide the area: ");
                        input = Console.ReadLine();
                    } while (!decimal.TryParse(input, out output));
                    newOrder.Area = output;
                }
                editComplete = true;
            }
            return newOrder;
        }

        public static void DisplayTrainingModeMessage()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("--------------------TRAINING MODE--------------------");
            Console.ResetColor();
            Console.WriteLine();
            PressAnyKeyToContinue();
            Console.Clear();
        }

        public static void DisplayOrderRemovedSuccessfully()
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Order has been successfully edited.");
            Console.ResetColor();           
            PressAnyKeyToContinue();
        }

        public static void DisplayNegativeResponseMesssage(string message)
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message); 
            Console.ResetColor();           
            ConsoleIO.PressAnyKeyToContinue();
        }

        public static void DisplayPositiveResponseMesssage(string message)
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(message);
            Console.ResetColor();            
            ConsoleIO.PressAnyKeyToContinue();
        }

        internal static void DisplayListofStates(List<StateTaxes> listOfStates)
        {
            string line = "{0,-25} {1,-15} {2,-15}"; 
            Console.WriteLine(line, "Abbreviation", "State", "Tax Rate");
            Console.WriteLine("===============================================================");

            foreach (var state in listOfStates)
            {
                Console.WriteLine(line, state.StateAbbreviation, state.StateName, state.TaxRate);
            }
            Console.WriteLine("===============================================================");
            Console.WriteLine();
        }

        public static void DisplayListOfProducts(List<Products> listOfProducts)
        {
            string line = "{0,-25} {1,-15:c} {2,-15:c}"; 
            Console.WriteLine(line, "Product Type", "Per Sq/Ft", "Cost Per Sq/Ft");
            Console.WriteLine("===============================================================");

            foreach (var product in listOfProducts)
            {
                Console.WriteLine(line, product.ProductType, product.CostPerSquareFoot, product.LaborCostPerSquareFoot);
            }
            Console.WriteLine("===============================================================");
            Console.WriteLine();
        }

        public static void PressAnyKeyToContinue()
        {
            Console.WriteLine();
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        public static void DisplayOrderEditCancelled()
        {
            Console.WriteLine();
            Console.WriteLine("Edit order has been cancelled.");
            PressAnyKeyToContinue();
        }

        public static void DisplayOrderAddedSuccessfully()
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Order has been successfully edited.");
            Console.ResetColor();
            PressAnyKeyToContinue();
        }

        internal static Order GetOrderToBeRemovedOrEditedInfo()
        {
            Order orderToFind = new Order();                  
            string inputDate;
            string inputOrderNumber;
            int num;
            DateTime date;

            do
            {
                Console.Write("Enter install date: ");
                inputDate = Console.ReadLine();
            } while (!DateTime.TryParse(inputDate, out date));
            orderToFind.Date = date;

            do
            {
                Console.Write("Enter order number: ");
                inputOrderNumber = Console.ReadLine();
            } while (!int.TryParse(inputOrderNumber, out num));
            orderToFind.OrderNumber = num;

            return orderToFind;
        }

        public static void DisplayListOfOrders(List<Order> listOfOrders)
        {
            while (true)
            {
                if (listOfOrders == null)
                {
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"There are no orders for that day.");
                    Console.ResetColor();
                    break;
                }

                Console.Clear();
                string line = "{0,-7} {1,-10} {2,-15} {3,-10} {4,12:c}";
                Console.WriteLine(line, "Order", "Install", "", "", "");
                Console.WriteLine(line, "Number", "Date", "Name", "Product", "Total");
                Console.WriteLine("==========================================================");

                foreach (var o in listOfOrders)
                {
                    Console.WriteLine(line, o.OrderNumber, o.Date.ToShortDateString(), o.CustomerName, o.ProductType, o.Total);
                }
                Console.WriteLine("==========================================================");
                Console.WriteLine();


                //foreach (var orders in listOfOrders)
                //{
                //    Console.WriteLine();
                //    Console.WriteLine($"[{ orders.OrderNumber}] | [{orders.Date.ToShortDateString()}]");
                //    Console.WriteLine($"[{orders.CustomerName }]");
                //    Console.WriteLine($"Total: [{orders.Total}]");
                //    Console.WriteLine();
                //    Console.WriteLine("**************************************");
                //}
                break;
            }
        }

        public static DateTime GetInstallDateFromCustomer()
        {
            Console.Write("Please enter install date: ");
            string dateInput = Console.ReadLine();
            DateTime installDate = DateTime.Parse(dateInput);

            return installDate;
        }

        public static bool DisplayOrderToBeConfirmed(OrderAddResponse response)
        {
            //display all order details and then ask if they want to proceed.  If yes then return true, else return false and return to menu
            Console.Clear();
            Console.WriteLine("Below is the order created.");
            Console.WriteLine();
            Console.WriteLine($"[{ response.Order.OrderNumber}] | [{response.Order.Date.ToShortDateString()}]"); // add response.Order.Date after date is added to order model
            Console.WriteLine($"[{response.Order.CustomerName }]");
            Console.WriteLine($"[{response.Order.State}]");
            Console.WriteLine($"Product: [{response.Order.ProductType}]");
            Console.WriteLine($"Materials: [{response.Order.MaterialCost:c}]");
            Console.WriteLine($"Labor: [{response.Order.LaborCost:c}]");
            Console.WriteLine($"Tax: [{response.Order.Tax:c}]");
            Console.WriteLine($"Total: [{response.Order.Total:c}]");
            Console.WriteLine();

            Console.Write("Confirm Order: Y or N: ");
            string answer = Console.ReadLine();

            if (answer == "Y" || answer =="y")
            {
                return true;
            }
            else
                return false;
        }

        public static char DisplayMenu()
        {
            Console.WriteLine("************************************************************************************");
            Console.WriteLine("*                                                                                  *");
            Console.WriteLine("*  1. Display Orders                                                               *");
            Console.WriteLine("*  2. Add an Order                                                                 *");
            Console.WriteLine("*  3. Edit an Order                                                                *");
            Console.WriteLine("*  4. Remove an Order                                                              *");
            Console.WriteLine("*  5. Quit                                                                         *");
            Console.WriteLine("*                                                                                  *");
            Console.WriteLine("************************************************************************************");
            Console.WriteLine();
            Console.Write("Please choose and option: ");

            char entry = Console.ReadKey().KeyChar;
            Console.Clear();
            return entry;
        }
    }
}
