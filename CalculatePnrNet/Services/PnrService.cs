using NLog;
using Peleg.CalculatePnrNet.Data;
using Peleg.CalculatePnrNet.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;

namespace Peleg.CalculatePnrNet.Services
{
    public class PnrService
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly PnrDbContext _context;
        private readonly GlobalParameters _globalParams;

        public PnrService(PnrDbContext context, GlobalParameters globalParams)
        {
            _context = context;
            _globalParams = globalParams;
        }

        public PnrModel GetPnr(int pnrId)
        {
            try
            {
                var entity = _context.PNRs
                    .Include("Pnr_rate")
                    .Include("Pnr_la")
                    .Include("Pnr_px")
                    .FirstOrDefault(p => p.pnr_Pnr == pnrId);
                if (entity == null)
                {
                    Logger.Warn($"PNR ID {pnrId} not found in the database.");
                    return null;
                }

                Logger.Info($"PNR ID {pnrId} found. Returning data.");

                var pnr = new PnrModel(entity);

                // Fetch related Pnr_Free records
                var pnrFrees = _context.Pnr_Free
                    .Where(pf => pf.Pnr_Pnr == entity.pnr_Pnr)
                    .ToList();

                pnr.PnrFrees = pnrFrees;

                // Fetch PnrRates and map to PnrRate Model
                var pnrRates = entity.Pnr_rate
                        .Select(p => new PnrRate
                        {
                            PX_ID = p.PX_ID,
                            Pnrr_Id = p.Pnrr_Id,
                            Pnrr_Supp = p.Pnrr_Supp,
                            Pnrr_service = p.Pnrr_service,
                            Pnrr_Flight = p.Pnrr_Flight,
                            Pnrr_Line = p.Pnrr_Line,
                            Pnrr_Rate_Type = p.Pnrr_Rate_Type,
                            Pnrr_Rate_Code = p.Pnrr_Rate_Code,
                            Pnrr_Rate_PLG = p.Pnrr_Rate_PLG,
                            Pnrr_PriceList = p.Pnrr_PriceList,
                            Pnrr_CurrBrutto = string.IsNullOrEmpty(p.Pnrr_CurrBrutto?.ToString()) ? _globalParams.LocalCurrency : p.Pnrr_CurrBrutto,
                            Pnrr_CurrNeto = string.IsNullOrEmpty(p.Pnrr_CurrNeto?.ToString()) ? _globalParams.LocalCurrency : p.Pnrr_CurrNeto,
                            Pnrr_Rate_brutto = p.Pnrr_Rate_brutto,
                            Pnrr_Rate_neto = p.Pnrr_Rate_neto,
                            Pnrr_Rate_Detail = p.Pnrr_Rate_Detail,
                            Pnrr_Price_Include = p.Pnrr_Price_Include,
                            Pnrr_vat = p.Pnrr_vat,
                            Pnrr_Invoice = p.Pnrr_Invoice,
                            Pnrr_Inv_Supplier = p.Pnrr_Inv_Supplier,
                            Pnrr_Comm_Group = p.Pnrr_Comm_Group,
                            Pnrr_Comm_Percent = p.Pnrr_Comm_Percent ?? 0,
                            Pnrr_Comm_Summ = p.Pnrr_Comm_Summ ?? 0,
                            Pnrr_Comm_Need = p.Pnrr_Comm_Need,
                            Pnrr_Comm_Product = p.Pnrr_Comm_Product,
                            Pnrr_Price_Type = p.Pnrr_Price_Type,
                            Pnrr_CalcStatus = p.Pnrr_CalcStatus,
                            Pnrr_DateUpd = p.Pnrr_DateUpd,
                            Pnrr_User = p.Pnrr_User
                        })
                        .ToList();

                pnr.PnrRates = pnrRates;

                GetFullCDM(pnr);

                CreateDeclaredPriceDictionary(pnr);

                Logger.Info($"PNR Model object created for PNR ID: {pnr.PnrId}");
                return pnr;
            }
            catch (Exception ex)
            {
                Logger.Error(ex, $"Error in GetPnr: PNR={pnrId}");
                throw;
            }
        }


        // GetFull_CDM_ByPnr
        public void GetFullCDM(PnrModel pnr)
        {
            try
            {
                int pnrId = pnr.PnrId;
                // Call stored procedure or query the database
                var cdmData = _context.Plg_GetFull_CDM_Data(pnrId).ToList();

                if (cdmData == null || !cdmData.Any())
                    return;

                pnr.CommissionData = cdmData.Where(cdm => cdm.CDM_Type == "C").ToList();
                pnr.DiscountData = cdmData.Where(cdm => cdm.CDM_Type == "D").ToList();
                pnr.MarkUpData = cdmData.Where(cdm => cdm.CDM_Type == "M").ToList();

                Logger.Info($"Plg_GetFull_CDM_Data: Finish, PNR={pnr.PnrId}");

            }
            catch (Exception ex)
            {
                Logger.Error(ex, $"Error in GetFullCDMByPnr: PNR={pnr.PnrId}");
                throw;
            }
        }

        public void CreateDeclaredPriceDictionary(PnrModel pnr)
        {
            try
            {
                var filteredPrices = _context.PLG_Check_Pnr_La_Price(pnr.PnrId)
                .Where(p => p.Gross != 0 || p.Nett != 0)
                .ToList();

                pnr.DeclaredPriceDictionary.Clear();

                foreach (var price in filteredPrices)
                {
                    string InDic = price.PX_ID + "/" + price.Line;
                    pnr.DeclaredPriceDictionary[InDic] = "";
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex, $"Error in CreateDeclaredPriceDictionary: PNR={pnr.PnrId}");
                throw;
            }
        }

    }
}
