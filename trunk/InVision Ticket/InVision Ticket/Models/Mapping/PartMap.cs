using System.ComponentModel.DataAnnotations.Schema;
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
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(255);

            // Table & Column Mappings
            this.ToTable("Part");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.PartID).HasColumnName("PartID");
            this.Property(t => t.Price).HasColumnName("Price");
        }
    }
}
