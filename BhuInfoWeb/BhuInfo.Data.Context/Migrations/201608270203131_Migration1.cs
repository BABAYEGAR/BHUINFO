namespace BhuInfo.Data.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.News", "NewsCategory_NewsCategoryId", "dbo.NewsCategories");
            DropIndex("dbo.News", new[] { "NewsCategory_NewsCategoryId" });
            RenameColumn(table: "dbo.News", name: "NewsCategory_NewsCategoryId", newName: "NewsCategoryId");
            AlterColumn("dbo.News", "Title", c => c.String());
            AlterColumn("dbo.News", "Content", c => c.String());
            AlterColumn("dbo.News", "NewsCategoryId", c => c.Long(nullable: false));
            CreateIndex("dbo.News", "NewsCategoryId");
            AddForeignKey("dbo.News", "NewsCategoryId", "dbo.NewsCategories", "NewsCategoryId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.News", "NewsCategoryId", "dbo.NewsCategories");
            DropIndex("dbo.News", new[] { "NewsCategoryId" });
            AlterColumn("dbo.News", "NewsCategoryId", c => c.Long());
            AlterColumn("dbo.News", "Content", c => c.Long(nullable: false));
            AlterColumn("dbo.News", "Title", c => c.Long(nullable: false));
            RenameColumn(table: "dbo.News", name: "NewsCategoryId", newName: "NewsCategory_NewsCategoryId");
            CreateIndex("dbo.News", "NewsCategory_NewsCategoryId");
            AddForeignKey("dbo.News", "NewsCategory_NewsCategoryId", "dbo.NewsCategories", "NewsCategoryId");
        }
    }
}
