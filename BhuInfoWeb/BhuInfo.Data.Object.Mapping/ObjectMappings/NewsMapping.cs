using System.Data.Entity.ModelConfiguration;
using BhuInfo.Data.Objects.Entities;

namespace BhuInfo.Data.Object.Mapping.ObjectMappings
{
    public class NewsMapping : EntityTypeConfiguration<News>

    {
        public NewsMapping()
        {
            //this property is a primary key
            HasKey(n => n.NewsId);

            //this are the remaining properties
            Property(n => n.Title)
                .IsRequired();
            Property(n => n.Content)
                .IsRequired();

            //mapping the object entities to their respective columns
            ToTable("News");
            Property(n => n.NewsId).HasColumnName("NewsId");
            Property(n => n.Title).HasColumnName("Title");
            Property(n => n.Content).HasColumnName("Content");
            Property(n => n.Image).HasColumnName("Image");
            Property(n => n.CreatedById).HasColumnName("CreatedById");
            Property(n => n.NewsCategoryId).HasColumnName("NewsCategoryId");
            Property(n => n.LastModifiedById).HasColumnName("LastModifiedById");
            Property(n => n.DateCreated).HasColumnName("DateCreated");
            Property(n => n.DateLastModified).HasColumnName("DateLastModified");
            Property(n => n.NewsView).HasColumnName("NewsView");
        }
    }
}