using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using ShipmentApp.Data.Contracts.Entities;
using System.Collections.Generic;

namespace ShipmentApp.Data.EntityFramework
{
    public class AppDbContext : DbContext
    {
        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Shipment> Shipments { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Carrier> Carriers { get; set; }
        public DbSet<ActivityLog> ActivityLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Shipment>(entity =>
            {
                entity.Property(e => e.Description).HasMaxLength(256);
                entity.Property(e => e.RecipientName).HasMaxLength(256);
                entity.Property(e => e.SenderName).HasMaxLength(256);

                entity.HasOne(d => d.RecipientAddress)
                .WithMany().HasForeignKey(d => d.RecipientAddressId).OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.SenderAddress)
                .WithMany().HasForeignKey(d => d.SenderAddressId).OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.Carrier)
                .WithMany(p => p.Shipments)
                .HasForeignKey(d => d.CarrierId).OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(d => d.ActivityLogs)
                .WithOne(p => p.Shipment)
                .HasForeignKey(d => d.ShipmentId).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Address>(entity =>
            {
                entity.Property(d => d.PostCode).HasMaxLength(128);
                entity.Property(d => d.CountryCode).HasMaxLength(2);
                entity.Property(d => d.State).HasMaxLength(128);
                entity.Property(d => d.City).HasMaxLength(128);
                entity.Property(d => d.Line1).HasMaxLength(128);
                entity.Property(d => d.Line2).HasMaxLength(128);
            });

            modelBuilder.Entity<Carrier>(entity =>
            {
                entity.Property(d => d.Name).HasMaxLength(128);

                entity.HasMany(d => d.ActivityLogs)
                .WithOne(p => p.Carrier)
                .HasForeignKey(d => d.CarrierId);
            });

            modelBuilder.Entity<Carrier>().HasData(new List<Carrier>
            {
                new Carrier { Id  = 1, Name = "Carrier1"},
                new Carrier { Id  = 2, Name = "Carrier2"},
                new Carrier { Id  = 3, Name = "Carrier3"},
                new Carrier { Id  = 4, Name = "Carrier4"}
            });
        }
    }
}
