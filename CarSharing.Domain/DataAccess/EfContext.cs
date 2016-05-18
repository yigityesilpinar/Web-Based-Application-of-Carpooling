using CarSharing.Domain.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSharing.Domain.DataAccess
{
    public class EfContext : DbContext
    {
        public DbSet<Car> Cars { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Seat> Seats { get; set; }
        public DbSet<Trip> Trips { get; set; }
        public DbSet<User> Users { get; set; }

        public EfContext() : base("name=CarSharingDb")
        {
            Configuration.ValidateOnSaveEnabled = false;
        }

        static EfContext()
        {
            //Database.SetInitializer(new DropCreateDatabaseAlways<EfContext>());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Feedback>()
                .HasRequired(f => f.Trip)
                .WithMany(t => t.Feedbacks)
                .HasForeignKey(f => f.TripId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Feedback>()
                .HasRequired(f => f.User)
                .WithMany(u => u.Feedbacks)
                .HasForeignKey(f => f.UserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Seat>()
                .HasRequired(f => f.Trip)
                .WithMany(t => t.Seats)
                .HasForeignKey(f => f.TripId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Seat>()
                .HasRequired(f => f.User)
                .WithMany(u => u.Seats)
                .HasForeignKey(f => f.UserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Trip>()
                .HasRequired(t => t.Car)
                .WithMany(c => c.Trips)
                .HasForeignKey(t => t.CarId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Trip>()
                .HasRequired(f => f.User)
                .WithMany(u => u.Trips)
                .HasForeignKey(f => f.UserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Trip>()
                .HasRequired(f => f.DepartLocation)
                .WithMany(u => u.DepartTrips)
                .HasForeignKey(f => f.DepartLocationId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Trip>()
                .HasRequired(f => f.ArrivalLocation)
                .WithMany(u => u.ArrivalTrips)
                .HasForeignKey(f => f.ArrivalLocationId)
                .WillCascadeOnDelete(false);
        }
    }
}
