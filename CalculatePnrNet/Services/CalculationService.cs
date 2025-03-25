using Peleg.CalculatePnrNet.Data;
using Peleg.CalculatePnrNet.Model;
using System.Linq;

namespace Peleg.CalculatePnrNet
{
    public class CalculationService
    {
        private readonly PnrDbContext _context;
        private readonly GlobalParameters _globalParams;

        public CalculationService(PnrDbContext context, GlobalParameters globalParams)
        {
            _context = context;
            _globalParams = globalParams;
        }

        internal decimal Calculate(PnrModel pnrData)
        {
            return Calculate(pnrData, false, false, false);
        }

        public decimal Calculate(PnrModel pnr, bool netCalculateOnly, bool permissionNetOnly, bool grossCalculateOnly)
        {
            //calculationStart line 188
            if (pnr.NetPricesOnly && permissionNetOnly) netCalculateOnly = true;
            var pnrRates = pnr.PnrRates;

            if (netCalculateOnly)
            {
                pnrRates = pnr.PnrRates.Where(pr => pr.Pnrr_Rate_Type == "B").ToList();
            }

            if (grossCalculateOnly || pnr.GroupCostingExists)
            {
                pnrRates = pnr.PnrRates.Where(pr => pr.Pnrr_Rate_Type == "N").ToList();
            }


            return 0;
        }

        
    }


}
