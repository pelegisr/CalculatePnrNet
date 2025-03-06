﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Peleg.CalculatePnrNet.Data
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class PnrDbContext : DbContext
    {
        public PnrDbContext()
            : base("name=PnrDbContext")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<global> globals { get; set; }
        public virtual DbSet<Internet_PNR_LOG> Internet_PNR_LOG { get; set; }
        public virtual DbSet<PCK_Package> PCK_Package { get; set; }
        public virtual DbSet<PNR> PNRs { get; set; }
        public virtual DbSet<Pnr_Free> Pnr_Free { get; set; }
        public virtual DbSet<Pnr_La> Pnr_La { get; set; }
        public virtual DbSet<Pnr_rate> Pnr_rate { get; set; }
    
        public virtual ObjectResult<Plg_GetFull_CDM_Data_Result> Plg_GetFull_CDM_Data(Nullable<int> pnr)
        {
            var pnrParameter = pnr.HasValue ?
                new ObjectParameter("Pnr", pnr) :
                new ObjectParameter("Pnr", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Plg_GetFull_CDM_Data_Result>("Plg_GetFull_CDM_Data", pnrParameter);
        }
    }
}
