using System;
using System.Collections.Generic;
using System.Text;

using IT703_Assignment2.Areas.Identity.Data;
using IT703_Assignment2.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IT703_Assignment2.Data
{
    public class ApplicationDbContext : IdentityDbContext<AccountUser, AccountRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }


        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Agency> Agencies { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<CheckSheet> CheckSheets { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Guest> Guests { get; set; }
        public DbSet<HouseKeeper> HouseKeepers { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<RoomType> RoomTypes { get; set; }
        public DbSet<Schedule> Schedules { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }


        public DbSet<ParkingLot> ParkingLot { get; set; }


    }
}
