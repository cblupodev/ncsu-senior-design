﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EFSQLConnector
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class MappingContext : DbContext
    {
        public MappingContext()
            : base("name=MappingContext")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<assembly_map> assembly_map { get; set; }
        public virtual DbSet<namespace_map> namespace_map { get; set; }
        public virtual DbSet<sdk_map2> sdk_map2 { get; set; }
        public virtual DbSet<sdk2> sdk2 { get; set; }
    }
}
