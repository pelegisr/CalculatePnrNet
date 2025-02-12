using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Peleg.CalculatePnrNet.Data
{
    public partial class PnrDbContext : DbContext
    {
        public PnrDbContext(string connectionString) : base(connectionString) { }
    }
}
