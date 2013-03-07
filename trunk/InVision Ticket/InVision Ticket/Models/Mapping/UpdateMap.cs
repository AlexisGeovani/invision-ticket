using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace InVision_Ticket.Models.Mapping
{
    public class UpdateMap : EntityTypeConfiguration<Update>
    {
        public UpdateMap()
        {
            // Primary Key
            this.HasKey(t => t.UpdateID);

            // Properties
            this.Property(t => t.Comment)
                .IsRequired()
                .HasMaxLength(4000);

            // Table & Column Mappings
            this.ToTable("Update");
            this.Property(t => t.UpdateID).HasColumnName("UpdateID");
            this.Property(t => t.TicketID).HasColumnName("TicketID");
            this.Property(t => t.Comment).HasColumnName("Comment");
            this.Property(t => t.CommentMarkDown).HasColumnName("CommentMarkDown");
            this.Property(t => t.BilledMinutes).HasColumnName("BilledMinutes");
            this.Property(t => t.ActualMinutes).HasColumnName("ActualMinutes");
            this.Property(t => t.ActivityDateTime).HasColumnName("ActivityDateTime");
            this.Property(t => t.Urgent).HasColumnName("Urgent");
            this.Property(t => t.LoginID).HasColumnName("LoginID");

            // Relationships
            this.HasRequired(t => t.Login)
                .WithMany(t => t.Updates)
                .HasForeignKey(d => d.LoginID);
            this.HasRequired(t => t.Ticket)
                .WithMany(t => t.Updates)
                .HasForeignKey(d => d.TicketID);

        }
    }
}
