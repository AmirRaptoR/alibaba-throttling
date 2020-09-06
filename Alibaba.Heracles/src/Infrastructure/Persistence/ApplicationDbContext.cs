using Alibaba.Heracles.Application.Common.Interfaces;
using Alibaba.Heracles.Domain.Entities;
using Alibaba.Heracles.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Alibaba.Heracles.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public DbSet<ThrottlingEntity> Throttlings { get; set; }

        public ApplicationDbContext(
            DbContextOptions options,
            IDateTime dateTime) : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ThrottlingEntity>(builder =>
            {
                builder.OwnsOne(x => x.Limit, navigationBuilder =>
                {
                    navigationBuilder.ToTable(null);
                    navigationBuilder.Property(x => x.Unit)
                        .HasConversion(new EnumToStringConverter<LimitUnit>())
                        .IsRequired();

                    navigationBuilder.Property(x => x.Value)
                        .IsRequired();
                });
                builder.HasKey(x => x.Id);
                builder.Property(x => x.Pattern)
                    .IsRequired();

                builder.HasIndex(x => x.Pattern)
                    .IsUnique();
            });
        }
    }
}