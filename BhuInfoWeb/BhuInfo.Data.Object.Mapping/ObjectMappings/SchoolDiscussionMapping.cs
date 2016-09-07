using System.Data.Entity.ModelConfiguration;
using BhuInfo.Data.Objects.Entities;

namespace BhuInfo.Data.Object.Mapping.ObjectMappings
{
    public class SchoolDiscussionMapping : EntityTypeConfiguration<SchoolDiscussion>

    {
        public SchoolDiscussionMapping()
        {
            //this property is a primary key
            HasKey(n => n.SchoolDiscussionId);

            //this are the remaining properties
            Property(n => n.Content)
                .IsRequired();
            Property(n => n.Topic)
                .IsRequired();

            //mapping the object entities to their respective columns
            ToTable("SchoolDiscussions");
            Property(n => n.SchoolDiscussionId).HasColumnName("SchoolDiscussionId");
            Property(n => n.Topic).HasColumnName("Topic");
            Property(n => n.Content).HasColumnName("Content");
            Property(n => n.Status).HasColumnName("Status");
            Property(n => n.CreatedById).HasColumnName("CreatedById");
            Property(n => n.DiscussionView).HasColumnName("DiscussionView");
            Property(n => n.LastModifiedById).HasColumnName("LastModifiedById");
            Property(n => n.DateCreated).HasColumnName("DateCreated");
            Property(n => n.DateLastModified).HasColumnName("DateLastModified");
        }
    }
}