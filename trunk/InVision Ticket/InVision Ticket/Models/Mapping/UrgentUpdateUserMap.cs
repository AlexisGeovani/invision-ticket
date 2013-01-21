using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace InVision_Ticket.Models.Mapping
{
    public class UrgentUpdateUserMap : EntityTypeConfiguration<UrgentUpdateUser>
    {
        public UrgentUpdateUserMap()
        {
            // Primary Key
            this.HasKey(t => t.UrgentUpdateUserID);

            // Properties
            // Table & Column Mappings
            this.ToTable("UrgentUpdateUser");
            this.Property(t => t.UrgentUpdateUserID).HasColumnName("UrgentUpdateUserID");
            this.Property(t => t.LoginID).HasColumnName("LoginID");
            this.Property(t => t.UpdateID).HasColumnName("UpdateID");

            // Relationships
            this.HasRequired(t => t.Login)
                .WithMany(t => t.UrgentUpdateUsers)
                .HasForeignKey(d => d.LoginID);
            this.HasRequired(t => t.Update)
                .WithMany(t => t.UrgentUpdateUsers)
                .HasForeignKey(d => d.UpdateID);

        }
    }
}
