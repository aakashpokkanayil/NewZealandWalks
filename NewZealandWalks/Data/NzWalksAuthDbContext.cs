using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace NewZealandWalks.Data
{
    public class NzWalksAuthDbContext : IdentityDbContext
    {
        public NzWalksAuthDbContext(DbContextOptions<NzWalksAuthDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var readerId = "355c861a-2b58-4ab6-98b9-66b6fe6daa42";
            var writerId = "5973c707-a417-4627-9bf8-a41c2fd269a6";

            var roles = new List<IdentityRole> 
            {
                new IdentityRole
                    {
                    Id = readerId,
                    ConcurrencyStamp=readerId,
                    Name = "reader",
                    NormalizedName = "reader".ToUpper(),
                    }
                    ,
                new IdentityRole
                    {
                    Id = writerId,
                    ConcurrencyStamp=writerId,
                    Name = "writer",
                    NormalizedName = "writer".ToUpper(),
                    }
            };

                builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}
