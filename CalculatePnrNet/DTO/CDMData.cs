using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Peleg.CalculatePnrNet.DTO
{
    public class CDMData
    {
        public string PX_ID { get; set; }
        public string Prod { get; set; }
        public string ProdCode { get; set; }
        public int ProdId { get; set; }
        public int ProdId2 { get; set; }
        public string CommCategory { get; set; }
        public decimal CommNorm { get; set; }
        public decimal CommSpec { get; set; }
        public string PrcAmnt { get; set; }
        public string InclTax { get; set; }
        public string InclReg { get; set; }
        public string CDM_Type { get; set; }
    }
}
