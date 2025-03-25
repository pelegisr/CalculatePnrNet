using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Peleg.CalculatePnrNet.Model
{
    public class PnrRate
    {
        public string PX_ID { get; set; }
        public int Pnrr_Id { get; set; }
        public string Pnrr_Supp { get; set; }
        public string Pnrr_service { get; set; }
        public string Pnrr_Flight { get; set; }
        public short Pnrr_Line { get; set; }
        public string Pnrr_Rate_Type { get; set; }
        public short Pnrr_Rate_Code { get; set; }
        public short Pnrr_Rate_PLG { get; set; }
        public int Pnrr_PriceList { get; set; }
        public string Pnrr_CurrBrutto { get; set; }
        public string Pnrr_CurrNeto { get; set; }
        public decimal Pnrr_Rate_brutto { get; set; }
        public decimal Pnrr_Rate_neto { get; set; }
        public string Pnrr_Rate_Detail { get; set; }
        public bool? Pnrr_Price_Include { get; set; }
        public decimal Pnrr_vat { get; set; }
        public decimal? Pnrr_Invoice { get; set; }
        public string Pnrr_Inv_Supplier { get; set; }
        public string Pnrr_Comm_Group { get; set; }
        public decimal? Pnrr_Comm_Percent { get; set; }
        public decimal? Pnrr_Comm_Summ { get; set; }
        public string Pnrr_Comm_Need { get; set; }
        public string Pnrr_Comm_Product { get; set; }
        public string Pnrr_Price_Type { get; set; }
        public string Pnrr_CalcStatus { get; set; }
        public DateTime Pnrr_DateUpd { get; set; }
        public string Pnrr_User { get; set; }
    }
}
