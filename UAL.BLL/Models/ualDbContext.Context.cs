﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace UAL.BLL.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class UnitedAccessoriesDBEntities : DbContext
    {
        public UnitedAccessoriesDBEntities()
            : base("name=UnitedAccessoriesDBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<ArtWorkUpload> ArtWorkUploads { get; set; }
        public DbSet<AccessToUser> AccessToUsers { get; set; }
        public DbSet<Url> Urls { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Size> Sizes { get; set; }
        public DbSet<Breakdown> Breakdowns { get; set; }
        public DbSet<Indent> Indents { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<WOSize> WOSizes { get; set; }
        public DbSet<PurchaseOrder> PurchaseOrders { get; set; }
    }
}