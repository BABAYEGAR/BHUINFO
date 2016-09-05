using System.Data.Entity.Migrations;

namespace BhuInfo.Data.Context.Migrations
{
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                    "dbo.NewsCategories",
                    c => new
                    {
                        NewsCategoryId = c.Long(false, true),
                        Name = c.String(),
                        DateCreated = c.DateTime(),
                        DateLastModified = c.DateTime(),
                        CreatedById = c.Long(false),
                        LastModifiedById = c.Long(false)
                    })
                .PrimaryKey(t => t.NewsCategoryId);

            CreateTable(
                    "dbo.News",
                    c => new
                    {
                        NewsId = c.Long(false, true),
                        Title = c.Long(false),
                        Content = c.Long(false),
                        Image = c.String(),
                        CreatedById = c.Long(false),
                        LastModifiedById = c.Long(false),
                        DateCreated = c.DateTime(false),
                        DateLastModified = c.DateTime(false),
                        NewsCategory_NewsCategoryId = c.Long()
                    })
                .PrimaryKey(t => t.NewsId)
                .ForeignKey("dbo.NewsCategories", t => t.NewsCategory_NewsCategoryId)
                .Index(t => t.NewsCategory_NewsCategoryId);
        }

        public override void Down()
        {
            DropForeignKey("dbo.News", "NewsCategory_NewsCategoryId", "dbo.NewsCategories");
            DropIndex("dbo.News", new[] {"NewsCategory_NewsCategoryId"});
            DropTable("dbo.News");
            DropTable("dbo.NewsCategories");
        }
    }
}