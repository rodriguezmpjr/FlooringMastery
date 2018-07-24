using SWCCorp.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCCorp.BLL
{
    public static class StateTaxManagerFactory
    {
        public static StateTaxManager Create()
        {
            string mode = ConfigurationManager.AppSettings["ModeT"].ToString();

            switch (mode)
            {
                case "TestData":
                    return new StateTaxManager(new StateTaxTestRepository());
                case "LiveData":
                    return new StateTaxManager(new StateTaxFileRepository(@"C:\Data\Flooring\Taxes.txt"));
                case "TrainingMode":
                    return new StateTaxManager(new StateTaxFileRepository(@"C:\Data\Flooring\Training\Taxes.txt"));
                default:
                    throw new Exception("move this out to consoleIO");  //here temporarily, message to be sent from front end.
            }
        }
    }
}
