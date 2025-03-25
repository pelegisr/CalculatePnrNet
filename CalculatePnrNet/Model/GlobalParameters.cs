using Peleg.CalculatePnrNet.Data;
using Peleg.CalculatePnrNet.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Peleg.CalculatePnrNet.Model
{
    public class GlobalParameters
    {
        private readonly UtilService _utilService;
        
        public GlobalParameters(PnrDbContext context)
        {
            _utilService = new UtilService(context);
        }

        private bool? _seatWithCommiss;
        public bool SeatWithCommiss
        {
            get
            {
                if (_seatWithCommiss == null)
                {
                    _seatWithCommiss = _utilService.GetNumericValue("SeatWithCommiss") == 1;
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
                    _pnr4GroupPCK = _utilService.GetNumericValue("Pnr4GroupPCK") == 1;
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
                    _checkByConsist = _utilService.GetNumericValue("CheckByConsist") == 1;
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
                    _buildFltVch = _utilService.GetNumericValue("Build_Flt_Vch") == 1;
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
                    _mustCheckMealAdd = Convert.ToInt32(_utilService.GetNumericValue("CheckMealAdd")) == 1;
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
                    _babyPayNetFlt = _utilService.GetNumericValue("BabyPayNetFlt") == 1;
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
                    _babyCruiseTax = _utilService.GetNumericValue("Baby_CruiseTax") == 1;
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
                    _accumulateComm = _utilService.GetNumericValue("Accumulate_Comm") == 1;
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
                    _showPriceBySrv = _utilService.GetNumericValue("ShowPriceBySrv") == 1;
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
                    _addNetoPrice = _utilService.GetNumericValue("AddNetoPrice") == 1;
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
                    _isPckCarAllPric = _utilService.GetNumericValue("IsPckCarAllPric") == 1;
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
                    _discountPckNeto = _utilService.GetNumericValue("DiscountPckNeto") == 1;
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
                    _multiPckEnabled = _utilService.GetNumericValue("MultiPckEnabled") == 1;
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
                    _calcByGroupEnab = _utilService.GetNumericValue("CalcByGroupEnab") == 1;
                }
                return _calcByGroupEnab.Value;
            }
        }

        private string _calcGroupCode;
        public string CalcGroupCode => _calcGroupCode ?? (_calcGroupCode = _utilService.GetStringValue("CalcByGroupEnab"));


        private bool? _showDPCDebug;
        public bool ShowDPCDebug
        {
            get
            {
                if (_showDPCDebug == null)
                {
                    _showDPCDebug = _utilService.GetNumericValue("ShowDPCDebug") == 1;
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
                    _cruiseEnable = _utilService.GetNumericValue("CruiseEnable") == 1;
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
                    _eventAsHotel = _utilService.GetGlobalParameter("EventsAsHotel").Value;
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
                    _htlChoiceBasket = _utilService.GetNumericValue("HtlChoiceBasket") == 1;
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
                    _arpTaxNetByAgt = _utilService.GetNumericValue("ArpTaxNetByAgt") == 1;
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
                    var value = _utilService.GetNumericValue("ATC_Enabled");
                    _atcEnabled = value == 0 ? false : Convert.ToBoolean(value);
                }
                return _atcEnabled.Value;
            }
        }

        private bool? _BabyPayHtlOnly;
        public bool BabyPayHtlOnly
        {
            get
            {
                if (_BabyPayHtlOnly == null)
                {
                    _BabyPayHtlOnly = _utilService.GetNumericValue("BabyPay_HtlOnly") == 1;
                }
                return _BabyPayHtlOnly.Value;
            }
        }

        private bool? _MustSameBoard;
        public bool MustSameBoard
        {
            get
            {
                if (_MustSameBoard == null)
                {
                    _MustSameBoard = _utilService.GetNumericValue("MustSameBoard") == 1;
                }
                return _MustSameBoard.Value;
            }
        }

        private string _LocalCurrency;
        public string LocalCurrency
        {
            get
            {
                if (_LocalCurrency == null)
                {
                    _LocalCurrency = _utilService.GetStringValue("Currency");
                }
                return _LocalCurrency;
            }
        }

        private bool? _isKenesSystem;
        public bool  IsKenesSystem
        {
            get
            {
                if (_isKenesSystem == null)
                {
                    _isKenesSystem = _utilService.GetNumericValue("isKenesSystem") == 1;
                }
                return _isKenesSystem.Value;
            }
        }

        private string _CalcSystem;
        public string CalcSystem
        {
            get
            {
                if (_CalcSystem == null)
                {
                    _LocalCurrency = _utilService.GetStringValue("CalcSystem");
                }
                return _CalcSystem;
            }
        }

        private bool? _Change2Package;
        public bool Change2Package
        {
            get
            {
                if (_Change2Package == null)
                {
                    _Change2Package = _utilService.GetNumericValue("Change2Package") == 1;
                }
                return _Change2Package.Value;
            }
        }

        private string _Transfer;
        public string Transfer
        {
            get
            {
                if (_Transfer == null)
                {
                    _Transfer = _utilService.GetStringValue("Transfer");
                }
                return _Transfer;
            }
        }

    }
}
