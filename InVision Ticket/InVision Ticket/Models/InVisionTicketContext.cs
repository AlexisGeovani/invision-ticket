using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using InVision_Ticket.Models.Mapping;

namespace InVision_Ticket.Models
{
    public class InVisionTicketContext : DbContext
    {
        static InVisionTicketContext()
        {
            Database.SetInitializer<InVisionTicketContext>(null);
        }

		public InVisionTicketContext()
			: base("Name=InVisionTicketContext")
		{
		}

        public DbSet<BillRate> BillRates { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerContact> CustomerContacts { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Login> Logins { get; set; }
        public DbSet<LoginHistory> LoginHistories { get; set; }
        public DbSet<Part> Parts { get; set; }
        public DbSet<System> Systems { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<TicketStatu> TicketStatus { get; set; }
        public DbSet<TicketType> TicketTypes { get; set; }
        public DbSet<Update> Updates { get; set; }
        public DbSet<UserType> UserTypes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new BillRateMap());
            modelBuilder.Configurations.Add(new CustomerMap());
            modelBuilder.Configurations.Add(new CustomerContactMap());
            modelBuilder.Configurations.Add(new LocationMap());
            modelBuilder.Configurations.Add(new LoginMap());
            modelBuilder.Configurations.Add(new LoginHistoryMap());
            modelBuilder.Configurations.Add(new PartMap());
            modelBuilder.Configurations.Add(new SystemMap());
            modelBuilder.Configurations.Add(new TicketMap());
            modelBuilder.Configurations.Add(new TicketStatusMap());
            modelBuilder.Configurations.Add(new TicketTypeMap());
            modelBuilder.Configurations.Add(new UpdateMap());
            modelBuilder.Configurations.Add(new UserTypeMap());
        }
    }
}
