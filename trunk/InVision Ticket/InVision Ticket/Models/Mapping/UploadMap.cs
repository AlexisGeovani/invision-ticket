using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations;

namespace InVision_Ticket.Models.Mapping
{
    public class UploadMap : EntityTypeConfiguration<Upload>
    {
        public UploadMap()
        { 
            //Primary Key
            this.HasKey(t => t.UploadID);

            //Properties
            this.Property(t => t.Description)
                .HasMaxLength(400);
            this.Property(t => t.FileName)
                .IsRequired();
            this.Property(t => t.Data)
                .IsRequired();


            //Table & col mappings
            this.ToTable("Upload");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.Data).HasColumnName("Data");
            this.Property(t => t.UploadID).HasColumnName("UploadID");
            this.Property(t => t.TicketID).HasColumnName("TicketID");
            this.Property(t => t.FileName).HasColumnName("FileName");

            //Relationships
            this.HasRequired(t => t.Ticket)
                .WithMany(t => t.Uploads)
                .HasForeignKey(t => t.TicketID);
        
        }
    }
}
