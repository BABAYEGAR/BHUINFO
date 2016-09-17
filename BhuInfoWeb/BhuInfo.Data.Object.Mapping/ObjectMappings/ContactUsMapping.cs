using System.Data.Entity.ModelConfiguration;
using BhuInfo.Data.Objects.Entities;

namespace BhuInfo.Data.Object.Mapping.ObjectMappings
{
    public class ContactUsMapping : EntityTypeConfiguration<ContactUs>

    {
        public ContactUsMapping()
        {
            //this property is a primary key
            HasKey(n => n.ContactUsId);

            //this are the remaining properties
            Property(n => n.Email)
                .IsRequired();
            Property(n => n.Message)
                .IsRequired();
            Property(n => n.SenderName)
                .IsRequired();

            //mapping the object entities to their respective columns
            ToTable("ContactUs");
            Property(n => n.ContactUsId).HasColumnName("ContactUsId");
            Property(n => n.Email).HasColumnName("Email");
            Property(n => n.Message).HasColumnName("Message");
            Property(n => n.SenderName).HasColumnName("SenderName");
            Property(n => n.DateCreated).HasColumnName("DateCreated");
        }
    }
}