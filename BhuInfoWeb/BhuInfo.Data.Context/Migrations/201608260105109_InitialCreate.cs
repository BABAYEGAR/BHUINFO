namespace BhuInfo.Data.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.NewsCategories",
                c => new
                    {
                        NewsCategoryId = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        DateCreated = c.DateTime(),
                        DateLastModified = c.DateTime(),
                        CreatedById = c.Long(nullable: false),
                        LastModifiedById = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.NewsCategoryId);
            
            CreateTable(
                "dbo.News",
                c => new
                    {
                        NewsId = c.Long(nullable: false, identity: true),
                        Title = c.Long(nullable: false),
                        Content = c.Long(nullable: false),
                        Image = c.String(),
                        CreatedById = c.Long(nullable: false),
                        LastModifiedById = c.Long(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        DateLastModified = c.DateTime(nullable: false),
                        NewsCategory_NewsCategoryId = c.Long(),
                    })
                .PrimaryKey(t => t.NewsId)
                .ForeignKey("dbo.NewsCategories", t => t.NewsCategory_NewsCategoryId)
                .Index(t => t.NewsCategory_NewsCategoryId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.News", "NewsCategory_NewsCategoryId", "dbo.NewsCategories");
            DropIndex("dbo.News", new[] { "NewsCategory_NewsCategoryId" });
            DropTable("dbo.News");
            DropTable("dbo.NewsCategories");
        }
    }
}
