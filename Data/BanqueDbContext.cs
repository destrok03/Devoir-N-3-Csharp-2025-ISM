using GesBanqueAspNet.Models;
using Microsoft.EntityFrameworkCore;

namespace GesBanqueAspNet.Data
{
    public class BanqueDbContext : DbContext
    {
        public BanqueDbContext(DbContextOptions<BanqueDbContext> options)
            : base(options)
        {
        }

        public DbSet<Compte> Comptes => Set<Compte>();
        public DbSet<TransactionCompte> Transactions => Set<TransactionCompte>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.ConfigureWarnings(w =>
                w.Ignore(Microsoft.EntityFrameworkCore.Diagnostics.RelationalEventId.PendingModelChangesWarning));
        }

        public void EnsureTablesCreated()
        {
            try
            {
                Database.ExecuteSql($@"
                    CREATE TABLE IF NOT EXISTS ""Comptes"" (
                        ""Id"" serial PRIMARY KEY,
                        ""Numero"" text NOT NULL UNIQUE,
                        ""Titulaire"" text NOT NULL,
                        ""Type"" integer NOT NULL,
                        ""DateCreation"" timestamp with time zone NOT NULL,
                        ""Statut"" integer NOT NULL,
                        ""SoldeActuel"" numeric NOT NULL,
                        ""DureeBlocageJours"" integer,
                        ""DateDeblocage"" timestamp with time zone
                    );

                    CREATE TABLE IF NOT EXISTS ""Transactions"" (
                        ""Id"" serial PRIMARY KEY,
                        ""Reference"" text NOT NULL,
                        ""DateOperation"" timestamp with time zone NOT NULL,
                        ""Type"" integer NOT NULL,
                        ""Montant"" numeric NOT NULL,
                        ""SoldeApres"" numeric NOT NULL,
                        ""Description"" text NOT NULL,
                        ""CompteId"" integer NOT NULL,
                        FOREIGN KEY(""CompteId"") REFERENCES ""Comptes""(""Id"") ON DELETE CASCADE
                    );

                    CREATE INDEX IF NOT EXISTS ""IX_Transactions_CompteId"" ON ""Transactions"" (""CompteId"");
                ");

                Database.ExecuteSql($@"
                    ALTER TABLE ""Comptes"" 
                    ADD COLUMN IF NOT EXISTS ""DureeBlocageJours"" integer;
                    
                    ALTER TABLE ""Comptes"" 
                    ADD COLUMN IF NOT EXISTS ""DateDeblocage"" timestamp with time zone;
                ");
            }
            catch
            {
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TransactionCompte>()
                .ToTable("Transactions");

            modelBuilder.Entity<Compte>()
                .HasIndex(c => c.Numero)
                .IsUnique();

            modelBuilder.Entity<Compte>()
                .HasMany(c => c.Transactions)
                .WithOne(t => t.Compte)
                .HasForeignKey(t => t.CompteId);
        }
    }
}
