using HotelManagement.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelManagement.ViewModels;

namespace HotelManagement.Models
{
    public class AppDBContext:IdentityDbContext<ApplicationUser>
    {
        public AppDBContext(DbContextOptions<AppDBContext> options)
            :base(options)
        {

        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Clients> Clients { get; set; }

        public DbSet<Rooms> Rooms { get; set; }

        public DbSet<Reservation> Reservations { get; set; }

        public DbSet<Payment> Payments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            foreach (var foreignKey in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
            }

            modelBuilder.seed();
             
        }

        public DbSet<HotelManagement.Models.Clients> Clients_1 { get; set; }

        public DbSet<HotelManagement.ViewModels.RoomsViewModel> RoomsViewModel { get; set; }

        public DbSet<HotelManagement.Models.Reservation> Reservation { get; set; }
    }
}
