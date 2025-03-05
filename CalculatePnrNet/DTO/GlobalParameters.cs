using Peleg.CalculatePnrNet.Data;
using Peleg.CalculatePnrNet.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Peleg.CalculatePnrNet.DTO
{
    public class GlobalParameters
    {
        private readonly GlobalParameterService _globalParameterService;
        private readonly PnrDbContext _context;

        public GlobalParameters(PnrDbContext context)
        {
            _globalParameterService = new GlobalParameterService(context);
        }

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
                    _atcEnabled = value == 0 ? false : Convert.ToBoolean(value);
                }
                return _atcEnabled.Value;
            }
        }

    }
}
