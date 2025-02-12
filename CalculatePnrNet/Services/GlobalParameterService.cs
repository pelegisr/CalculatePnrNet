using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Peleg.CalculatePnrNet.Data;

namespace Peleg.CalculatePnrNet.Services
{
    class GlobalParameterService
    {
        private readonly PnrDbContext _context;

        public GlobalParameterService(PnrDbContext context)
        {
            _context = context;
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
                .FirstOrDefault() ?? "";       // Return 0 if null
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
                //TODO: take date format from DB
                //DECLARE @date_format int
                //SET     @date_format = dbo.PLG_Global_n('DateFormat')
                int dateFormat = 3;

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
                                            (param.n_value != null ? param.n_value.ToString() :
                                                (param.d_value.HasValue ? ConvertDateFormat(param.d_value.Value, dateFormat) : null));

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

        public static string ConvertDateFormat(DateTime date, int sqlStyle)
        {
            string format;
            switch (sqlStyle)
            {
                case 1:
                    format = "MM/dd/yy"; // USA style
                    break;
                case 3:
                    format = "dd/MM/yy"; // British/French style
                    break;
                case 4:
                    format = "dd.MM.yy"; // German style
                    break;
                case 10:
                    format = "MM-dd-yy"; // USA style with hyphens
                    break;
                case 11:
                    format = "yy/MM/dd"; // Japanese style
                    break;
                case 12:
                    format = "yy.MM.dd"; // Japanese style
                    break;
                case 13:
                    format = "dd MMM yy"; // British style with abbreviated month
                    break;
                case 14:
                    format = "HH:mm:ss"; // 24-hour format (time only)
                    break;
                case 20:
                    format = "yyyy-MM-dd"; // ISO 8601 (standard date format)
                    break;
                case 21:
                    format = "yyyy-MM-dd HH:mm:ss"; // ISO 8601 with time
                    break;
                case 23:
                    format = "yyyy/MM/dd"; // ISO 8601 without hyphens
                    break;
                default:
                    format = "yyyy-MM-dd"; // Default to ISO if unknown
                    break;
            }

            return date.ToString(format);
        }
    }
}
