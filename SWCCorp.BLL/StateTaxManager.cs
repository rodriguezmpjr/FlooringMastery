using SWCCorp.Models;
using SWCCorp.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCCorp.BLL
{
    public class StateTaxManager
    {
        private IStateTaxRepository _stateTaxRepository;

        public StateTaxManager(IStateTaxRepository stateTaxRepository)
        {
            _stateTaxRepository = stateTaxRepository;
        }

        public bool CheckIfStateIsServiced(string state)
        {
            return _stateTaxRepository.LoadStatesTaxes().Any(s => s.StateAbbreviation == state);
        }

        public decimal GetTaxRate(string state)
        {
            var getTaxRate = _stateTaxRepository.LoadStatesTaxes().Where(s => s.StateAbbreviation == state).Select(t => t.TaxRate).First();

            return getTaxRate;
        }

        public List<StateTaxes> LoadStates()
        {
            return _stateTaxRepository.LoadStatesTaxes();
        }
    }
}
