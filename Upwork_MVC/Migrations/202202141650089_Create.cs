namespace Upwork_MVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Create : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Agreements",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 30),
                        ProductId = c.Int(nullable: false),
                        EffectiveDate = c.DateTime(nullable: false),
                        ExpirationDate = c.DateTime(nullable: false),
                        ProductPrice = c.Decimal(nullable: false, precision: 19, scale: 2),
                        NewPrice = c.Decimal(nullable: false, precision: 19, scale: 2),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductGroupId = c.Int(nullable: false),
                        ProductDescription = c.String(nullable: false, maxLength: 200),
                        ProductNumber = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 19, scale: 2),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ProductGroups", t => t.ProductGroupId, cascadeDelete: true)
                .Index(t => t.ProductGroupId)
                .Index(t => t.ProductNumber, unique: true, name: "IX_ProductNumber_Unique");
            
            CreateTable(
                "dbo.ProductGroups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GroupDescription = c.String(nullable: false, maxLength: 200),
                        GroupCode = c.String(nullable: false, maxLength: 20),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.GroupCode, unique: true, name: "IX_ProductGroup_Unique");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "ProductGroupId", "dbo.ProductGroups");
            DropForeignKey("dbo.Agreements", "ProductId", "dbo.Products");
            DropIndex("dbo.ProductGroups", "IX_ProductGroup_Unique");
            DropIndex("dbo.Products", "IX_ProductNumber_Unique");
            DropIndex("dbo.Products", new[] { "ProductGroupId" });
            DropIndex("dbo.Agreements", new[] { "ProductId" });
            DropTable("dbo.ProductGroups");
            DropTable("dbo.Products");
            DropTable("dbo.Agreements");
        }
    }
}
