namespace Upwork_MVC.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Upwork_MVC.Data;
    using Upwork_MVC.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<Upwork_MVC.Data.ProductAgreementDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Upwork_MVC.Data.ProductAgreementDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.

            var ProductGroups = new List<ProductGroup>
            {
            new ProductGroup{GroupDescription="Product Group 1",GroupCode="PG1",Active=true},
            new ProductGroup{GroupDescription="Product Group 2",GroupCode="PG2",Active=true}
            };

            ProductGroups.ForEach(s => context.ProductGroups.AddOrUpdate(s));
            context.SaveChanges();
            var products = new List<Product>
            {
            new Product{ProductGroupId=1,ProductDescription="Product Description 1",ProductNumber=1,Price=1.11M,Active=true},
            new Product{ProductGroupId=2,ProductDescription="Product Description 2",ProductNumber=2,Price= 2.22M,Active=true}
            };
            products.ForEach(s => context.Products.AddOrUpdate(s));
            context.SaveChanges();
            var agreements = new List<Agreement>
            {
            new Agreement{UserId="SeedData",ProductId=1,EffectiveDate=DateTime.Now.Date,ExpirationDate=DateTime.Now.AddDays( 50 ).Date,ProductPrice= 1.11M,NewPrice= 1.10M},
              new Agreement {UserId="SeedData",ProductId=2,EffectiveDate=DateTime.Now.Date,ExpirationDate=DateTime.Now.AddDays( 50).Date,ProductPrice= 1.11M,NewPrice= 1.10M},
            };

            agreements.ForEach(s => context.Agreements.AddOrUpdate(s));
            context.SaveChanges();

        }
    }
}
