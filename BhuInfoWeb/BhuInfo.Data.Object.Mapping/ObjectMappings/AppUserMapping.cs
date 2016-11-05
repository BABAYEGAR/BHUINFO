using System.Data.Entity.ModelConfiguration;
using BhuInfo.Data.Objects.Entities;

namespace BhuInfo.Data.Object.Mapping.ObjectMappings
{
    public class AppUserMapping : EntityTypeConfiguration<AppUser>
    {
        public AppUserMapping()
        {
            //this property is a primary key
            HasKey(n => n.AppUserId);

            //this are the remaining properties
            Property(n => n.Firstname)
                .IsRequired();
            Property(n => n.Lastname)
                .IsRequired();
            Property(n => n.Email)
                .IsRequired();
            Property(n => n.Mobile)
                .IsRequired();
            Property(n => n.Role)
                .IsRequired(); 

            //mapping the object entities to their respective columns
            ToTable("AppUsers");
            Property(n => n.AppUserId).HasColumnName("AppUserId");
            Property(n => n.Firstname).HasColumnName("Firstname");
            Property(n => n.Lastname).HasColumnName("Lastname");
            Property(n => n.Email).HasColumnName("Email");
            Property(n => n.RememberMe).HasColumnName("RememberMe");
            Property(n => n.Mobile).HasColumnName("Mobile");
            Property(n => n.MatricNumber).HasColumnName("MatricNumber");
            Property(n => n.Password).HasColumnName("Password");
            Property(n => n.Role).HasColumnName("Role");
            Property(n => n.CreatedById).HasColumnName("CreatedById");
            Property(n => n.LastModifiedById).HasColumnName("LastModifiedById");
            Property(n => n.DateCreated).HasColumnName("DateCreated");
            Property(n => n.DateLastModified).HasColumnName("DateLastModified");
            Property(n => n.AppUserImage).HasColumnName("AppUserImage");
        }
    }
}