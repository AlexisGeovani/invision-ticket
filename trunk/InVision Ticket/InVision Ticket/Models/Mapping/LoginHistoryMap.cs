using System.ComponentModel.DataAnnotations.Schema;
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
            // Table & Column Mappings
            this.ToTable("LoginHistory");
            this.Property(t => t.LoginHistoryID).HasColumnName("LoginHistoryID");
            this.Property(t => t.LoginID).HasColumnName("LoginID");
            this.Property(t => t.DateTime).HasColumnName("DateTime");

            // Relationships
            this.HasRequired(t => t.Login)
                .WithMany(t => t.LoginHistories)
                .HasForeignKey(d => d.LoginID);

        }
    }
}
