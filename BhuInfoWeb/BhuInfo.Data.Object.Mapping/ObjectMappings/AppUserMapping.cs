using System.Data.Entity.ModelConfiguration;
using BhuInfo.Data.Objects.Entities;

namespace BhuInfo.Data.Object.Mapping.ObjectMappings
{
   public class AppUserMapping : EntityTypeConfiguration<AppUser>
    {
        public AppUserMapping()
        {
            //this property is a primary key
            this.HasKey(n => n.AppUserId);

            //this are the remaining properties
            this.Property(n => n.Firstname)
                .IsRequired();
            this.Property(n => n.Lastname)
               .IsRequired();
            this.Property(n => n.Email)
            .IsRequired();
            this.Property(n => n.Password)
            .IsRequired();
            this.Property(n => n.Mobile)
            .IsRequired();
            this.Property(n => n.Role)
           .IsRequired();

            //mapping the object entities to their respective columns
            this.ToTable("AppUser");
            this.Property(n => n.AppUserId).HasColumnName("AppUserId");
            this.Property(n => n.Firstname).HasColumnName("Firstname");
            this.Property(n => n.Lastname).HasColumnName("Lastname");
            this.Property(n => n.Email).HasColumnName("Email");
            this.Property(n => n.Email).HasColumnName("Mobile");
            this.Property(n => n.Password).HasColumnName("Password");
            this.Property(n => n.Role).HasColumnName("Role");
            this.Property(n => n.CreatedById).HasColumnName("CreatedById");
            this.Property(n => n.LastModifiedById).HasColumnName("LastModifiedById");
            this.Property(n => n.DateCreated).HasColumnName("DateCreated");
            this.Property(n => n.DateLastModified).HasColumnName("DateLastModified");
        }
    }
}
