using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace InVision_Ticket.Models.Mapping
{
    public class PartMap : EntityTypeConfiguration<Part>
    {
        public PartMap()
        {
            // Primary Key
            this.HasKey(t => t.PartID);

            // Properties
            this.Property(t => t.Summary)
                .IsRequired()
                .HasMaxLength(500);

            // Table & Column Mappings
            this.ToTable("Part");
            this.Property(t => t.Summary).HasColumnName("Summary");
            this.Property(t => t.PartID).HasColumnName("PartID");
            this.Property(t => t.Cost).HasColumnName("Cost");
            this.Property(t => t.Charge).HasColumnName("Charge");
            this.Property(t => t.TicketID).HasColumnName("TicketID");

            // Relationships
            this.HasRequired(t => t.Ticket)
                .WithMany(t => t.Parts)
                .HasForeignKey(d => d.TicketID);

        }
    }
}
