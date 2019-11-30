using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using RestaurantAPI.Helpers;
using RestaurantAPI.Models;

namespace RestaurantAPI.Models
{
    public partial class RestaurantAppContext : DbContext
    {
        //private readonly string _connectionString;
        public RestaurantAppContext()
        {
        }

        public RestaurantAppContext(DbContextOptions<RestaurantAppContext> options)
            : base(options)
        {
        }
        //public RestaurantAppContext(IOptions<DbConnectionInfo> dbConnectionInfo)
        //{
        //    _connectionString = dbConnectionInfo.Value.RestaurantDatabase;
        //}

        public virtual DbSet<Foods> Foods { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //if (!optionsBuilder.IsConfigured)
            //{
            ////#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
            //    optionsBuilder.UseSqlServer(_connectionString);
            //}
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<Orders>()
                .HasKey(c => new { c.BillId, c.FoodId });

            modelBuilder.Entity<Foods>(entity =>
            {
                entity.Property(e => e.Id);

                entity.Property(e => e.Image).HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");
            });
        }

        public DbSet<RestaurantAPI.Models.Bills> Bills { get; set; }

        public DbSet<RestaurantAPI.Models.Orders> Orders { get; set; }

        public DbSet<RestaurantAPI.Models.Tables> Tables { get; set; }

        public DbSet<RestaurantAPI.Models.Users> Users { get; set; }
    }
}
