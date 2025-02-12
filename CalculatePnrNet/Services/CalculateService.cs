using System;
using System.Linq;
using NLog;
using Peleg.CalculatePnrNet.Data;

namespace Peleg.CalculatePnrNet.Services
{
    public class CalculateService : IDisposable
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();
        private readonly PnrDbContext _context;
        private readonly GlobalParameterService _globalParameterService;
        private readonly int pnrId;

        #region Parameters

        private bool? _seatWithCommiss;
        public bool SeatWithCommiss
        {
            get
            {
                if (_seatWithCommiss == null)
                {
                    _seatWithCommiss = _globalParameterService.GetNumericValue("SeatWithCommiss") == 1;
                }
                return _seatWithCommiss.Value; // Ensures a non-null return value
            }
        }

        private bool? _pnr4GroupPCK;
        public bool Pnr4GroupPCK
        {
            get
            {
                if (_pnr4GroupPCK == null)
                {
                    _pnr4GroupPCK = _globalParameterService.GetNumericValue("Pnr4GroupPCK") == 1;
                }
                return _pnr4GroupPCK.Value; // Ensures a non-null return value
            }
        }

        private bool? _checkByConsist;
        public bool CheckByConsist
        {
            get
            {
                if (_checkByConsist == null)
                {
                    _checkByConsist = _globalParameterService.GetNumericValue("CheckByConsist") == 1;
                }
                return _checkByConsist.Value; // Ensures a non-null return value
            }
        }

        private bool? _WithInsurance;
        public bool WithInsurance
        {
            get
            {
                if (_WithInsurance == null)
                {
                    _WithInsurance = _context.Pnr_Free
                        .Any(p => p.Service == "INS"
                                  && new[] { "OK", "RQ" }.Contains(p.Status)
                                  && p.Pnr_Pnr == pnrId) ? true : false;
                }
                return _WithInsurance.Value; // Ensures a non-null return value
            }
        }

        private bool? _buildFltVch;
        public bool Build_Flt_Vch
        {
            get
            {
                if (_buildFltVch == null)
                {
                    _buildFltVch = _globalParameterService.GetNumericValue("Build_Flt_Vch") == 1;
                }
                return _buildFltVch.Value;
            }
        }

        private bool? _mustCheckMealAdd;
        public bool MustCheckMealAdd
        {
            get
            {
                if (_mustCheckMealAdd == null)
                {
                    _mustCheckMealAdd = Convert.ToInt32(_globalParameterService.GetNumericValue("CheckMealAdd")) == 1;
                }
                return _mustCheckMealAdd.Value;
            }
        }

        private bool? _groupCostingExists;
        public bool GroupCostingExists
        {
            get
            {
                if (_groupCostingExists == null)
                {
                    _groupCostingExists = _context.Pnr_rate
                        .Any(p => p.Pnr_pnr == pnrId && p.Pnrr_service == "CST");
                }

                return _groupCostingExists.Value;
            }
        }


        private bool? _babyPayNetFlt;
        public bool BabyPayNetFlt
        {
            get
            {
                if (_babyPayNetFlt == null)
                {
                    _babyPayNetFlt = _globalParameterService.GetNumericValue("BabyPayNetFlt") == 1;
                }
                return _babyPayNetFlt.Value;
            }
        }

        private bool? _babyCruiseTax;
        public bool BabyCruiseTax
        {
            get
            {
                if (_babyCruiseTax == null)
                {
                    _babyCruiseTax = _globalParameterService.GetNumericValue("Baby_CruiseTax") == 1;
                }
                return _babyCruiseTax.Value;
            }
        }

        private bool? _accumulateComm;
        public bool AccumulateComm
        {
            get
            {
                if (_accumulateComm == null)
                {
                    _accumulateComm = _globalParameterService.GetNumericValue("Accumulate_Comm") == 1;
                }
                return _accumulateComm.Value;
            }
        }

