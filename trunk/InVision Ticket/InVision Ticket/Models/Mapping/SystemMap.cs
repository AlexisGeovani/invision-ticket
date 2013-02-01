using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace InVision_Ticket.Models.Mapping
{
    public class SystemMap : EntityTypeConfiguration<System>
    {
        public SystemMap()
        {
            // Primary Key
            this.HasKey(t => t.SystemID);

            // Properties
            this.Property(t => t.Type)
                .IsRequired()
                .HasMaxLength(25);

            this.Property(t => t.Desciption)
                .IsRequired()
                .HasMaxLength(4000);

            // Table & Column Mappings
            this.ToTable("System");
            this.Property(t => t.SystemID).HasColumnName("SystemID");
            this.Property(t => t.Type).HasColumnName("Type");
            this.Property(t => t.CustomerID).HasColumnName("CustomerID");
            this.Property(t => t.Desciption).HasColumnName("Desciption");

            // Relationships
            this.HasRequired(t => t.Customer)
                .WithMany(t => t.Systems)
                .HasForeignKey(d => d.CustomerID);

        }
    }
}
