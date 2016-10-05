namespace BhuInfo.Data.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class firstmigration : DbMigration
    {
        public override void Up()
        {
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
                "dbo.SchoolDiscussionComments",
                c => new
                    {
                        SchoolDiscussionCommentId = c.Long(nullable: false, identity: true),
                        CommentBy = c.String(),
                        Email = c.String(),
                        Comment = c.String(nullable: false),
                        SchoolDiscussionId = c.Long(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.SchoolDiscussionCommentId)
                .ForeignKey("dbo.SchoolDiscussions", t => t.SchoolDiscussionId, cascadeDelete: true)
                .Index(t => t.SchoolDiscussionId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SchoolDiscussionComments", "SchoolDiscussionId", "dbo.SchoolDiscussions");
            DropIndex("dbo.SchoolDiscussionComments", new[] { "SchoolDiscussionId" });
            DropTable("dbo.SchoolDiscussionComments");
            DropTable("dbo.SchoolDiscussions");
        }
    }
}
