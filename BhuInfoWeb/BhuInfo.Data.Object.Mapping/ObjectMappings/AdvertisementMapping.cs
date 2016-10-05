using System.Data.Entity.ModelConfiguration;
using BhuInfo.Data.Objects.Entities;

namespace BhuInfo.Data.Object.Mapping.ObjectMappings
{
    public class AdvertisementtMapping : EntityTypeConfiguration<Advertisement>
    {
        public AdvertisementtMapping()
        {
            //this property is a primary key
            HasKey(n => n.AdvertisementId);

            //this are the remaining properties
            Property(n => n.AdvertCompanyName)
                .IsRequired();
            Property(n => n.AdvertType)
              .IsRequired();
            Property(n => n.Email)
             .IsRequired();
            Property(n => n.Status)
             .IsRequired();
            //mapping the object entities to their respective columns
            ToTable("Advertisements");
            Property(n => n.AdvertisementId).HasColumnName("AdvertisementId");
            Property(n => n.AdvertImage).HasColumnName("AdvertImage");
            Property(n => n.AdvertCompanyName).HasColumnName("AdvertCompanyName");
            Property(n => n.AdvertText).HasColumnName("AdvertText");
            Property(n => n.AccessCode).HasColumnName("AccessCode");
            Property(n => n.Status).HasColumnName("Status");
            Property(n => n.AdvertType).HasColumnName("AdvertType");
            Property(n => n.Email).HasColumnName("Email");
            Property(n => n.CreatedById).HasColumnName("CreatedById");
            Property(n => n.LastModifiedById).HasColumnName("LastModifiedById");
            Property(n => n.DateCreated).HasColumnName("DateCreated");
            Property(n => n.DateLastModified).HasColumnName("DateLastModified");
        }
    }
}