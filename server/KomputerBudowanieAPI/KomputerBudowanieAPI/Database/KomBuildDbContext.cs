using KomputerBudowanieAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace KomputerBudowanieAPI.Database
{
    public class KomBuildDbContext : DbContext
    {
        public KomBuildDbContext(DbContextOptions<KomBuildDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<PC_Configuration> PC_Configurations { get; set; }
        public DbSet<GraphicCard> GraphicCards { get; set; }
        public DbSet<CPU> CPUs { get; set; }
        public DbSet<CPU_Cooling> CPU_Coolings { get; set; }
        public DbSet<Fan> Fans { get; set; }
        public DbSet<Memory> Memories { get; set; }
        public DbSet<Ram> Rams { get; set; }
        public DbSet<Motherboard> Motherboards { get; set; }
        public DbSet<PowerSupply> PowerSupplys { get; set; }
        public DbSet<Case> Cases { get; set; }

        /*
         * Dodanie OnModelCreating dla enumów ale to do przemyslonka jeszcze
         */

    }
}
