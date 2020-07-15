using AdessoRideShare.Db.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.InMemory.ValueGeneration.Internal;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace AdessoRideShare.Db.Context
{
    public class AdessoRideShareContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<UserTravelPlan> UserTravelPlans { get; set; }
        public DbSet<TravelPlanMembership> TravelPlanMemberships { get; set; }
        public DbSet<TravelPlanMembershipRequest> TravelPlanMembershipRequests { get; set; }

        public AdessoRideShareContext(DbContextOptions<AdessoRideShareContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>().HasQueryFilter(p => p.Deleted == 0);
            builder.Entity<City>().HasQueryFilter(p => p.Deleted == 0);
            builder.Entity<UserTravelPlan>().HasQueryFilter(p => p.Deleted == 0);
            //builder.Entity<UserTravelPlan>().HasMany(x => x.TravelPlanMemberships).WithOne(s => s.UserTravelPlan);


            builder.Entity<TravelPlanMembership>().HasQueryFilter(p => p.Deleted == 0);
            //builder.Entity<TravelPlanMembership>().HasOne(x => x.UserTravelPlan).WithMany(s => s.TravelPlanMemberships).HasForeignKey(s => s.TravelPlanId);
            
            builder.Entity<TravelPlanMembershipRequest>().HasQueryFilter(p => p.Deleted == 0);

        }
    }
}