        private bool? _showPriceBySrv;
        public bool ShowPriceBySrv
        {
            get
            {
                if (_showPriceBySrv == null)
                {
                    _showPriceBySrv = _globalParameterService.GetNumericValue("ShowPriceBySrv") == 1;
                }
                return _showPriceBySrv.Value;
            }
        }

        private bool? _addNetoPrice;
        public bool AddNetoPrice
        {
            get
            {
                if (_addNetoPrice == null)
                {
                    _addNetoPrice = _globalParameterService.GetNumericValue("AddNetoPrice") == 1;
                }
                return _addNetoPrice.Value;
            }
        }

        private bool? _isPckCarAllPric;
        public bool IsPckCarAllPric
        {
            get
            {
                if (_isPckCarAllPric == null)
                {
                    _isPckCarAllPric = _globalParameterService.GetNumericValue("IsPckCarAllPric") == 1;
                }
                return _isPckCarAllPric.Value;
            }
        }

        private bool? _discountPckNeto;
        public bool DiscountPckNeto
        {
            get
            {
                if (_discountPckNeto == null)
                {
                    _discountPckNeto = _globalParameterService.GetNumericValue("DiscountPckNeto") == 1;
                }
                return _discountPckNeto.Value;
            }
        }

        private bool? _multiPckEnabled;
        public bool MultiPckEnabled
        {
            get
            {
                if (_multiPckEnabled == null)
                {
                    _multiPckEnabled = _globalParameterService.GetNumericValue("MultiPckEnabled") == 1;
                }
                return _multiPckEnabled.Value;
            }
        }

        private bool? _calcByGroupEnab;
        public bool CalcByGroupEnab
        {
            get
            {
                if (_calcByGroupEnab == null)
                {
                    _calcByGroupEnab = _globalParameterService.GetNumericValue("CalcByGroupEnab") == 1;
                }
                return _calcByGroupEnab.Value;
            }
        }

        private string _calcGroupCode;
        public string CalcGroupCode => _calcGroupCode ?? (_calcGroupCode = _globalParameterService.GetStringValue("CalcByGroupEnab"));


        private bool? _showDPCDebug;
        public bool ShowDPCDebug
        {
            get
            {
                if (_showDPCDebug == null)
                {
                    _showDPCDebug = _globalParameterService.GetNumericValue("ShowDPCDebug") == 1;
                }
                return _showDPCDebug.Value;
            }
        }

        private bool? _cruiseEnable;
        public bool CruiseEnable
        {
            get
            {
                if (_cruiseEnable == null)
                {
                    _cruiseEnable = _globalParameterService.GetNumericValue("CruiseEnable") == 1;
                }
                return _cruiseEnable.Value;
            }
        }

        private string _eventAsHotel;
        public string EventAsHotel
        {
            get
            {
                if (_eventAsHotel == null)
                {
                    _eventAsHotel = _globalParameterService.GetGlobalParameter("EventsAsHotel").Value;
                }
                return _eventAsHotel;
            }
        }

        private bool? _htlChoiceBasket;
        public bool HtlChoiceBasket
        {
            get
            {
                if (_htlChoiceBasket == null)
                {
                    _htlChoiceBasket = _globalParameterService.GetNumericValue("HtlChoiceBasket") == 1;
                }
                return _htlChoiceBasket.Value;
            }
        }

        private bool? _arpTaxNetByAgt;
        public bool ArpTaxNetByAgt
        {
            get
            {
                if (_arpTaxNetByAgt == null)
                {
                    _arpTaxNetByAgt = _globalParameterService.GetNumericValue("ArpTaxNetByAgt") == 1;
                }
                return _arpTaxNetByAgt.Value;
            }
        }

        private bool? _atcEnabled;
        public bool ATC_Enabled
        {
            get
            {
                if (_atcEnabled == null)
                {
                    var value = _globalParameterService.GetNumericValue("ATC_Enabled");
                    _atcEnabled = value == null ? false : value == 1;
                }
                return _atcEnabled.Value;
            }
        }

        #endregion


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

            _globalParameterService = new GlobalParameterService(_context);
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

