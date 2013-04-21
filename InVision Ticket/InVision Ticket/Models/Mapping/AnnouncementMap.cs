using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.ModelConfiguration;

namespace InVision_Ticket.Models.Mapping
{
    public class AnnouncementMap : EntityTypeConfiguration<Announcement>
    {
        public AnnouncementMap()
        {
            this.HasKey(a => a.AnnouncementID);
            this.Property(a => a.HTML).IsRequired();
            this.Property(a => a.CreatedDateTime).IsRequired();
            this.Property(a => a.CreatedByLoginID).IsRequired();
            this.Property(a => a.DirectHTML).IsRequired();

            this.ToTable("Announcement");

            this.Property(a => a.AnnouncementID).HasColumnName("AnnouncementID");
            this.Property(a => a.HTML).HasColumnName("HTML");
            this.Property(a => a.Markup).HasColumnName("Markup");
            this.Property(a => a.CreatedDateTime).HasColumnName("CreatedDateTime");
            this.Property(a => a.CreatedByLoginID).HasColumnName("CreatedByLoginID");
            this.Property(a => a.DirectHTML).HasColumnName("DirectHTML");
            this.Property(a => a.CSS).HasColumnName("CSS");

            this.HasRequired(l => l.Login)
                .WithMany(a => a.Announcements)
                .HasForeignKey(a => a.CreatedByLoginID);
        
        }
    }
}