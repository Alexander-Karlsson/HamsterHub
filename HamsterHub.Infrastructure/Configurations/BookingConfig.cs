using HamsterHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HamsterHub.Infrastructure.Configurations;

public class BookingConfig : IEntityTypeConfiguration<Booking>
{
    public void Configure(EntityTypeBuilder<Booking> builder)
    {
        builder.HasKey(b => b.Id);

        builder.Property(b => b.CustomerName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(b => b.CustomerEmail)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(b => b.Purpose)
            .HasMaxLength(300);

        builder.HasOne(b => b.Hamster)
            .WithMany()
            .HasForeignKey(b => b.HamsterId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}