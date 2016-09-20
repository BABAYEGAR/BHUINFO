using System.Data.Entity.ModelConfiguration;
using BhuInfo.Data.Objects.Entities;

namespace BhuInfo.Data.Object.Mapping.ObjectMappings
{
    public class EventMapping : EntityTypeConfiguration<Event>
    {
        public EventMapping()
        {
            //this property is a primary key
            HasKey(n => n.EventId);

            //this are the remaining properties
            Property(n => n.EventName)
                .IsRequired();
            Property(n => n.Organizer)
                .IsRequired();
            Property(n => n.Venue)
                .IsRequired();
            Property(n => n.StartDate)
                .IsRequired();
            Property(n => n.EndDate)
                .IsRequired();
            Property(n => n.StartTime)
                .IsRequired();
            Property(n => n.EndTime)
                .IsRequired();

            //mapping the object entities to their respective columns
            ToTable("Events");
            Property(n => n.EventId).HasColumnName("EventId");
            Property(n => n.EndDate).HasColumnName("EndDate");
            Property(n => n.StartDate).HasColumnName("StartDate");
            Property(n => n.StartTime).HasColumnName("StartTime");
            Property(n => n.EndTime).HasColumnName("EndTime");
            Property(n => n.EventName).HasColumnName("EventName");
            Property(n => n.Venue).HasColumnName("Venue");
            Property(n => n.CreatedById).HasColumnName("CreatedById");
            Property(n => n.LastModifiedById).HasColumnName("LastModifiedById");
            Property(n => n.DateCreated).HasColumnName("DateCreated");
            Property(n => n.DateLastModified).HasColumnName("DateLastModified");
        }
    }
}