using System.Data.Entity.ModelConfiguration;
using BhuInfo.Data.Objects.Entities;

namespace BhuInfo.Data.Object.Mapping.ObjectMappings
{
    public class ContactUsMapping : EntityTypeConfiguration<ContactUs>
  
    {
        public ContactUsMapping()
        {
            //this property is a primary key
            this.HasKey(n => n.ContactUsId);

            //this are the remaining properties
            this.Property(n => n.Email)
                .IsRequired();
            this.Property(n => n.Message)
               .IsRequired();
            this.Property(n => n.SenderName)
              .IsRequired();

            //mapping the object entities to their respective columns
            this.ToTable("ContactUs");
            this.Property(n => n.ContactUsId).HasColumnName("ContactUsId");
            this.Property(n => n.Email).HasColumnName("Email");
            this.Property(n => n.Message).HasColumnName("Message");
            this.Property(n => n.Email).HasColumnName("SenderName");
            this.Property(n => n.DateCreated).HasColumnName("DateCreated");
            
        }
    }
}
