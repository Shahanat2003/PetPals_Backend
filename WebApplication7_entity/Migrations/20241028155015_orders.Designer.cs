﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebApplication7_petPals;

#nullable disable

namespace WebApplication7_petPals.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20241028155015_orders")]
    partial class orders
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("WebApplication7_petPals.Models.Cart", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("User_id")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("User_id")
                        .IsUnique();

                    b.ToTable("carts");
                });

            modelBuilder.Entity("WebApplication7_petPals.Models.CartItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Cart_id")
                        .HasColumnType("int");

                    b.Property<int>("Product_id")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Cart_id");

                    b.HasIndex("Product_id");

                    b.ToTable("cartItems");
                });

            modelBuilder.Entity("WebApplication7_petPals.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("catogories");
                });

            modelBuilder.Entity("WebApplication7_petPals.Models.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Customer_Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Customer_address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Customer_city")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Customer_email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Customer_phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Order_date")
                        .HasColumnType("datetime2");

                    b.Property<int>("Order_id")
                        .HasColumnType("int");

                    b.Property<string>("Order_string")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Transaction_id")
                        .HasColumnType("int");

                    b.Property<int>("User_id")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("User_id");

                    b.ToTable("orders");
                });

            modelBuilder.Entity("WebApplication7_petPals.Models.OrderItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Order_id")
                        .HasColumnType("int");

                    b.Property<int>("Product_id")
                        .HasColumnType("int");

                    b.Property<decimal>("Total_Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("quantity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Order_id");

                    b.HasIndex("Product_id");

                    b.ToTable("orderItems");
                });

            modelBuilder.Entity("WebApplication7_petPals.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Img")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NewPrice")
                        .HasColumnType("int");

                    b.Property<int>("OldPrice")
                        .HasColumnType("int");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("products");
                });

            modelBuilder.Entity("WebApplication7_petPals.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(max)")
                        .HasDefaultValue("User");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("WebApplication7_petPals.Models.Cart", b =>
                {
                    b.HasOne("WebApplication7_petPals.Models.User", "User")
                        .WithOne("Cart")
                        .HasForeignKey("WebApplication7_petPals.Models.Cart", "User_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("WebApplication7_petPals.Models.CartItem", b =>
                {
                    b.HasOne("WebApplication7_petPals.Models.Cart", "Cart")
                        .WithMany("CartItems")
                        .HasForeignKey("Cart_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebApplication7_petPals.Models.Product", "Product")
                        .WithMany("CartItems")
                        .HasForeignKey("Product_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cart");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("WebApplication7_petPals.Models.Order", b =>
                {
                    b.HasOne("WebApplication7_petPals.Models.User", "User")
                        .WithMany("Orders")
                        .HasForeignKey("User_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("WebApplication7_petPals.Models.OrderItem", b =>
                {
                    b.HasOne("WebApplication7_petPals.Models.Order", "Order")
                        .WithMany("OrderItems")
                        .HasForeignKey("Order_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebApplication7_petPals.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("Product_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("WebApplication7_petPals.Models.Product", b =>
                {
                    b.HasOne("WebApplication7_petPals.Models.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("WebApplication7_petPals.Models.Cart", b =>
                {
                    b.Navigation("CartItems");
                });

            modelBuilder.Entity("WebApplication7_petPals.Models.Category", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("WebApplication7_petPals.Models.Order", b =>
                {
                    b.Navigation("OrderItems");
                });

            modelBuilder.Entity("WebApplication7_petPals.Models.Product", b =>
                {
                    b.Navigation("CartItems");
                });

            modelBuilder.Entity("WebApplication7_petPals.Models.User", b =>
                {
                    b.Navigation("Cart")
                        .IsRequired();

                    b.Navigation("Orders");
                });
#pragma warning restore 612, 618
        }
    }
}