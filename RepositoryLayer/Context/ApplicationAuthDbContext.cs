using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace RepositoryLayer.Context
{
    public class ApplicationAuthDbContext : IdentityDbContext
    {
        public ApplicationAuthDbContext(DbContextOptions<ApplicationAuthDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var memberId = "494bd4fc-b80b-4446-8a36-26ef8adcb4c6";
            var adminId = "76a1e527-ca4d-46f1-a5ab-d351780e7a17";

            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id = memberId,
                    Name = "Member",
                    NormalizedName = "Member".ToUpper(),
                    ConcurrencyStamp = memberId
                },

                new IdentityRole
                {
                    Id = adminId,
                    Name = "Admin",
                    NormalizedName = "Admin".ToUpper(),
                    ConcurrencyStamp = adminId
                }
            };

            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}
