using System.Data.Entity.ModelConfiguration;
using BhuInfo.Data.Objects.Entities;

namespace BhuInfo.Data.Object.Mapping.ObjectMappings
{
   public class EventMapping : EntityTypeConfiguration<Event>
    {
        public EventMapping()
        {
            //this property is a primary key
            this.HasKey(n => n.EventId);

            //this are the remaining properties
            this.Property(n => n.EventName)
                .IsRequired();
            this.Property(n => n.Organizer)
               .IsRequired();
            this.Property(n => n.Venue)
            .IsRequired();
            this.Property(n => n.StartDate)
            .IsRequired();
            this.Property(n => n.EndDate)
            .IsRequired();

            //mapping the object entities to their respective columns
            this.ToTable("Events");
            this.Property(n => n.EventId).HasColumnName("EventId");
            this.Property(n => n.EndDate).HasColumnName("EndDate");
            this.Property(n => n.StartDate).HasColumnName("StartDate");
            this.Property(n => n.EventName).HasColumnName("EventName");
            this.Property(n => n.Venue).HasColumnName("Venue");
            this.Property(n => n.DateCreated).HasColumnName("DateCreated");
        }
    }
}
