using Microsoft.EntityFrameworkCore;
using System.Data;

namespace StargateAPI.Data
{
    public class StargateContext : DbContext
    {
        public IDbConnection Connection => Database.GetDbConnection();
        public virtual DbSet<Person> People { get; set; }
        public virtual DbSet<AstronautDetail> AstronautDetails { get; set; }
        public virtual DbSet<AstronautDuty> AstronautDuties { get; set; }

        public StargateContext(DbContextOptions<StargateContext> options)
        : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(StargateContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
