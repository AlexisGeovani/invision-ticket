using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace InVision_Ticket.Models.Mapping
{
    public class UserTypeMap : EntityTypeConfiguration<UserType>
    {
        public UserTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.UserTypeID);

            // Properties
            this.Property(t => t.UserType1)
                .IsRequired()
                .HasMaxLength(255);

            this.Property(t => t.Description)
                .IsRequired()
                .HasMaxLength(4000);

            // Table & Column Mappings
            this.ToTable("UserType");
            this.Property(t => t.UserTypeID).HasColumnName("UserTypeID");
            this.Property(t => t.UserType1).HasColumnName("UserType");
            this.Property(t => t.Description).HasColumnName("Description");
        }
    }
}
