using GesBanqueAspNet.Models;

namespace GesBanqueAspNet.Data
{
    public static class DataSeeder
    {
        public static void Seed(BanqueDbContext db)
        {
            try
            {
                var random = new Random();
                var comptes = new List<Compte>();

                var noms = new[]
                {
                    "Aminata Diop",
                    "Jean Dupont",
                    "Seydou Traoré",
                    "Fatou Ndiaye",
                    "Marie Koné"
                };

                for (int i = 1; i <= 5; i++)
                {
                    var compte = new Compte
                    {
                        Numero = $"C00{i:00000}",
                        Titulaire = noms[i - 1],
                        Type = TypeCompte.Epargne,
                        DateCreation = DateTime.UtcNow.AddMonths(-3),
                        Statut = StatutCompte.Actif,
                        SoldeActuel = 0
                    };

                    decimal solde = 0;
                    for (int j = 1; j <= 15; j++)
                    {
                        var isDepot = random.Next(0, 2) == 0;
                        var montant = random.Next(50_000, 300_000);

                        if (isDepot)
                            solde += montant;
                        else
                            solde -= montant;

                        var tr = new TransactionCompte
                        {
                            Reference = $"T{i:000}{j:000}",
                            DateOperation = DateTime.UtcNow.AddDays(-j),
                            Type = isDepot ? TypeTransaction.Depot : TypeTransaction.Retrait,
                            Montant = montant,
                            SoldeApres = solde,
                            Description = isDepot ? "Dépôt test" : "Retrait test"
                        };
                        compte.Transactions.Add(tr);
                    }

                    compte.SoldeActuel = solde;
                    comptes.Add(compte);
                }

                db.Comptes.AddRange(comptes);
                db.SaveChanges();
                Console.WriteLine($"✓ {comptes.Count} comptes créés avec {comptes.Count * 15} transactions");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"✗ Erreur seed: {ex.Message}");
            }
        }
    }
}
