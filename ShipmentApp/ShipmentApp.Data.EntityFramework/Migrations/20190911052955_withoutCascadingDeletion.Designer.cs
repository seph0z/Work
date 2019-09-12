﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ShipmentApp.Data.EntityFramework;

namespace ShipmentApp.Data.EntityFramework.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20190911052955_withoutCascadingDeletion")]
    partial class withoutCascadingDeletion
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ShipmentApp.Data.Contracts.Entities.ActivityLog", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Action");

                    b.Property<int>("CarrierId");

                    b.Property<DateTime>("DateTime");

                    b.Property<Guid>("ShipmentId");

                    b.HasKey("Id");

                    b.HasIndex("CarrierId");

                    b.HasIndex("ShipmentId");

                    b.ToTable("ActivityLogs");
                });

            modelBuilder.Entity("ShipmentApp.Data.Contracts.Entities.Address", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("City")
                        .HasMaxLength(128);

                    b.Property<string>("CountryCode")
                        .HasMaxLength(2);

                    b.Property<string>("Line1")
                        .HasMaxLength(128);

                    b.Property<string>("Line2")
                        .HasMaxLength(128);

                    b.Property<string>("PostCode")
                        .HasMaxLength(128);

                    b.Property<string>("State")
                        .HasMaxLength(128);

                    b.HasKey("Id");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("ShipmentApp.Data.Contracts.Entities.Carrier", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasMaxLength(128);

                    b.HasKey("Id");

                    b.ToTable("Carriers");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Carrier1"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Carrier2"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Carrier3"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Carrier4"
                        });
                });

            modelBuilder.Entity("ShipmentApp.Data.Contracts.Entities.Shipment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CarrierId");

                    b.Property<string>("Description")
                        .HasMaxLength(256);

                    b.Property<Guid>("RecipientAddressId");

                    b.Property<string>("RecipientName")
                        .HasMaxLength(256);

                    b.Property<Guid>("SenderAddressId");

                    b.Property<string>("SenderName")
                        .HasMaxLength(256);

                    b.Property<double>("Weight");

                    b.HasKey("Id");

                    b.HasIndex("CarrierId");

                    b.HasIndex("RecipientAddressId");

                    b.HasIndex("SenderAddressId");

                    b.ToTable("Shipments");
                });

            modelBuilder.Entity("ShipmentApp.Data.Contracts.Entities.ActivityLog", b =>
                {
                    b.HasOne("ShipmentApp.Data.Contracts.Entities.Carrier", "Carrier")
                        .WithMany("ActivityLogs")
                        .HasForeignKey("CarrierId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ShipmentApp.Data.Contracts.Entities.Shipment", "Shipment")
                        .WithMany("ActivityLogs")
                        .HasForeignKey("ShipmentId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("ShipmentApp.Data.Contracts.Entities.Shipment", b =>
                {
                    b.HasOne("ShipmentApp.Data.Contracts.Entities.Carrier", "Carrier")
                        .WithMany("Shipments")
                        .HasForeignKey("CarrierId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("ShipmentApp.Data.Contracts.Entities.Address", "RecipientAddress")
                        .WithMany()
                        .HasForeignKey("RecipientAddressId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("ShipmentApp.Data.Contracts.Entities.Address", "SenderAddress")
                        .WithMany()
                        .HasForeignKey("SenderAddressId")
                        .OnDelete(DeleteBehavior.Restrict);
                });
#pragma warning restore 612, 618
        }
    }
}
