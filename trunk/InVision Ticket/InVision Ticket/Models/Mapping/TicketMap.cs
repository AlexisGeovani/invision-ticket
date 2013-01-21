using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace InVision_Ticket.Models.Mapping
{
    public class TicketMap : EntityTypeConfiguration<Ticket>
    {
        public TicketMap()
        {
            // Primary Key
            this.HasKey(t => t.TicketID);

            // Properties
            this.Property(t => t.Summary)
                .IsRequired()
                .HasMaxLength(255);

            this.Property(t => t.Details)
                .IsRequired()
                .HasMaxLength(4000);

            this.Property(t => t.TicketType)
                .IsRequired()
                .HasMaxLength(255);

            this.Property(t => t.Status)
                .IsRequired()
                .HasMaxLength(255);

            this.Property(t => t.ResolvedDateTime)
                .HasMaxLength(255);

            // Table & Column Mappings
            this.ToTable("Ticket");
            this.Property(t => t.TicketID).HasColumnName("TicketID");
            this.Property(t => t.Summary).HasColumnName("Summary");
            this.Property(t => t.Details).HasColumnName("Details");
            this.Property(t => t.CreatedDateTime).HasColumnName("CreatedDateTime");
            this.Property(t => t.SalesmenID).HasColumnName("SalesmenID");
            this.Property(t => t.TechnicianID).HasColumnName("TechnicianID");
            this.Property(t => t.Priority).HasColumnName("Priority");
            this.Property(t => t.CustomerID).HasColumnName("CustomerID");
            this.Property(t => t.TicketType).HasColumnName("TicketType");
            this.Property(t => t.LastModifiedDateTime).HasColumnName("LastModifiedDateTime");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.ResolvedDateTime).HasColumnName("ResolvedDateTime");
            this.Property(t => t.LastModified).HasColumnName("LastModified");
            this.Property(t => t.CurrentlyEditByLogin).HasColumnName("CurrentlyEditByLogin");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.LocationID).HasColumnName("LocationID");

            // Relationships
            this.HasRequired(t => t.Customer)
                .WithMany(t => t.Tickets)
                .HasForeignKey(d => d.CreatedBy);
            this.HasOptional(t => t.Customer1)
                .WithMany(t => t.Tickets1)
                .HasForeignKey(d => d.CustomerID);
            this.HasRequired(t => t.Location)
                .WithMany(t => t.Tickets)
                .HasForeignKey(d => d.LocationID);
            this.HasRequired(t => t.Login)
                .WithMany(t => t.Tickets)
                .HasForeignKey(d => d.CreatedBy);
            this.HasOptional(t => t.Login1)
                .WithMany(t => t.Tickets1)
                .HasForeignKey(d => d.SalesmenID);
            this.HasOptional(t => t.Login2)
                .WithMany(t => t.Tickets2)
                .HasForeignKey(d => d.TechnicianID);

        }
    }
}
