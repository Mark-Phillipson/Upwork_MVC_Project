using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Annotations;
using System.Linq;
using System.Web;
using Upwork_MVC.Models;

namespace Upwork_MVC.Data
{
    public class ProductAgreementDbContext : DbContext
    {
        public ProductAgreementDbContext() : base("DefaultConnection")
        {
            //Database.Log = sql => Debug.Write("" + sql);
        }
        public DbSet<ProductGroup> ProductGroups { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Agreement> Agreements { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .Property(e => e.Price)
                .HasPrecision(19, 2);
            modelBuilder.Entity<Agreement>()
                .Property(e => e.ProductPrice)
                .HasPrecision(19, 2);
            modelBuilder.Entity<Agreement>()
                .Property(e => e.NewPrice)
                .HasPrecision(19, 2);
            modelBuilder.Entity<Product>()
                .Property(p => p.ProductNumber)
                .IsRequired()
                .HasColumnAnnotation(
                "Index", new IndexAnnotation(new IndexAttribute("IX_ProductNumber_Unique") { IsUnique = true }));
            modelBuilder.Entity<ProductGroup>()
                .Property(p => p.GroupCode)
                .IsRequired()
                .HasColumnAnnotation(
                "Index", new IndexAnnotation(new IndexAttribute("IX_ProductGroup_Unique") { IsUnique = true }));


        }
    }
}