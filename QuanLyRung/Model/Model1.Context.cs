﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace QuanLyRung.Model
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class QuanLyCSDLTaiNguyenRungEntities : DbContext
    {
        public QuanLyCSDLTaiNguyenRungEntities()
            : base("name=QuanLyCSDLTaiNguyenRungEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Animal> Animal { get; set; }
        public virtual DbSet<StorageAnimal> StorageAnimal { get; set; }
        public virtual DbSet<SupplierTree> SupplierTree { get; set; }
        public virtual DbSet<SupplierWood> SupplierWood { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<Tree> Tree { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<Wood> Wood { get; set; }
    }
}