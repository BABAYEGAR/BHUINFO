using System.Data.Entity.Migrations;

namespace BhuInfo.Data.Context.Migrations
{
    public partial class NewsCategoryDataContext : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.News", "NewsCategory_NewsCategoryId", "dbo.NewsCategories");
            DropIndex("dbo.News", new[] {"NewsCategory_NewsCategoryId"});
            RenameColumn("dbo.News", "NewsCategory_NewsCategoryId", "NewsCategoryId");
            CreateTable(
                    "dbo.NewsComments",
                    c => new
                    {
                        NewsCommentId = c.Long(false, true),
                        CommentBy = c.String(),
                        Email = c.String(),
                        Comment = c.String(),
                        NewsId = c.Long(false),
                        DateCreated = c.DateTime(false)
                    })
                .PrimaryKey(t => t.NewsCommentId)
                .ForeignKey("dbo.News", t => t.NewsId, true)
                .Index(t => t.NewsId);

            AddColumn("dbo.News", "NewsView", c => c.Int(false));
            AlterColumn("dbo.News", "Title", c => c.String());
            AlterColumn("dbo.News", "Content", c => c.String());
            AlterColumn("dbo.News", "NewsCategoryId", c => c.Long(false));
            CreateIndex("dbo.News", "NewsCategoryId");
            AddForeignKey("dbo.News", "NewsCategoryId", "dbo.NewsCategories", "NewsCategoryId", true);
        }

        public override void Down()
        {
            DropForeignKey("dbo.News", "NewsCategoryId", "dbo.NewsCategories");
            DropForeignKey("dbo.NewsComments", "NewsId", "dbo.News");
            DropIndex("dbo.NewsComments", new[] {"NewsId"});
            DropIndex("dbo.News", new[] {"NewsCategoryId"});
            AlterColumn("dbo.News", "NewsCategoryId", c => c.Long());
            AlterColumn("dbo.News", "Content", c => c.Long(false));
            AlterColumn("dbo.News", "Title", c => c.Long(false));
            DropColumn("dbo.News", "NewsView");
            DropTable("dbo.NewsComments");
            RenameColumn("dbo.News", "NewsCategoryId", "NewsCategory_NewsCategoryId");
            CreateIndex("dbo.News", "NewsCategory_NewsCategoryId");
            AddForeignKey("dbo.News", "NewsCategory_NewsCategoryId", "dbo.NewsCategories", "NewsCategoryId");
        }
    }
}