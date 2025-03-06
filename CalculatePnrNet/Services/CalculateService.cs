using System;
using System.Linq;
using NLog;
using Peleg.CalculatePnrNet.Data;
using Peleg.CalculatePnrNet.DTO;

namespace Peleg.CalculatePnrNet.Services
{
    public class CalculateService : IDisposable
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();
        private readonly PnrDbContext _context;
        private readonly GlobalParameters _globalParameters;
        private readonly int pnrId;


        public CalculateService(string connectionString, int pnrId)
        {
            Logger.Debug("Connection String: {0}", connectionString);
            if (string.IsNullOrEmpty(connectionString))
            {
                Logger.Error("Connection string cannot be null or empty");
                throw new ArgumentException("Connection string cannot be null or empty", nameof(connectionString));
            }
            var dataModelConnectionString = NaumTools.Utils.Sql2Entity(connectionString, "Data.Model");
            Logger.Debug(dataModelConnectionString);
            _context = new PnrDbContext(dataModelConnectionString);

            Logger.Debug("Pnr Id: {0}", pnrId);

            if (pnrId <= 0)
            {
                Logger.Error("Invalid PNR ID: {0}", pnrId);
                throw new ArgumentException("PNR cannot be null or empty", nameof(pnrId));
            }

            if (!PnrExists(pnrId))
            {
                Logger.Warn("PNR with ID {0} does not exist.", pnrId);
                throw new InvalidOperationException($"PNR with ID {pnrId} does not exist.");
            }
            this.pnrId = pnrId;

            _globalParameters = new GlobalParameters(_context);
        }

        public decimal CalculateGross()
        {
            Logger.Info("Starting calculation for PNR ID: {0}", pnrId);

            try
            {
                // Perform the calculation 
                decimal grossAmount = 0; // Example calculation
                Logger.Info("Gross amount calculated successfully: {0}", grossAmount);
                return grossAmount;
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "An error occurred while calculating the gross amount for PNR ID {0}", pnrId);
                throw new InvalidOperationException("An error occurred while calculating gross amount.", ex);
            }
        }

        private bool PnrExists(int pnrId)
        {
            return _context.PNRs.Any(p => p.pnr_Pnr == pnrId);
        }

        private void SetParameters()
        {
            /*
             *
               59       TRSUpd = False
               60       PrintErrorMessage = True
               61       EmptyGross9999 = False
               62       Is_Declared_Price = False
               63       WasEmptyPrice = False
               64       Free_Only = False
               70       '**** 20/05/08 ***
               72       Calculated_By_Declared_Price = False
               73       '25/10/15
               74       Is_Declared_Gross = False
               75       Is_Declared_Net = False
               
               76       Pck_Updated = ""
               77       '24/05/18
               78     
                        ClearDictionaries
               
               104      '----------------------------------
               105      LineWasCalculated.RemoveAll
               
               106      ErrFlag = 0
               107      BruttoWithoutTax = 0
               108      ChangeNetSpl = False
               109      HotelWasCalculated = False
               110      RT_As_2OW = False
               111      '06/08/07
               112      ShowDPCDebug = CBool(GetNumericValue("ShowDPCDebug") = 1)
               113      CruiseEnable = CBool(GetNumericValue("CruiseEnable") = 1)
               114      EventAsHotel = GetGlobalParametr("EventsAsHotel")
               
               116      HtlChoiceBasket = CBool(GetNumericValue("HtlChoiceBasket") = 1)
               
               117      '-----------------------------------------------------------
               118      '11/04/10
               119      CarNotRounded = True
               120      ' 12/07/06
               121      ArpTaxNetByAgt = False
               122      ArpTaxNetByAgt = CBool(GetNumericValue("ArpTaxNetByAgt") = 1)
               123      ATC_Enabled = IIf(IsNull(GetNumericValue("ATC_Enabled")), 0, GetNumericValue("ATC_Enabled"))
               
               124      If ATC_Enabled = 1 Then
               125          DE.cmdCheckATC_InPNR prmPNR
               126          ATC_Enabled = DE.rscmdCheckATC_InPNR!ATC
               127          DE.rscmdCheckATC_InPNR.Close
               128      End If '»If ATC_Enabled = 1 Then
                */
             
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}

