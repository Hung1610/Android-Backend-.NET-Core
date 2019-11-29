using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using RestaurantAPI.Helpers;

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

            modelBuilder.Entity<Foods>(entity =>
            {
                entity.Property(e => e.Id);

                entity.Property(e => e.Image).HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");
            });
            // Data Seeding
            modelBuilder.Entity<Foods>().HasData(
                new Foods() { Id = 1, Name = "Canh cá", Price = 7000, Image = "image/canhca.jpg" },
                new Foods() { Id = 2, Name = "Cơm trắng", Price = 5000, Image = "image/comtrang.jpg" },
                new Foods() { Id = 3, Name = "Cơm chiên", Price = 10000, Image = "image/comchien.jpg" },
                new Foods() { Id = 4, Name = "Canh thịt bò", Price = 15000, Image = "image/canhthitbo.jpg" }
            );
        }
    }
}
