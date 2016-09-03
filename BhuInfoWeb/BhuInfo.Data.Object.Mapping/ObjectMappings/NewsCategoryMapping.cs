using System.Data.Entity.ModelConfiguration;
using BhuInfo.Data.Objects.Entities;

namespace BhuInfo.Data.Object.Mapping.ObjectMappings
{
    public class NewsCategoryMapping : EntityTypeConfiguration<NewsCategory>
    {
        public NewsCategoryMapping()
        {
            //this property is a primary key
            this.HasKey(n => n.NewsCategoryId);

            //this are the remaining properties
            this.Property(n => n.Name)
                .IsRequired();

            //mapping the object entities to their respective columns
            this.ToTable("NewsCategories");
            this.Property(n => n.NewsCategoryId).HasColumnName("NewsCategoryId");
            this.Property(n => n.Name).HasColumnName("Name");
            this.Property(n => n.CreatedById).HasColumnName("CreatedById");
            this.Property(n => n.LastModifiedById).HasColumnName("LastModifiedById");
            this.Property(n => n.DateCreated).HasColumnName("DateCreated");
            this.Property(n => n.DateLastModified).HasColumnName("DateLastModified");
        }
    }
}
