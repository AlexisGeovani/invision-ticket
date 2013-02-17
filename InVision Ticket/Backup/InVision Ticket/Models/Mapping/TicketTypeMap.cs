using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace InVision_Ticket.Models.Mapping
{
    public class TicketTypeMap : EntityTypeConfiguration<TicketType>
    {
        public TicketTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.TicketTypeID);

            // Properties
            this.Property(t => t.TicketType1)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("TicketType");
            this.Property(t => t.TicketTypeID).HasColumnName("TicketTypeID");
            this.Property(t => t.TicketType1).HasColumnName("TicketType");
        }
    }
}
