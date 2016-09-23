using System.Data.Entity.ModelConfiguration;
using BhuInfo.Data.Objects.Entities;

namespace BhuInfo.Data.Object.Mapping.ObjectMappings
{
    public class AdvertMapping : EntityTypeConfiguration<Advertisement>
    {
        public AdvertMapping()
        {
            //this property is a primary key
            HasKey(n => n.AdvertId);

            //this are the remaining properties
            Property(n => n.AdvertCompanyName)
                .IsRequired();
            Property(n => n.AdvertImage)
                .IsRequired();
            Property(n => n.AdvertText)
                .IsRequired();
            Property(n => n.AdvertCompanyName)
                .IsRequired();
            //mapping the object entities to their respective columns
            ToTable("Advertisement");
            Property(n => n.AdvertId).HasColumnName("AdvertId");
            Property(n => n.AdvertImage).HasColumnName("AdvertImage");
            Property(n => n.AdvertCompanyName).HasColumnName("AdvertCompanyName");
            Property(n => n.AdvertText).HasColumnName("AdvertText");
            Property(n => n.AccessCode).HasColumnName("AccessCode");
            Property(n => n.Status).HasColumnName("Status");
        }
    }
}