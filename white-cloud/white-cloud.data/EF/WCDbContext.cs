using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using white_cloud.entities;
using white_cloud.entities.Tests;

namespace white_cloud.data.EF
{
    public class WCDbContext : IdentityDbContext<User>
    {
        public WCDbContext(DbContextOptions<WCDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Therapist> Therapists { get; set; }
        public DbSet<ClientInvite> ClientInvites { get; set; }
        public DbSet<TestRequest> TestRequests { get; set; }
        public DbSet<TestSubmission> TestSubmissions { get; set; }
    }
}
