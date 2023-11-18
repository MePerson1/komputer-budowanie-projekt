using KomputerBudowanieAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace KomputerBudowanieAPI.Database
{
    public class KomBuildDbContext : DbContext
    {
        public KomBuildDbContext(DbContextOptions<KomBuildDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<PcConfiguration> PcConfigurations { get; set; }
        public DbSet<GraphicCard> GraphicCards { get; set; }
        public DbSet<Cpu> Cpus { get; set; }
        public DbSet<CpuCooling> CpuCoolings { get; set; }
        public DbSet<Fan> Fans { get; set; }
        public DbSet<WaterCooling> WaterCoolings { get; set; }
        public DbSet<Storage> Storages { get; set; }
        public DbSet<Ram> Rams { get; set; }
        public DbSet<Motherboard> Motherboards { get; set; }
        public DbSet<PowerSupply> PowerSupplies { get; set; }
        public DbSet<Case> Cases { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PcConfiguration>()
                .HasOne(pc => pc.User)           // Each PcConfiguration has one User
                .WithMany(u => u.UserConfigurations)  // User has many PcConfigurations
                .HasForeignKey(pc => pc.UserId) // Foreign key property in PcConfiguration
                .IsRequired(false);             // Optional relationship (user might not be assigned)

            modelBuilder.Entity<User>()
                .HasMany(u => u.FavouriteConfigurations)
                .WithMany(pc => pc.UsersFavorited)
                .UsingEntity(j => j.ToTable("UserFavoriteConfigurations"));
        }

    }
}
