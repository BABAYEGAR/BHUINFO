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
            Property(n => n.AdvertLogo)
                .IsRequired();
            Property(n => n.AdvertText)
                .IsRequired();
            //mapping the object entities to their respective columns
            ToTable("Advertisement");
            Property(n => n.AdvertId).HasColumnName("AdvertId");
            Property(n => n.AdvertLogo).HasColumnName("AdvertLogo");
            Property(n => n.AdvertCompanyName).HasColumnName("AdvertCompanyName");
            Property(n => n.AdvertText).HasColumnName("AdvertText");
        }
    }
}