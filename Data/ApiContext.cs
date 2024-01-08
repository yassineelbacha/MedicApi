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
            modelBuilder.Entity<Personne>().HasKey(p => p.Id);  // Définir la clé primaire pour Patient
            modelBuilder.Entity<Appoin>().HasKey(a => a.Rid);  // Définir la clé primaire pour Appoin

            /*  modelBuilder.Entity<Appoin>()
                  .HasOne<Personne>()  // Définir le type de l'entité liée (Patient)
                  .WithMany()         // Aucune propriété de navigation dans Patient
                  .HasForeignKey(a => a.PersonneId);  // Clé étrangère*/
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

            // D'autres configurations de modèle peuvent être ajoutées ici...

            base.OnModelCreating(modelBuilder);
        }
        public DbSet<DossierMedical>? DossierMedicals { get; set; }
        public DbSet<Travail>? Travails { get; set; }
    }
}
