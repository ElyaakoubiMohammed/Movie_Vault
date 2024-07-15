using Microsoft.EntityFrameworkCore;
using ControlInventoryManagment.Models;

namespace ControlInventoryManagment.DatabaseContext;

    public class AppDbContext : DbContext
    {
        public DbSet<Categorie> Categories { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Entreprise> Entreprises { get; set; }
        public DbSet<Etage> Etages { get; set; }
        public DbSet<Local> Locals { get; set; }
        public DbSet<Operation> Operations { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Stockage> Stockages { get; set; }
        public DbSet<TypeEtage> TypeEtages { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            GeneratingRelations(modelBuilder);
        }

        private void GeneratingRelations(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Entreprise>()
                .HasOne(e => e.City)
                .WithMany()
                .HasForeignKey(e => e.CityId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Local>()
                .HasOne(l => l.City)
                .WithMany()
                .HasForeignKey(l => l.CityId)
                .OnDelete(DeleteBehavior.Restrict);
                
            modelBuilder.Entity<Etage>()
                .HasOne(e => e.Local)
                .WithMany(l => l.Etages)
                .HasForeignKey(e => e.LocalId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Stockage>()
                .HasOne(s => s.Etage)
                .WithMany(e => e.Stockages)
                .HasForeignKey(s =>s.EtagetId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Product>()
                .HasOne(p => p.Categorie)
                .WithMany(category => category.Products)
                .HasForeignKey(p => p.CategorieId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
