using KomputerBudowanieAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace KomputerBudowanieAPI.Database
{
    public class KomBuildDbContext : DbContext
    {
        public KomBuildDbContext(DbContextOptions<KomBuildDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<PcConfiguration> PC_Configurations { get; set; }
        public DbSet<GraphicCard> GraphicCards { get; set; }
        public DbSet<Cpu> Cpus { get; set; }
        public DbSet<CpuCooling> CPU_Coolings { get; set; }
        public DbSet<Fan> Fans { get; set; }
        public DbSet<Memory> Memories { get; set; }
        public DbSet<Ram> Rams { get; set; }
        public DbSet<Motherboard> Motherboards { get; set; }
        public DbSet<PowerSupply> PowerSupplys { get; set; }
        public DbSet<Case> Cases { get; set; }

        public DbSet<PcConfigurationMemory> PcConfigurationMemories { get; set; }
        public DbSet<PcConfigurationRam> PcConfigurationRams { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PcConfigurationRam>()
                .HasKey(pcr => new { pcr.PcConfigurationId, pcr.RamId });
            modelBuilder.Entity<PcConfigurationRam>()
                .HasOne(pc => pc.PcConfiguration)
                .WithMany(pcr => pcr.PcConfigurationRams)
                .HasForeignKey(r => r.PcConfigurationId);
            modelBuilder.Entity<PcConfigurationRam>()
                .HasOne(r => r.Ram)
                .WithMany(pcr => pcr.PcConfigurationRams)
                .HasForeignKey(r => r.RamId);

            modelBuilder.Entity<PcConfigurationMemory>()
                 .HasKey(pcr => new { pcr.PcConfigurationId, pcr.MemoryId });
            modelBuilder.Entity<PcConfigurationMemory>()
                .HasOne(pc => pc.PcConfiguration)
                .WithMany(pcm => pcm.PcConfigurationMemories)
                .HasForeignKey(r => r.PcConfigurationId);
            modelBuilder.Entity<PcConfigurationMemory>()
                .HasOne(m => m.Memory)
                .WithMany(pcm => pcm.PcConfigurationMemories)
                .HasForeignKey(m => m.MemoryId);
        }

    }
}
