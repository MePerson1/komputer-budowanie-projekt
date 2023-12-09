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
        public DbSet<WaterCooling> WaterCoolings { get; set; }
        public DbSet<Storage> Storages { get; set; }
        public DbSet<Ram> Rams { get; set; }
        public DbSet<Motherboard> Motherboards { get; set; }
        public DbSet<PowerSupply> PowerSupplies { get; set; }
        public DbSet<Case> Cases { get; set; }
        public DbSet<PcConfigurationRam> PcConfigurationRam { get; set; }
        public DbSet<PcConfigurationStorage> PcConfigurationStorage { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PcConfigurationRam>()
                    .HasKey(pc => new { pc.PcConfigurationsId, pc.RamsId });
            modelBuilder.Entity<PcConfigurationRam>()
                    .HasOne(p => p.PcConfiguration)
                    .WithMany(pc => pc.PcConfigurationRams)
                    .HasForeignKey(p => p.PcConfigurationsId);
            modelBuilder.Entity<PcConfigurationRam>()
                    .HasOne(r => r.Ram)
                    .WithMany(pc => pc.PcConfigurationRams)
                    .HasForeignKey(c => c.RamsId);

            modelBuilder.Entity<PcConfigurationStorage>()
                    .HasKey(po => new { po.PcConfigurationsId, po.StoragesId });
            modelBuilder.Entity<PcConfigurationStorage>()
                    .HasOne(p => p.PcConfiguration)
                    .WithMany(pc => pc.PcConfigurationStorages)
                    .HasForeignKey(p => p.PcConfigurationsId);
            modelBuilder.Entity<PcConfigurationStorage>()
                    .HasOne(p => p.Storage)
                    .WithMany(pc => pc.PcConfigurationStorages)
                    .HasForeignKey(c => c.StoragesId);
        }
    }
}
