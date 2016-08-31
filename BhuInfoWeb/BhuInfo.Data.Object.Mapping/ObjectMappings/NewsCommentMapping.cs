using System.Data.Entity.ModelConfiguration;
using BhuInfo.Data.Objects.Entities;

namespace BhuInfo.Data.Object.Mapping.ObjectMappings
{
    public class NewsCommentMapping : EntityTypeConfiguration<NewsComment>
        
    {
        public NewsCommentMapping()
        {
            //this property is a primary key
            this.HasKey(n => n.NewsCommentId);

            //this are the remaining properties
            this.Property(n => n.CommentBy)
                .IsRequired();
            this.Property(n => n.Email)
               .IsRequired();
            this.Property(n => n.Comment)
              .IsRequired();

            //mapping the object entities to their respective columns
            this.ToTable("NewsComments");
            this.Property(n => n.NewsCommentId).HasColumnName("NewsCommentId");
            this.Property(n => n.Comment).HasColumnName("Comment");
            this.Property(n => n.CommentBy).HasColumnName("CommentBy");
            this.Property(n => n.Email).HasColumnName("Email");
            this.Property(n => n.NewsId).HasColumnName("NewsId");
            this.Property(n => n.DateCreated).HasColumnName("DateCreated");
            
        }
    }
}
