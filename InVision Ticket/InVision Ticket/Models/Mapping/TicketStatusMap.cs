using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace InVision_Ticket.Models.Mapping
{
    public class TicketStatusMap : EntityTypeConfiguration<TicketStatus>
    {
        public TicketStatusMap()
        {
            // Primary Key
            this.HasKey(t => t.TicketStatusID);

            // Properties
            this.Property(t => t.Status)
                .IsRequired()
                .HasMaxLength(255);

            // Table & Column Mappings
            this.ToTable("TicketStatus");
            this.Property(t => t.TicketStatusID).HasColumnName("TicketStatusID");
            this.Property(t => t.Open).HasColumnName("Open");
            this.Property(t => t.AttentionRequired).HasColumnName("AttentionRequired");
            this.Property(t => t.Status).HasColumnName("TicketStatus");
        }
    }
}
