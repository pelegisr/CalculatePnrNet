using Peleg.CalculatePnrNet.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Peleg.CalculatePnrNet.Model
{
    public class PnrModel
    {
        public PNR Entity { get; set; }

        public PnrModel(PNR entity)
        {
            Entity = entity;
        }

        public int PnrId => Entity.pnr_Pnr;
        public DateTime ReservationDate => Entity.pnr_Create_Date.Date;
        public string CompanyCode => Entity.cmp_Code;
        public string Agency => Entity.pnr_Agency;
        public string Agent => Entity.pnr_Agent;
        public string Branch => Entity.pnr_branch;
        public int? PCK_ID => Entity.pnr_Package;
        public int FromInternet => (Entity.pnr_Internet ?? 0) > 0 ? 1 : 0;
        public string MainAgentPlace => Entity.pnr_Agent;

        public List<Plg_GetFull_CDM_Data_Result> CommissionData { get; set; }
        public List<Plg_GetFull_CDM_Data_Result> DiscountData { get; set; }
        public List<Plg_GetFull_CDM_Data_Result> MarkUpData { get; set; }

        public Dictionary<string, string> DeclaredPriceDictionary { get; private set; } = new Dictionary<string, string>();
        public bool DeclaredPrice => DeclaredPriceDictionary.Any();
        public bool NetPricesOnly => DeclaredPrice && !DeclaredPriceDictionary.Keys.Any(key => key.Contains("Gross"));
        public List<PnrRate> PnrRates { get; internal set; }

        public bool GroupCostingExists => PnrRates.Any(p => p.Pnrr_service == "CST");

        public List<Pnr_Free> PnrFrees { get; set; }

        public bool? WithInsurance =>  PnrFrees.Any(p =>
            p.Service == "INS" && new[] { "OK", "RQ" }.Contains(p.Status));

        public bool FreeOnly =>
            !Entity.Pnr_Px.Any(px => new[] { "OK", "RQ" }.Contains(px.pfp_status)) &&
            !Entity.Pnr_La.Any(la => la.pla_Status.StartsWith("OK") || la.pla_Status == "RQ") &&
            PnrFrees.Any();
    }

}