namespace BhuInfo.Data.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewsCategoryDataContext : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.News", "NewsCategory_NewsCategoryId", "dbo.NewsCategories");
            DropIndex("dbo.News", new[] { "NewsCategory_NewsCategoryId" });
            RenameColumn(table: "dbo.News", name: "NewsCategory_NewsCategoryId", newName: "NewsCategoryId");
            CreateTable(
                "dbo.NewsComments",
                c => new
                    {
                        NewsCommentId = c.Long(nullable: false, identity: true),
                        CommentBy = c.String(),
                        Email = c.String(),
                        Comment = c.String(),
                        NewsId = c.Long(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.NewsCommentId)
                .ForeignKey("dbo.News", t => t.NewsId, cascadeDelete: true)
                .Index(t => t.NewsId);
            
            AddColumn("dbo.News", "NewsView", c => c.Int(nullable: false));
            AlterColumn("dbo.News", "Title", c => c.String());
            AlterColumn("dbo.News", "Content", c => c.String());
            AlterColumn("dbo.News", "NewsCategoryId", c => c.Long(nullable: false));
            CreateIndex("dbo.News", "NewsCategoryId");
            AddForeignKey("dbo.News", "NewsCategoryId", "dbo.NewsCategories", "NewsCategoryId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.News", "NewsCategoryId", "dbo.NewsCategories");
            DropForeignKey("dbo.NewsComments", "NewsId", "dbo.News");
            DropIndex("dbo.NewsComments", new[] { "NewsId" });
            DropIndex("dbo.News", new[] { "NewsCategoryId" });
            AlterColumn("dbo.News", "NewsCategoryId", c => c.Long());
            AlterColumn("dbo.News", "Content", c => c.Long(nullable: false));
            AlterColumn("dbo.News", "Title", c => c.Long(nullable: false));
            DropColumn("dbo.News", "NewsView");
            DropTable("dbo.NewsComments");
            RenameColumn(table: "dbo.News", name: "NewsCategoryId", newName: "NewsCategory_NewsCategoryId");
            CreateIndex("dbo.News", "NewsCategory_NewsCategoryId");
            AddForeignKey("dbo.News", "NewsCategory_NewsCategoryId", "dbo.NewsCategories", "NewsCategoryId");
        }
    }
}
