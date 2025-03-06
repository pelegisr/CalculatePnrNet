using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using NLog;
using Peleg.CalculatePnrNet.Data;

namespace Peleg.CalculatePnrNet.Services
{
    class UtilService
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly PnrDbContext _context;
        public UtilService(PnrDbContext context)
        {
            _context = context;
        }

        private string _dateFormat;

        public string DateFormat
        {
            get
            {
                if (_dateFormat == null)
                {
                    var dateFormat = GetNumericValue("DateFormat");

                    // Define an array for quick lookup
                    string[] dateFormats = new string[]
                    {
                        "yyyy-MM-dd", // Index 0 (default fallback)
                        "MM/dd/yy", // 1 - USA style
                        "", // 2 (not defined)
                        "dd/MM/yy", // 3 - British/French style
                        "dd.MM.yy", // 4 - German style
                        "", "", "", "", "", // 5-9 (not defined)
                        "MM-dd-yy", // 10 - USA style with hyphens
                        "yy/MM/dd", // 11 - Japanese style
                        "yy.MM.dd", // 12 - Japanese style
                        "dd MMM yy", // 13 - British style with abbreviated month
                        "HH:mm:ss", // 14 - Time only (24-hour format)
                        "", "", "", "", "", // 15-19 (not defined)
                        "yyyy-MM-dd", // 20 - ISO 8601 standard
                        "yyyy-MM-dd HH:mm:ss", // 21 - ISO 8601 with time
                        "", // 22 (not defined)
                        "yyyy/MM/dd" // 23 - ISO 8601 without hyphens
                    };

                    // Assign the format, ensuring we stay within bounds
                    _dateFormat = (dateFormat >= 1 && dateFormat < dateFormats.Length &&
                                   !string.IsNullOrEmpty(dateFormats[dateFormat]))
                        ? dateFormats[dateFormat]
                        : dateFormats[0]; // Default to "yyyy-MM-dd"
                }

                return _dateFormat;
            }
        }

        public int GetNumericValue(string paramName)
        {
            try
            {
                return _context.globals
                    .Where(g => g.name == paramName)
                    .Select(g => (int?)g.n_value) // Nullable int to handle null values
                    .FirstOrDefault() ?? 0; // Return 0 if null
            }
            catch (Exception ex)
            {
                // Handle error properly, log it, etc.
                throw new Exception($"Error retrieving global parameter GetNumericValue {paramName}", ex);
            }
        }

        public string GetStringValue(string paramName)
        {
            try
            {
                return _context.globals
                    .Where(g => g.name == paramName)
                    .Select(g => g.c_value)
                    .FirstOrDefault() ?? ""; // Return 0 if null
            }
            catch (Exception ex)
            {
                // Handle error properly, log it, etc.
                throw new Exception($"Error retrieving global parameter GetStringValue {paramName}", ex);
            }
        }

        public (string Value, string Type) GetGlobalParameter(string paramName)
        {
            try
            {
                var param = _context.globals
                    .Where(g => g.name == paramName)
                    .Select(g => new
                    {
                        c_value = g.c_value,
                        n_value = g.n_value,
                        d_value = g.d_value,
                        Type = g.c_value != null ? "C" : (g.n_value != null ? "N" : "D")
                    })
                    .FirstOrDefault();

                if (param != null)
                {
                    var formattedValue = param.c_value ??
                                         (param.n_value != null
                                             ? param.n_value.ToString()
                                             : param.d_value?.ToString(DateFormat));

                    return (formattedValue, param.Type);
                }
                else
                {
                    // Handle missing parameter
                    string errorMessage = $"Parameter - {paramName} does not exist";
                    // Log error or show message box if needed
                    return (null, null);
                }
            }
            catch (Exception ex)
            {
                // Handle error properly, log it, etc.
                throw new Exception($"Error retrieving global parameter GetGlobalParameter {paramName}", ex);
            }
        }

        public void SaveIntoInternetPnrLog(string code, string value)
        {
            try
            {
                var logEntry = new Internet_PNR_LOG
                {
                    Prm_Code = code,
                    Prm_Value = value
                };

                _context.Internet_PNR_LOG.Add(logEntry);
                _context.Entry(logEntry).State = EntityState.Added; // Explicitly mark only this entity as added
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Logger.Error(ex, $"Error in SaveIntoInternetPnrLog: Code={code}, Value={value}");
                throw; 
            }
        }

    }
}
