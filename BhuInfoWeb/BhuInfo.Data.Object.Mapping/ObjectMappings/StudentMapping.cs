using System.Data.Entity.ModelConfiguration;
using BhuInfo.Data.Objects.Entities;

namespace BhuInfo.Data.Object.Mapping.ObjectMappings
{
    public class StudentMapping : EntityTypeConfiguration<Student>
    {
        public StudentMapping()
        {
            //this property is a primary key
            HasKey(n => n.StudentId);

            //this are the remaining properties
            Property(n => n.Firstname)
                .IsRequired();
            Property(n => n.Lastname)
                .IsRequired();
            Property(n => n.Email)
                .IsRequired();
            Property(n => n.Password)
                .IsRequired();
            Property(n => n.Mobile)
                .IsRequired();
            Property(n => n.MatricNo)
                .IsRequired(); 

            //mapping the object entities to their respective columns
            ToTable("Students");
            Property(n => n.StudentId).HasColumnName("StudentId");
            Property(n => n.Firstname).HasColumnName("Firstname");
            Property(n => n.Lastname).HasColumnName("Lastname");
            Property(n => n.Email).HasColumnName("Email");
            Property(n => n.Mobile).HasColumnName("Mobile");
            Property(n => n.Password).HasColumnName("Password");
            Property(n => n.MatricNo).HasColumnName("MatricNo");
            Property(n => n.DateCreated).HasColumnName("DateCreated");

        }
    }
}