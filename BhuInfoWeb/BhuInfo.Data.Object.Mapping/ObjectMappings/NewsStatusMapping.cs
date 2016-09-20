using System.Data.Entity.ModelConfiguration;
using BhuInfo.Data.Objects.Entities;

namespace BhuInfo.Data.Object.Mapping.ObjectMappings
{
    public class NewsStatusMapping : EntityTypeConfiguration<NewsStatus>
    {
        public NewsStatusMapping()
        {
            //this property is a primary key
            HasKey(n => n.StatusId);

            //this are the remaining properties
            Property(n => n.LoggedInUserId)
                .IsRequired();
            Property(n => n.Status)
                .IsRequired();
            Property(n => n.NewsId)
                .IsRequired();
            //mapping the object entities to their respective columns
            ToTable("NewsStatus");
            Property(n => n.StatusId).HasColumnName("StatusId");
            Property(n => n.LoggedInUserId).HasColumnName("LoggedInUserId");
            Property(n => n.Status).HasColumnName("Status");
            Property(n => n.NewsId).HasColumnName("NewsId");
        }
    }
}