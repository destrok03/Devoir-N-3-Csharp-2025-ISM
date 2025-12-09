# GesBanqueAspNetPostgres

Application ASP.NET Core (C#) de gestion de comptes bancaires avec PostgreSQL.

Contenu :
- Code source ASP.NET Core
- DbContext et seed pour créer 5 comptes de test + transactions

Comment publier ce projet sur GitHub (publique) :

Option A — avec GitHub CLI (`gh`) (recommandé si `gh` est installé et connecté) :

```cmd
cd "c:\Users\AMINATA\Desktop\GesBanqueAspNetPostgres"
# Crée le repo public et pousse la branche actuelle
gh repo create <votre-nom-utilisateur>/GesBanqueAspNetPostgres --public --source=. --remote=origin --push
```

Option B — via l'interface web GitHub :
1. Aller sur https://github.com et créer un nouveau dépôt nommé `GesBanqueAspNetPostgres` (Public).
2. Puis dans le terminal local :

```cmd
cd "c:\Users\AMINATA\Desktop\GesBanqueAspNetPostgres"
# ajouter la remote (remplacez <VOTRE_URL> par l'URL fournie par GitHub)
git remote add origin https://github.com/<votre-nom-utilisateur>/GesBanqueAspNetPostgres.git
# pousser la branche actuelle (probablement main)
git push -u origin HEAD
```

Notes :
- Le dépôt local a été initialisé et commité localement (voir commit).
- Pour que je puisse créer et pousser le repo directement, il me faudrait un token ou l'accès à votre compte GitHub — je ne le ferai pas pour des raisons de sécurité. Je fournis des commandes claires à exécuter.

Si vous voulez, je peux aussi :
- Ajouter un fichier `.github/workflows/ci.yml` pour CI (build/test) ;
- Suggérer un README plus complet ;
- Vous préparer la commande `gh` exacte si vous me donnez votre nom d'utilisateur GitHub.
