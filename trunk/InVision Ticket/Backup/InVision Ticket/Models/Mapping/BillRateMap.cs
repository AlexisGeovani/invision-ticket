using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace InVision_Ticket.Models.Mapping
{
    public class BillRateMap : EntityTypeConfiguration<BillRate>
    {
        public BillRateMap()
        {
            // Primary Key
            this.HasKey(t => t.BillRateID);

            // Properties
            this.Property(t => t.BillRateDescription)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("BillRate");
            this.Property(t => t.BillRateID).HasColumnName("BillRateID");
            this.Property(t => t.TicketBillRate).HasColumnName("BillRate");
            this.Property(t => t.BillRateDescription).HasColumnName("BillRateDescription");
        }
    }
}
