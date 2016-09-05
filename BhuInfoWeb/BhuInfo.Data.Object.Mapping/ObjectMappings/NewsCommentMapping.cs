using System.Data.Entity.ModelConfiguration;
using BhuInfo.Data.Objects.Entities;

namespace BhuInfo.Data.Object.Mapping.ObjectMappings
{
    public class NewsCommentMapping : EntityTypeConfiguration<NewsComment>

    {
        public NewsCommentMapping()
        {
            //this property is a primary key
            HasKey(n => n.NewsCommentId);

            //this are the remaining properties
            Property(n => n.CommentBy)
                .IsRequired();
            Property(n => n.Email)
                .IsRequired();
            Property(n => n.Comment)
                .IsRequired();

            //mapping the object entities to their respective columns
            ToTable("NewsComments");
            Property(n => n.NewsCommentId).HasColumnName("NewsCommentId");
            Property(n => n.Comment).HasColumnName("Comment");
            Property(n => n.CommentBy).HasColumnName("CommentBy");
            Property(n => n.Email).HasColumnName("Email");
            Property(n => n.NewsId).HasColumnName("NewsId");
            Property(n => n.DateCreated).HasColumnName("DateCreated");
        }
    }
}