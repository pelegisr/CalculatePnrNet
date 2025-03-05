using Peleg.CalculatePnrNet.Data;
using Peleg.CalculatePnrNet.DTO;
using Peleg.CalculatePnrNet.Services;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public decimal CalcGross(PnrData pnr)
        {
            // Example of calling a stored procedure
            //return _context.Database.SqlQuery<decimal>("EXEC CalcGross @PnrId",
            //    new SqlParameter("@PnrId", pnr.Id)).FirstOrDefault();
            if (_globalParams.CruiseEnable) {
                return 1;
            }
            return 0;

        }

        public decimal CalcNet(PnrData pnr)
        {
            //decimal baseNet = CalcGross(pnr) - pnr.Tickets.Sum(t => t.Discount);
            //decimal taxRate = decimal.Parse(_globalParams.GetParameter("TaxRate") ?? "0.1"); // Default 10%
            //return baseNet * (1 + taxRate);
            return 0;
        }
    }


}
