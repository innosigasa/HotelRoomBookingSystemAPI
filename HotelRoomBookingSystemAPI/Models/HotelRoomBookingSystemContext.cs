using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace HotelRoomBookingSystemAPI.Models
{
    public partial class HotelRoomBookingSystemContext : DbContext
    {
        public HotelRoomBookingSystemContext()
        {
        }

        public HotelRoomBookingSystemContext(DbContextOptions<HotelRoomBookingSystemContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Booking> Bookings { get; set; }
        public virtual DbSet<Guest> Guests { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }
        public virtual DbSet<Room> Rooms { get; set; }
        public virtual DbSet<RoomsType> RoomsTypes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=localhost;Database=HotelRoomBookingSystem;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Booking>(entity =>
            {
                entity.HasKey(e => e.BookingsId)
                    .HasName("PK__Bookings__05A7B636E843925D");

                entity.Property(e => e.BookingsId).HasColumnName("BookingsID");

                entity.Property(e => e.GuestId).HasColumnName("GuestID");

                entity.Property(e => e.RoomsId).HasColumnName("RoomsID");

                entity.HasOne(d => d.Guest)
                    .WithMany(p => p.Bookings)
                    .HasForeignKey(d => d.GuestId)
                    .HasConstraintName("FK__Bookings__GuestI__2B3F6F97");

                entity.HasOne(d => d.Rooms)
                    .WithMany(p => p.Bookings)
                    .HasForeignKey(d => d.RoomsId)
                    .HasConstraintName("FK__Bookings__RoomsI__2C3393D0");
            });

            modelBuilder.Entity<Guest>(entity =>
            {
                entity.Property(e => e.GuestId).HasColumnName("GuestID");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(30);
            });

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.HasKey(e => e.PaymentsId)
                    .HasName("PK__Payments__FD75746AF3860F61");

                entity.Property(e => e.PaymentsId).HasColumnName("PaymentsID");

                entity.Property(e => e.AmountsPaid).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.BookingsId).HasColumnName("BookingsID");

                entity.HasOne(d => d.Bookings)
                    .WithMany(p => p.Payments)
                    .HasForeignKey(d => d.BookingsId)
                    .HasConstraintName("FK__Payments__Bookin__2F10007B");
            });

            modelBuilder.Entity<Room>(entity =>
            {
                entity.HasKey(e => e.RoomsId)
                    .HasName("PK__Rooms__1AC62DAC3250B2C8");

                entity.Property(e => e.RoomsId).HasColumnName("RoomsID");

                entity.Property(e => e.RoomsTypeId).HasColumnName("RoomsTypeID");

                entity.HasOne(d => d.RoomsType)
                    .WithMany(p => p.Rooms)
                    .HasForeignKey(d => d.RoomsTypeId)
                    .HasConstraintName("FK__Rooms__RoomsType__286302EC");
            });

            modelBuilder.Entity<RoomsType>(entity =>
            {
                entity.Property(e => e.RoomsTypeId).HasColumnName("RoomsTypeID");

                entity.Property(e => e.Description).IsRequired();

                entity.Property(e => e.RoomsRate).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.RoomsType1)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasColumnName("RoomsType");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
