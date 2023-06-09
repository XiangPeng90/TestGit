﻿// <auto-generated />
using System;
using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Entities.Migrations
{
    [DbContext(typeof(Context))]
    [Migration("20230503075639_foodcount")]
    partial class foodcount
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("FoodOrder", b =>
                {
                    b.Property<long>("Foodsid")
                        .HasColumnType("bigint");

                    b.Property<long>("ordersid")
                        .HasColumnType("bigint");

                    b.HasKey("Foodsid", "ordersid");

                    b.HasIndex("ordersid");

                    b.ToTable("FoodOrder");
                });

            modelBuilder.Entity("FoodTrolley", b =>
                {
                    b.Property<long>("foodsid")
                        .HasColumnType("bigint");

                    b.Property<long>("trolleysId")
                        .HasColumnType("bigint");

                    b.HasKey("foodsid", "trolleysId");

                    b.HasIndex("trolleysId");

                    b.ToTable("FoodTrolley");
                });

            modelBuilder.Entity("Model.Models.Food", b =>
                {
                    b.Property<long>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<int>("count")
                        .HasColumnType("int");

                    b.Property<string>("description")
                        .HasMaxLength(40)
                        .HasColumnType("varchar(40)");

                    b.Property<string>("f_name")
                        .IsRequired()
                        .HasMaxLength(8)
                        .HasColumnType("varchar(8)");

                    b.Property<string>("img")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<long>("menuId")
                        .HasColumnType("bigint");

                    b.Property<double>("price")
                        .HasMaxLength(5)
                        .HasColumnType("double");

                    b.HasKey("id");

                    b.HasIndex("menuId");

                    b.ToTable("T_Food", (string)null);
                });

            modelBuilder.Entity("Model.Models.Menu", b =>
                {
                    b.Property<long>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<long>("ShopId")
                        .HasColumnType("bigint");

                    b.HasKey("id");

                    b.HasIndex("ShopId")
                        .IsUnique();

                    b.ToTable("T_Menu", (string)null);
                });

            modelBuilder.Entity("Model.Models.Order", b =>
                {
                    b.Property<long>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<long>("ShopId")
                        .HasColumnType("bigint");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("ordertime")
                        .HasMaxLength(20)
                        .HasColumnType("datetime(6)");

                    b.Property<int>("status")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("ShopId");

                    b.HasIndex("UserId");

                    b.ToTable("T_Order", (string)null);
                });

            modelBuilder.Entity("Model.Models.Shop", b =>
                {
                    b.Property<long>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("address")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("phone_number")
                        .HasMaxLength(12)
                        .HasColumnType("varchar(12)");

                    b.Property<string>("picture")
                        .HasColumnType("longtext");

                    b.Property<string>("s_account")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("varchar(10)");

                    b.Property<string>("s_name")
                        .IsRequired()
                        .HasMaxLength(16)
                        .HasColumnType("varchar(16)");

                    b.Property<string>("s_password")
                        .IsRequired()
                        .HasMaxLength(16)
                        .HasColumnType("varchar(16)");

                    b.Property<DateTime?>("s_regist_time")
                        .IsRequired()
                        .HasColumnType("datetime(6)");

                    b.HasKey("id");

                    b.ToTable("T_Shop", (string)null);
                });

            modelBuilder.Entity("Model.Models.ShopInfo", b =>
                {
                    b.Property<long>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<int>("FavorableRate")
                        .HasColumnType("int");

                    b.Property<long>("ShopId")
                        .HasColumnType("bigint");

                    b.HasKey("id");

                    b.HasIndex("ShopId")
                        .IsUnique();

                    b.ToTable("t_ShopInfo", (string)null);
                });

            modelBuilder.Entity("Model.Models.Trolley", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<long>("userId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("userId")
                        .IsUnique();

                    b.ToTable("t_trolley", (string)null);
                });

            modelBuilder.Entity("Model.Models.User", b =>
                {
                    b.Property<long>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("avatar")
                        .HasColumnType("longtext");

                    b.Property<string>("phone_number")
                        .HasMaxLength(12)
                        .HasColumnType("varchar(12)");

                    b.Property<string>("u_account")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.Property<string>("u_name")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.Property<string>("u_password")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.Property<DateTime>("u_regist_time")
                        .HasColumnType("datetime(6)");

                    b.HasKey("id");

                    b.ToTable("T_User", (string)null);
                });

            modelBuilder.Entity("Model.Models.View", b =>
                {
                    b.Property<long>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<long>("OrderId")
                        .HasColumnType("bigint");

                    b.Property<string>("view")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("varchar(40)");

                    b.HasKey("id");

                    b.HasIndex("OrderId");

                    b.ToTable("T_View", (string)null);
                });

            modelBuilder.Entity("FoodOrder", b =>
                {
                    b.HasOne("Model.Models.Food", null)
                        .WithMany()
                        .HasForeignKey("Foodsid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Model.Models.Order", null)
                        .WithMany()
                        .HasForeignKey("ordersid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FoodTrolley", b =>
                {
                    b.HasOne("Model.Models.Food", null)
                        .WithMany()
                        .HasForeignKey("foodsid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Model.Models.Trolley", null)
                        .WithMany()
                        .HasForeignKey("trolleysId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Model.Models.Food", b =>
                {
                    b.HasOne("Model.Models.Menu", "menu")
                        .WithMany("foods")
                        .HasForeignKey("menuId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("menu");
                });

            modelBuilder.Entity("Model.Models.Menu", b =>
                {
                    b.HasOne("Model.Models.Shop", "shop")
                        .WithOne("menu")
                        .HasForeignKey("Model.Models.Menu", "ShopId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("shop");
                });

            modelBuilder.Entity("Model.Models.Order", b =>
                {
                    b.HasOne("Model.Models.Shop", "shop")
                        .WithMany("orders")
                        .HasForeignKey("ShopId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Model.Models.User", "user")
                        .WithMany("orders")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("shop");

                    b.Navigation("user");
                });

            modelBuilder.Entity("Model.Models.ShopInfo", b =>
                {
                    b.HasOne("Model.Models.Shop", "Shop")
                        .WithOne("ShopInfo")
                        .HasForeignKey("Model.Models.ShopInfo", "ShopId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Shop");
                });

            modelBuilder.Entity("Model.Models.Trolley", b =>
                {
                    b.HasOne("Model.Models.User", "user")
                        .WithOne("trolley")
                        .HasForeignKey("Model.Models.Trolley", "userId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("user");
                });

            modelBuilder.Entity("Model.Models.View", b =>
                {
                    b.HasOne("Model.Models.Order", "order")
                        .WithMany("views")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("order");
                });

            modelBuilder.Entity("Model.Models.Menu", b =>
                {
                    b.Navigation("foods");
                });

            modelBuilder.Entity("Model.Models.Order", b =>
                {
                    b.Navigation("views");
                });

            modelBuilder.Entity("Model.Models.Shop", b =>
                {
                    b.Navigation("ShopInfo")
                        .IsRequired();

                    b.Navigation("menu");

                    b.Navigation("orders");
                });

            modelBuilder.Entity("Model.Models.User", b =>
                {
                    b.Navigation("orders");

                    b.Navigation("trolley");
                });
#pragma warning restore 612, 618
        }
    }
}
