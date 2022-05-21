using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using white_cloud.identity.Entities;

namespace white_cloud.identity
{
    public class UserDbContext : IdentityDbContext<User>
    {
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<User>(user => user.HasIndex(x => x.TherapistId).IsUnique(true));
            builder.Entity<Therapist>(t =>
            {
                t.ToTable("Therapists");
                t.HasKey(x => x.Id);
                t.HasOne<User>().WithOne().HasForeignKey(typeof(User), nameof(User.TherapistId));
            });
        }
    }
}
