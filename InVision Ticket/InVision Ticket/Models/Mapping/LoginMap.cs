using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace InVision_Ticket.Models.Mapping
{
    public class LoginMap : EntityTypeConfiguration<Login>
    {
        public LoginMap()
        {
            // Primary Key
            this.HasKey(t => t.LoginID);

            // Properties
            this.Property(t => t.Email)
                .IsRequired()
                .HasMaxLength(255);

            this.Property(t => t.Password)
                .IsRequired();

            this.Property(t => t.DisplayName)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("Login");
            this.Property(t => t.Email).HasColumnName("Email");
            this.Property(t => t.Password).HasColumnName("Password");
            this.Property(t => t.DisplayName).HasColumnName("DisplayName");
            this.Property(t => t.UserTypeID).HasColumnName("UserTypeID");
            this.Property(t => t.LocationID).HasColumnName("LocationID");
            this.Property(t => t.LoginID).HasColumnName("LoginID");

            // Relationships
            this.HasOptional(t => t.Location)
                .WithMany(t => t.Logins)
                .HasForeignKey(d => d.LocationID);
            this.HasRequired(t => t.UserType)
                .WithMany(t => t.Logins)
                .HasForeignKey(d => d.UserTypeID);

        }
    }
}
