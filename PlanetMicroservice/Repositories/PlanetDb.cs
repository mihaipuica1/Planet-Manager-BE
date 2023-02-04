using Microsoft.EntityFrameworkCore;

namespace PlanetMicroservice.Repositories
{
    public partial class PlanetDb : DbContext
    {
    
        public PlanetDb()
        {
        }
    
        public PlanetDb(DbContextOptions<PlanetDb> options)
            : base(options) { }

        public virtual DbSet<Planet> Planets { get; set; }
    
        // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        // {
        //     if (!optionsBuilder.IsConfigured)
        //     {
        //         optionsBuilder.UseMySQL("server=localhost;port=3306;user=root;password=parola123;database=planetmanager");
        //     }
        // }
    
        // protected override void OnModelCreating(ModelBuilder modelBuilder)
        // {
        //     modelBuilder.Entity<Planet>(entity =>
        //     {
        //         entity.ToTable("planets");
        //
        //         entity.Property(e => e.Id).HasColumnType("int(11)");
        //
        //         entity.Property(e => e.Name)
        //             .IsRequired();
        //
        //         entity.Property(e => e.Description);
        //
        //         entity.Property(e => e.Status)
        //             .IsRequired();
        //
        //         entity.Property(e => e.TeamId)
        //             .IsRequired();
        //     });
        //
        //     OnModelCreatingPartial(modelBuilder);
        // }
    
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }

}

