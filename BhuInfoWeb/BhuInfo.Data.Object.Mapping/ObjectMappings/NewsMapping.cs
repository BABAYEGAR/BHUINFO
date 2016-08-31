using System.Data.Entity.ModelConfiguration;
using BhuInfo.Data.Objects.Entities;

namespace BhuInfo.Data.Object.Mapping.ObjectMappings
{
    public class NewsMapping : EntityTypeConfiguration<News>
        
    {
        public NewsMapping()
        {
            //this property is a primary key
            this.HasKey(n => n.NewsId);

            //this are the remaining properties
            this.Property(n => n.Title)
                .IsRequired();
            this.Property(n => n.Content)
               .IsRequired();

            //mapping the object entities to their respective columns
            this.ToTable("News");
            this.Property(n => n.NewsId).HasColumnName("NewsId");
            this.Property(n => n.Title).HasColumnName("Title");
            this.Property(n => n.Content).HasColumnName("Content");
            this.Property(n => n.Image).HasColumnName("Image");
            this.Property(n => n.CreatedById).HasColumnName("CreatedById");
            this.Property(n => n.NewsCategoryId).HasColumnName("NewsCategoryId");
            this.Property(n => n.LastModifiedById).HasColumnName("LastModifiedById");
            this.Property(n => n.DateCreated).HasColumnName("DateCreated");
            this.Property(n => n.DateLastModified).HasColumnName("DateLastModified");
            this.Property(n => n.NewsView).HasColumnName("NewsView");


            
        }
    }
}
