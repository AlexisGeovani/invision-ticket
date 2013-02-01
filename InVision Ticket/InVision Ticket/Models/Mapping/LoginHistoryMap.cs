using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace InVision_Ticket.Models.Mapping
{
    public class LoginHistoryMap : EntityTypeConfiguration<LoginHistory>
    {
        public LoginHistoryMap()
        {
            // Primary Key
            this.HasKey(t => t.LoginHistoryID);

            // Properties
            this.Property(t => t.IPAddress)
                .HasMaxLength(50);

            this.Property(t => t.Browser)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("LoginHistory");
            this.Property(t => t.LoginHistoryID).HasColumnName("LoginHistoryID");
            this.Property(t => t.LoginID).HasColumnName("LoginID");
            this.Property(t => t.DateTime).HasColumnName("DateTime");
            this.Property(t => t.IPAddress).HasColumnName("IPAddress");
            this.Property(t => t.Browser).HasColumnName("Browser");

            // Relationships
            this.HasRequired(t => t.Login)
                .WithMany(t => t.LoginHistories)
                .HasForeignKey(d => d.LoginID);

        }
    }
}
