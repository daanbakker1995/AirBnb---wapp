using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using AirBnb.Models;

namespace AirBnb.Data
{
    public partial class Airbnb2022Context : DbContext
    {
        public Airbnb2022Context()
        {
        }

        public Airbnb2022Context(DbContextOptions<Airbnb2022Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Listing> Listings { get; set; } = null!;
        public virtual DbSet<Neighbourhood> Neighbourhoods { get; set; } = null!;
        public virtual DbSet<Review> Reviews { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=AIRBNB2022");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Listing>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("listings");

                entity.Property(e => e.Availability365).HasColumnName("availability_365");

                entity.Property(e => e.CalculatedHostListingsCount).HasColumnName("calculated_host_listings_count");

                entity.Property(e => e.HostId).HasColumnName("host_id");

                entity.Property(e => e.HostName)
                    .IsUnicode(false)
                    .HasColumnName("host_name");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.LastReview)
                    .HasColumnType("date")
                    .HasColumnName("last_review");

                entity.Property(e => e.Latitude).HasColumnName("latitude");

                entity.Property(e => e.License)
                    .IsUnicode(false)
                    .HasColumnName("license");

                entity.Property(e => e.Longitude).HasColumnName("longitude");

                entity.Property(e => e.MinimumNights).HasColumnName("minimum_nights");

                entity.Property(e => e.Name)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.Neighbourhood)
                    .IsUnicode(false)
                    .HasColumnName("neighbourhood");

                entity.Property(e => e.NeighbourhoodGroup).HasColumnName("neighbourhood_group");

                entity.Property(e => e.NumberOfReviews).HasColumnName("number_of_reviews");

                entity.Property(e => e.NumberOfReviewsLtm).HasColumnName("number_of_reviews_ltm");

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.ReviewsPerMonth).HasColumnName("reviews_per_month");

                entity.Property(e => e.RoomType)
                    .IsUnicode(false)
                    .HasColumnName("room_type");
            });

            modelBuilder.Entity<Neighbourhood>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("neighbourhoods");

                entity.Property(e => e.Neighbourhood1)
                    .HasMaxLength(50)
                    .HasColumnName("neighbourhood");

                entity.Property(e => e.NeighbourhoodGroup)
                    .HasMaxLength(1)
                    .HasColumnName("neighbourhood_group");
            });

            modelBuilder.Entity<Review>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("reviews");

                entity.Property(e => e.Date)
                    .HasColumnType("date")
                    .HasColumnName("date");

                entity.Property(e => e.ListingId).HasColumnName("listing_id");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
