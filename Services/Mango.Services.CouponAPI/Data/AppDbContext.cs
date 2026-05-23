using Microsoft.EntityFrameworkCore;
using Microservices.Services.CouponAPI.Models;

namespace Microservices.Services.CouponAPI.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        public DbSet<Coupon> coupons {get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Coupon>().HasData(
                new Coupon { CouponId = 1, CouponCode = "BLACKFRIDAY", DiscountAmount = 20, MinAmount = 100 },
                new Coupon { CouponId = 2, CouponCode = "NEWYEAR", DiscountAmount = 15, MinAmount = 50 }
            );
        }
    }

}