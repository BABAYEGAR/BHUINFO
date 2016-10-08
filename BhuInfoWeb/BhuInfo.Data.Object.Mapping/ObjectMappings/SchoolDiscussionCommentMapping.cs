using System.Data.Entity.ModelConfiguration;
using BhuInfo.Data.Objects.Entities;

namespace BhuInfo.Data.Object.Mapping.ObjectMappings
{
    public class SchoolDiscussionCommentMapping : EntityTypeConfiguration<SchoolDiscussionComment>

    {
        public SchoolDiscussionCommentMapping()
        {
            //this property is a primary key
            HasKey(n => n.SchoolDiscussionCommentId);

            //this are the remaining properties
            Property(n => n.CommentBy)
                .IsRequired();
            Property(n => n.Email)
                .IsRequired();
            Property(n => n.Comment)
                .IsRequired();

            //mapping the object entities to their respective columns
            ToTable("SchoolDiscussionComments");
            Property(n => n.SchoolDiscussionCommentId).HasColumnName("SchoolDiscussionCommentId");
            Property(n => n.Comment).HasColumnName("Comment");
            Property(n => n.CommentBy).HasColumnName("CommentBy");
            Property(n => n.Email).HasColumnName("Email");
            Property(n => n.SchoolDiscussionId).HasColumnName("SchoolDiscussionId");
            Property(n => n.DateCreated).HasColumnName("DateCreated");
            Property(n => n.AppUserId).HasColumnName("AppUserId");
        }
    }
}