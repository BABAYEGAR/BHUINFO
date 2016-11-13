namespace BhuInfo.Data.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migration1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.News",
                c => new
                    {
                        NewsId = c.Long(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 100),
                        Content = c.String(nullable: false),
                        Image = c.String(),
                        SecondImage = c.String(),
                        ThirdImage = c.String(),
                        CreatedById = c.Long(nullable: false),
                        NewsCategoryId = c.Long(nullable: false),
                        LastModifiedById = c.Long(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        DateLastModified = c.DateTime(nullable: false),
                        NewsView = c.Int(nullable: false),
                        Likes = c.Int(nullable: false),
                        Dislikes = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.NewsId)
                .ForeignKey("dbo.NewsCategories", t => t.NewsCategoryId, cascadeDelete: true)
                .Index(t => t.NewsCategoryId);
            
            CreateTable(
                "dbo.NewsComments",
                c => new
                    {
                        NewsCommentId = c.Long(nullable: false, identity: true),
                        CommentBy = c.String(),
                        AppUserId = c.Long(nullable: false),
                        Email = c.String(),
                        Comment = c.String(nullable: false),
                        NewsId = c.Long(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        Likes = c.Int(nullable: false),
                        Dislikes = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.NewsCommentId)
                .ForeignKey("dbo.AppUsers", t => t.AppUserId, cascadeDelete: true)
                .ForeignKey("dbo.News", t => t.NewsId, cascadeDelete: true)
                .Index(t => t.AppUserId)
                .Index(t => t.NewsId);
            
            CreateTable(
                "dbo.AppUsers",
                c => new
                    {
                        AppUserId = c.Long(nullable: false, identity: true),
                        Firstname = c.String(nullable: false, maxLength: 100),
                        Lastname = c.String(nullable: false, maxLength: 100),
                        Email = c.String(nullable: false, maxLength: 100),
                        Mobile = c.String(nullable: false, maxLength: 100),
                        MatricNumber = c.String(),
                        Password = c.String(),
                        Role = c.String(),
                        DateCreated = c.DateTime(nullable: false),
                        DateLastModified = c.DateTime(nullable: false),
                        CreatedById = c.Long(nullable: false),
                        LastModifiedById = c.Long(nullable: false),
                        AppUserImage = c.String(),
                        RememberMe = c.Boolean(nullable: false),
                        Token = c.String(),
                    })
                .PrimaryKey(t => t.AppUserId);
            
            CreateTable(
                "dbo.SchoolDiscussionComments",
                c => new
                    {
                        SchoolDiscussionCommentId = c.Long(nullable: false, identity: true),
                        CommentBy = c.String(),
                        AppUserId = c.Long(nullable: false),
                        Email = c.String(),
                        Comment = c.String(nullable: false),
                        SchoolDiscussionId = c.Long(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.SchoolDiscussionCommentId)
                .ForeignKey("dbo.AppUsers", t => t.AppUserId, cascadeDelete: true)
                .ForeignKey("dbo.SchoolDiscussions", t => t.SchoolDiscussionId, cascadeDelete: true)
                .Index(t => t.AppUserId)
                .Index(t => t.SchoolDiscussionId);
            
            CreateTable(
                "dbo.SchoolDiscussions",
                c => new
                    {
                        SchoolDiscussionId = c.Long(nullable: false, identity: true),
                        Topic = c.String(nullable: false),
                        Content = c.String(nullable: false),
                        Status = c.String(),
                        CreatedById = c.Long(nullable: false),
                        LastModifiedById = c.Long(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        DateLastModified = c.DateTime(nullable: false),
                        DiscussionView = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SchoolDiscussionId);
            
            CreateTable(
                "dbo.NewsCategories",
                c => new
                    {
                        NewsCategoryId = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        DateCreated = c.DateTime(),
                        DateLastModified = c.DateTime(),
                        CreatedById = c.Long(nullable: false),
                        LastModifiedById = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.NewsCategoryId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.News", "NewsCategoryId", "dbo.NewsCategories");
            DropForeignKey("dbo.NewsComments", "NewsId", "dbo.News");
            DropForeignKey("dbo.SchoolDiscussionComments", "SchoolDiscussionId", "dbo.SchoolDiscussions");
            DropForeignKey("dbo.SchoolDiscussionComments", "AppUserId", "dbo.AppUsers");
            DropForeignKey("dbo.NewsComments", "AppUserId", "dbo.AppUsers");
            DropIndex("dbo.SchoolDiscussionComments", new[] { "SchoolDiscussionId" });
            DropIndex("dbo.SchoolDiscussionComments", new[] { "AppUserId" });
            DropIndex("dbo.NewsComments", new[] { "NewsId" });
            DropIndex("dbo.NewsComments", new[] { "AppUserId" });
            DropIndex("dbo.News", new[] { "NewsCategoryId" });
            DropTable("dbo.NewsCategories");
            DropTable("dbo.SchoolDiscussions");
            DropTable("dbo.SchoolDiscussionComments");
            DropTable("dbo.AppUsers");
            DropTable("dbo.NewsComments");
            DropTable("dbo.News");
        }
    }
}
