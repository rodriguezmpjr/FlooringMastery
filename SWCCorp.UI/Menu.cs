using SWCCorp.UI.WorkFlows;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SWCCorp.UI
{
    public class Menu
    {
        public void Start()
        {
            while (true)
            {
                Console.Clear();

                string mode = ConfigurationManager.AppSettings["Mode"].ToString();
                if (mode == "TrainingMode")
                    ConsoleIO.DisplayTrainingModeMessage();

                //Console.Clear();
                char keyPressed = ConsoleIO.DisplayMenu();                

                switch (keyPressed)
                {
                    case '1':
                        DisplayOrdersWorkflow displayWorkflow = new DisplayOrdersWorkflow();
                        displayWorkflow.Execute();
                        break;
                    case '2':
                        AddOrderWorkflow addWorkflow = new AddOrderWorkflow();
                        addWorkflow.Execute();
                        break;
                    case '3':
                        EditOrderWorkflow editWorkflow = new EditOrderWorkflow();
                        editWorkflow.Execute();
                        break;
                    case '4':
                        RemoveOrderWorkflow removeWorkflow = new RemoveOrderWorkflow();
                        removeWorkflow.Execute();
                        break;
                    case '5':
                        return;
                    default:
                        break;
                }
            }
        }
    }
}
