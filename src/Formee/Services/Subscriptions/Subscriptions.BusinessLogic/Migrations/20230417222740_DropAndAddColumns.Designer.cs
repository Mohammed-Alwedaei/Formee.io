﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Subscriptions.BusinessLogic.DbContexts;

#nullable disable

namespace Subscriptions.BusinessLogic.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230417222740_DropAndAddColumns")]
    partial class DropAndAddColumns
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Subscriptions.BusinessLogic.Models.CouponModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Discount")
                        .HasColumnType("int");

                    b.Property<DateTime>("ExpireDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Coupons");
                });

            modelBuilder.Entity("Subscriptions.BusinessLogic.Models.OrderDetailsModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CouponId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("SubscriptionId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CouponId");

                    b.HasIndex("SubscriptionId");

                    b.ToTable("OrderDetails");
                });

            modelBuilder.Entity("Subscriptions.BusinessLogic.Models.OrderHeaderModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("HasConsent")
                        .HasColumnType("bit");

                    b.Property<int>("OrderDetailsId")
                        .HasColumnType("int");

                    b.Property<int>("SubscribedUserId")
                        .HasColumnType("int");

                    b.Property<decimal>("TotalPrice")
                        .HasColumnType("money");

                    b.Property<decimal>("TotalPriceAfterDiscount")
                        .HasColumnType("money");

                    b.HasKey("Id");

                    b.HasIndex("OrderDetailsId");

                    b.HasIndex("SubscribedUserId");

                    b.ToTable("OrderHeaders");
                });

            modelBuilder.Entity("Subscriptions.BusinessLogic.Models.SubscriptionFeaturesModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DeletedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<int>("NumberOfContainers")
                        .HasColumnType("int");

                    b.Property<int>("NumberOfForms")
                        .HasColumnType("int");

                    b.Property<int>("NumberOfLinks")
                        .HasColumnType("int");

                    b.Property<int>("NumberOfSites")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("SubscriptionFeatures");
                });

            modelBuilder.Entity("Subscriptions.BusinessLogic.Models.SubscriptionsModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AdminId")
                        .HasColumnType("int");

                    b.Property<decimal>("AnnualPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("SubscriptionFeaturesId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SubscriptionFeaturesId");

                    b.ToTable("Subscriptions");
                });

            modelBuilder.Entity("Subscriptions.BusinessLogic.Models.UserSubscriptionModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("SubscriptionId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SubscriptionId");

                    b.HasIndex("UserId");

                    b.ToTable("UserSubscriptions");
                });

            modelBuilder.Entity("Subscriptions.BusinessLogic.Models.UsersModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("GlobalUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Subscriptions.BusinessLogic.Models.OrderDetailsModel", b =>
                {
                    b.HasOne("Subscriptions.BusinessLogic.Models.CouponModel", "Coupon")
                        .WithMany()
                        .HasForeignKey("CouponId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Subscriptions.BusinessLogic.Models.SubscriptionsModel", "Subscription")
                        .WithMany()
                        .HasForeignKey("SubscriptionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Coupon");

                    b.Navigation("Subscription");
                });

            modelBuilder.Entity("Subscriptions.BusinessLogic.Models.OrderHeaderModel", b =>
                {
                    b.HasOne("Subscriptions.BusinessLogic.Models.OrderDetailsModel", "OrderDetails")
                        .WithMany()
                        .HasForeignKey("OrderDetailsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Subscriptions.BusinessLogic.Models.UsersModel", "SubscribedUser")
                        .WithMany()
                        .HasForeignKey("SubscribedUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("OrderDetails");

                    b.Navigation("SubscribedUser");
                });

            modelBuilder.Entity("Subscriptions.BusinessLogic.Models.SubscriptionsModel", b =>
                {
                    b.HasOne("Subscriptions.BusinessLogic.Models.SubscriptionFeaturesModel", "SubscriptionFeatures")
                        .WithMany()
                        .HasForeignKey("SubscriptionFeaturesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SubscriptionFeatures");
                });

            modelBuilder.Entity("Subscriptions.BusinessLogic.Models.UserSubscriptionModel", b =>
                {
                    b.HasOne("Subscriptions.BusinessLogic.Models.SubscriptionsModel", "Subscription")
                        .WithMany()
                        .HasForeignKey("SubscriptionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Subscriptions.BusinessLogic.Models.UsersModel", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Subscription");

                    b.Navigation("User");
                });
#pragma warning restore 612, 618
        }
    }
}
