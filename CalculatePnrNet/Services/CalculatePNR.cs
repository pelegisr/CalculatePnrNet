using NLog;
using Peleg.CalculatePnrNet.Data;
using Peleg.CalculatePnrNet.Model;
using Peleg.CalculatePnrNet.Services;
using System;

namespace Peleg.CalculatePnrNet
{
    public class CalculatePNR : StartMenuInterface.IStartMenu
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly string dataModelConnectionString;

        public void Run(ref string connectionString, ref string taskName)
        {
            NotImplementedException ex = new NotImplementedException();
            Logger.Error(ex, "This method is not implemented.");
        }

        public CalculatePNR(ref string connectionString)
        {
            dataModelConnectionString = NaumTools.Utils.Sql2Entity(connectionString, "Data.Model");
        }

        public CalculationResult Calculate(int pnrId)
        {
            try
            {
                using (var context = new PnrDbContext(dataModelConnectionString))
                {
                    GlobalParameters _globalParams = new GlobalParameters(context);
                    PnrService _pnrService = new PnrService(context, _globalParams);
                    CalculationService _calculationService = new CalculationService(context, _globalParams);

                    PnrModel pnr = _pnrService.GetPnr(pnrId);
                    if (pnr == null)
                    {
                        Logger.Warn($"PNR ID {pnrId} not found in the database.");
                        return new CalculationResult
                        {
                            Success = false,
                            Value = -1, 
                            ErrorMessage = $"PNR ID {pnrId} not found."
                        };
                    }
                    var amount = _calculationService.Calculate(pnr);
                    return new CalculationResult
                    {
                        Success = true,
                        Value = amount,
                        ErrorMessage = null
                    };
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex, $"Failed to calculate for PNR ID {pnrId}");
                throw; // Re-throw to inform the caller
            }

        }

        public decimal CalcNet(int pnrId)
        {
            return 0;
        }
    }

}
