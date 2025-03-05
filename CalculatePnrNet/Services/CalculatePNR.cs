using NLog;
using Peleg.CalculatePnrNet.Data;
using Peleg.CalculatePnrNet.DTO;
using Peleg.CalculatePnrNet.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Peleg.CalculatePnrNet
{
    public class CalculatePNR
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly PnrService _pnrService;
        private readonly CalculationService _calcService;
        private readonly GlobalParameters _globalParams;

        public CalculatePNR(PnrDbContext context)
        {
            _globalParams = new GlobalParameters(context);
            _pnrService = new PnrService(context);
            _calcService = new CalculationService(context, _globalParams);
        }

        public decimal CalcGross(int pnrId)
        {
            try
            {
                var pnr = _pnrService.GetPnrData(pnrId);
                return _calcService.CalcGross(pnr);
            }
            catch (Exception ex)
            {
                Logger.Error(ex, $"Failed to calculate gross for PNR ID {pnrId}");
                throw; // Re-throw to inform the caller
            }
        }

        public decimal CalcNet(int pnrId)
        {
            try
            {
                var pnr = _pnrService.GetPnrData(pnrId);
                return _calcService.CalcNet(pnr);
            }
            catch (Exception ex)
            {
                Logger.Error(ex, $"Failed to calculate net for PNR ID {pnrId}");
                throw;
            }
        }
    }


}
