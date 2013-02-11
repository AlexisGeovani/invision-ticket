using System.ComponentModel.DataAnnotations;
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

            // Table & Column Mappings
            this.ToTable("Ticket");
            this.Property(t => t.TicketID).HasColumnName("TicketID");
            this.Property(t => t.Summary).HasColumnName("Summary");
            this.Property(t => t.Details).HasColumnName("Details");
            this.Property(t => t.CreatedDateTime).HasColumnName("CreatedDateTime");
            this.Property(t => t.SalesmenLoginID).HasColumnName("SalesmenLoginID");
            this.Property(t => t.TechnicianLoginID).HasColumnName("TechnicianLoginID");
            this.Property(t => t.Priority).HasColumnName("Priority");
            this.Property(t => t.CustomerID).HasColumnName("CustomerID");
            this.Property(t => t.TicketTypeID).HasColumnName("TicketTypeID");
            this.Property(t => t.LastModifiedDateTime).HasColumnName("LastModifiedDateTime");
            this.Property(t => t.StatusID).HasColumnName("StatusID");
            this.Property(t => t.ResolvedDateTime).HasColumnName("ResolvedDateTime");
            this.Property(t => t.LastModifiedBy).HasColumnName("LastModifiedID");
            this.Property(t => t.CurrentlyEditByLoginID).HasColumnName("CurrentlyEditByLoginID");
            this.Property(t => t.CreatedByLoginID).HasColumnName("CreatedByLoginID");
            this.Property(t => t.CreatedByCustomerID).HasColumnName("CreatedByCustomerID");
            this.Property(t => t.LocationID).HasColumnName("LocationID");
            this.Property(t => t.SystemID).HasColumnName("SystemID");
            this.Property(t => t.BillRateID).HasColumnName("BillRateID");

            // Relationships
            this.HasOptional(t => t.BillRate)
                .WithMany(t => t.BillRateTickets)
                .HasForeignKey(d => d.BillRateID);
            this.HasOptional(t => t.CreatedByCustomer)
                .WithMany(t => t.CreatedByCustomerTickets)
                .HasForeignKey(d => d.CreatedByCustomerID);
            this.HasRequired(t => t.Customer)
                .WithMany(t => t.CustomerTickets)
                .HasForeignKey(d => d.CustomerID);
            this.HasOptional(t => t.Location)
                .WithMany(t => t.LocationTickets)
                .HasForeignKey(d => d.LocationID);
            this.HasOptional(t => t.Login)
                .WithMany(t => t.SalesmanTickets)
                .HasForeignKey(d => d.SalesmenLoginID);
            this.HasOptional(t => t.Technician)
                .WithMany(t => t.TechnicianTickets)
                .HasForeignKey(d => d.TechnicianLoginID);
            this.HasOptional(t => t.CreatedByLogin)
                .WithMany(t => t.CreatedTickets)
                .HasForeignKey(d => d.CreatedByLoginID);
            this.HasOptional(t => t.CurrentlyEditByLogin)
                .WithMany(t => t.EditingTickets)
                .HasForeignKey(d => d.CurrentlyEditByLoginID);
            this.HasOptional(t => t.System)
                .WithMany(t => t.SystemTickets)
                .HasForeignKey(d => d.SystemID);
            this.HasRequired(t => t.TicketStatus)
                .WithMany(t => t.StatusTickets)
                .HasForeignKey(d => d.StatusID);
            this.HasRequired(t => t.TicketType)
                .WithMany(t => t.TypeTickets)
                .HasForeignKey(d => d.TicketTypeID);

        }
    }
}
