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
            // Test global paramter load
            if (_globalParams.CruiseEnable) {
                return 1;
            }
            return 0;

        }

        public decimal CalcNet(PnrData pnr)
        {
            return 0;
        }
    }


}
