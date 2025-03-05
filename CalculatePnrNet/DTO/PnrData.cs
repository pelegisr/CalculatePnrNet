using Peleg.CalculatePnrNet.Data;
using System;

namespace Peleg.CalculatePnrNet.DTO
{
    public class PnrData
    {
        public int PnrId { get; }
        public DateTime ReservationDate { get; }
        public string CompanyCode { get; }
        public string Agency { get; }
        public string Agent { get; }
        public string Branch { get; }
        public int? PCK_ID { get; }
        public int FromInternet { get; }
        public string MainAgentPlace { get; }

        public bool? WithInsurance;

        public bool GroupCostingExists;

        public PnrData(int pnrId, DateTime reservationDate, string companyCode, string agency,
                       string agent, string branch, int? pckId, int fromInternet, string mainAgentPlace)
        {
            PnrId = pnrId;
            ReservationDate = reservationDate;
            CompanyCode = companyCode;
            Agency = agency;
            Agent = agent;
            Branch = branch;
            PCK_ID = pckId;
            FromInternet = fromInternet;
            MainAgentPlace = mainAgentPlace;
        }
    }

}