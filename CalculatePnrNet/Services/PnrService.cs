using NLog;
using Peleg.CalculatePnrNet.Data;
using Peleg.CalculatePnrNet.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Peleg.CalculatePnrNet.Services
{
    public class PnrService
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly PnrDbContext _context;

        public PnrService(PnrDbContext context)
        {
            _context = context;
        }

        public PnrData GetPnrData(int pnrId)
        {
            try
            {
                Logger.Info($"Fetching PNR data for PNR ID: {pnrId}");
                var pnr = _context.PNRs.FirstOrDefault(p => p.pnr_Pnr == pnrId);
                if (pnr == null)
                {
                    Logger.Warn($"PNR ID {pnrId} not found in the database.");
                    throw new KeyNotFoundException($"PNR ID {pnrId} not found.");
                }

                Logger.Info($"PNR ID {pnrId} found. Returning data.");

                // Convert to DTO
                return BuildPNRDataObject(pnr);

            }
            catch (Exception ex)
            {
                Logger.Error(ex, $"Error fetching PNR data for PNR ID {pnrId}");
                throw;
            }
        }

        // GetFull_CDM_ByPnr
        public void GetFullCDMByPnr()
        {
            //try
            //{
            //    // Call stored procedure or query the database
            //    var cdmData = _context.Plg_GetFull_CDM_Data(pnrId).ToList();

            //    if (cdmData == null || !cdmData.Any())
            //        return new CDMByPnrResult();

            //    var result = new CDMByPnrResult
            //    {
            //        CommissionData = cdmData.Where(cdm => cdm.CDM_Type == "C").ToList(),
            //        DiscountData = cdmData.Where(cdm => cdm.CDM_Type == "D").ToList(),
            //        MarkUpData = cdmData.Where(cdm => cdm.CDM_Type == "M").ToList()
            //    };

            //    Logger.Info($"Plg_GetFull_CDM_Data: Finish, PNR={pnrId}");
            //    return result;
            //}
            //catch (Exception ex)
            //{
            //    Logger.Error(ex, $"Error in GetFullCDMByPnr: PNR={pnrId}");
            //    throw;
            //}
        }


        private PnrData BuildPNRDataObject(PNR pnr)
        {
            var pnrData = new PnrData(
                pnr.pnr_Pnr,
                pnr.pnr_Create_Date.Date,
                pnr.cmp_Code,
                pnr.pnr_Agency,
                pnr.pnr_Agent,
                pnr.pnr_branch,
                pnr.pnr_Package,
                (pnr.pnr_Internet ?? 0) > 0 ? 1 : 0,
                pnr.pnr_Agent // Assuming `pnr_Agent` is also the main agent place
            );

            pnrData.WithInsurance = _context.Pnr_Free
                      .Any(p => p.Service == "INS"
                                && new[] { "OK", "RQ" }.Contains(p.Status)
                                && p.Pnr_Pnr == pnr.pnr_Pnr) ? true : false;

            pnrData.GroupCostingExists = _context.Pnr_rate.Any(p => p.Pnr_pnr == pnr.pnr_Pnr && p.Pnrr_service == "CST");


            return pnrData;
        }
    }
}
