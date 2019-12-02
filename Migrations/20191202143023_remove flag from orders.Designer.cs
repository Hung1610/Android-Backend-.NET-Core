﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RestaurantAPI.Models;

namespace RestaurantAPI.Migrations
{
    [DbContext(typeof(RestaurantAppContext))]
    [Migration("20191202143023_remove flag from orders")]
    partial class removeflagfromorders
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("RestaurantAPI.Models.Bills", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("CreateTime");

                    b.Property<int?>("CreatorId");

                    b.Property<int?>("Flag");

                    b.Property<long>("TableId");

                    b.Property<long?>("TotalPayment");

                    b.HasKey("Id");

                    b.HasIndex("TableId");

                    b.ToTable("Bills");
                });

            modelBuilder.Entity("RestaurantAPI.Models.Foods", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Image")
                        .HasMaxLength(50);

                    b.Property<string>("Name")
                        .HasMaxLength(50);

                    b.Property<decimal?>("Price")
                        .HasColumnType("decimal(18, 0)");

                    b.HasKey("Id");

                    b.ToTable("Foods");
                });

            modelBuilder.Entity("RestaurantAPI.Models.Orders", b =>
                {
                    b.Property<long>("BillId");

                    b.Property<long>("FoodId");

                    b.Property<long?>("PaymentAmount");

                    b.Property<int?>("Quantity");

                    b.HasKey("BillId", "FoodId");

                    b.HasIndex("FoodId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("RestaurantAPI.Models.Tables", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("Flag");

                    b.HasKey("Id");

                    b.ToTable("Tables");
                });

            modelBuilder.Entity("RestaurantAPI.Models.Users", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Image");

                    b.Property<string>("Password");

                    b.Property<string>("Username");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("RestaurantAPI.Models.Bills", b =>
                {
                    b.HasOne("RestaurantAPI.Models.Tables", "Table")
                        .WithMany()
                        .HasForeignKey("TableId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("RestaurantAPI.Models.Orders", b =>
                {
                    b.HasOne("RestaurantAPI.Models.Bills", "Bill")
                        .WithMany()
                        .HasForeignKey("BillId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("RestaurantAPI.Models.Foods", "Food")
                        .WithMany()
                        .HasForeignKey("FoodId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
