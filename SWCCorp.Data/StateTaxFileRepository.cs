using SWCCorp.Models;
using SWCCorp.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCCorp.Data
{
    public class StateTaxFileRepository : IStateTaxRepository
    {
        private string _filepath;

        public StateTaxFileRepository(string filepath)
        {
            _filepath = filepath;
        }

        public List<StateTaxes> LoadStatesTaxes()
        {
            if (File.Exists(_filepath))
            {
                List<StateTaxes> stateTaxList = new List<StateTaxes>();
                
                using (StreamReader sr = new StreamReader(_filepath))
                {
                    string headerLine = sr.ReadLine();
                    string line;

                    while ((line = sr.ReadLine()) != null)
                    {
                        StateTaxes stateTax = new StateTaxes();
                        string[] fields = line.Split(',');

                        stateTax.StateAbbreviation = fields[0];
                        stateTax.StateName = fields[1];
                        stateTax.TaxRate = decimal.Parse(fields[2]);

                        stateTaxList.Add(stateTax);
                    }

                }
                return stateTaxList;
            }
            else
                return null;
        }
    }
}
