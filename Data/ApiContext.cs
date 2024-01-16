using Microsoft.EntityFrameworkCore;
using MedicApi.Models;
namespace MedicApi.Data
{
    public class ApiContext : DbContext
    {
        public DbSet<Personne>? Personnes { get; set; }
        public DbSet<Appoin>? Appoins { get; set; }
        public ApiContext(DbContextOptions<ApiContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Personne>().HasKey(p => p.Id);  
            modelBuilder.Entity<Appoin>().HasKey(a => a.Rid);  

            /*  modelBuilder.Entity<Appoin>()
                  .HasOne<Personne>()  
                  .WithMany()         
                  .HasForeignKey(a => a.PersonneId); */
            modelBuilder.Entity<Appoin>()
     .HasOne<Personne>()
     .WithMany()
     .HasForeignKey(a => a.PersonneId)
     .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<DossierMedical>()
    .HasOne<Personne>()
    .WithMany()
    .HasForeignKey(d => d.PersonneId)
    .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Travail>()
                .HasOne<Personne>()
                .WithMany()
                .HasForeignKey(t => t.PersonneId)
                .OnDelete(DeleteBehavior.NoAction);


            base.OnModelCreating(modelBuilder);
        }
        public DbSet<DossierMedical>? DossierMedicals { get; set; }
        public DbSet<Travail>? Travails { get; set; }
    }
}
