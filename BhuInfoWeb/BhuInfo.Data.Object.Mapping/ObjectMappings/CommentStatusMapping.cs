using System.Data.Entity.ModelConfiguration;
using BhuInfo.Data.Objects.Entities;

namespace BhuInfo.Data.Object.Mapping.ObjectMappings
{
    public class CommentStatusMapping : EntityTypeConfiguration<CommentStatus>
    {
        public CommentStatusMapping()
        {
            //this property is a primary key
            HasKey(n => n.CommentStatusId);

            //this are the remaining properties
            Property(n => n.LoggedInUserId)
                .IsRequired();
            Property(n => n.Status)
                .IsRequired();
            Property(n => n.CommentId)
                .IsRequired();
            //mapping the object entities to their respective columns
            ToTable("CommentStatus");
            Property(n => n.CommentStatusId).HasColumnName("CommentStatusId");
            Property(n => n.LoggedInUserId).HasColumnName("LoggedInUserId");
            Property(n => n.Status).HasColumnName("Status");
            Property(n => n.CommentId).HasColumnName("CommentId");
        }
    }
}