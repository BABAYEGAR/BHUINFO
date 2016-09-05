using System.Data.Entity.ModelConfiguration;
using BhuInfo.Data.Objects.Entities;

namespace BhuInfo.Data.Object.Mapping.ObjectMappings
{
    public class NewsCategoryMapping : EntityTypeConfiguration<NewsCategory>
    {
        public NewsCategoryMapping()
        {
            //this property is a primary key
            HasKey(n => n.NewsCategoryId);

            //this are the remaining properties
            Property(n => n.Name)
                .IsRequired();

            //mapping the object entities to their respective columns
            ToTable("NewsCategories");
            Property(n => n.NewsCategoryId).HasColumnName("NewsCategoryId");
            Property(n => n.Name).HasColumnName("Name");
            Property(n => n.CreatedById).HasColumnName("CreatedById");
            Property(n => n.LastModifiedById).HasColumnName("LastModifiedById");
            Property(n => n.DateCreated).HasColumnName("DateCreated");
            Property(n => n.DateLastModified).HasColumnName("DateLastModified");
        }
    }
}