﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SecurityEF
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class SecurityDBEntities : DbContext
    {
        public SecurityDBEntities()
            : base("name=SecurityDBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<SecurityForm> SecurityForms { get; set; }
        public DbSet<SecurityFormAction> SecurityFormActions { get; set; }
        public DbSet<SecurityRole> SecurityRoles { get; set; }
        public DbSet<SecurityRoleAccess> SecurityRoleAccesses { get; set; }
        public DbSet<SecurityRoleApplication> SecurityRoleApplications { get; set; }
        public DbSet<SecurityUser> SecurityUsers { get; set; }
        public DbSet<SecurityUserAccess> SecurityUserAccesses { get; set; }
        public DbSet<SecurityUserApplication> SecurityUserApplications { get; set; }
        public DbSet<SecurityUserRole> SecurityUserRoles { get; set; }
        public DbSet<SecurityApplication> SecurityApplications { get; set; }
        public DbSet<SecurityLogin> SecurityLogins { get; set; }
    }
}