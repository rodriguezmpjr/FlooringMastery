using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SWCCorp.Models.Interfaces;
using SWCCorp.Models;

namespace SWCCorp.Data
{
    public class StateTaxTestRepository : IStateTaxRepository
    {
        private List<StateTaxes> _states = new List<StateTaxes>()
        {
            new StateTaxes() { StateAbbreviation = "OH", StateName = "Ohio", TaxRate = 6.25m },
            new StateTaxes() { StateAbbreviation = "PA", StateName = "Pennsylvania", TaxRate = 6.75m },
            new StateTaxes() { StateAbbreviation = "KY", StateName = "Kentucky", TaxRate = 10.0m }
        };

        public List<StateTaxes> LoadStatesTaxes()
        {
            return _states;
        }
    }
}
