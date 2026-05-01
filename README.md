# SecurIT Memory

Jeu de Memory sur le theme de la cybersecurite, developpe en C# avec WinForms.

## Technologies
- C# (.NET 8)
- Windows Forms (WinForms)

## Lancer le projet
1. Ouvrir le dossier dans Visual Studio ou VS Code
2. Lancer la commande:

```bash
dotnet run --project MemorySecurIT.csproj
```

## Fonctionnalites
- Menu principal: Jouer / Options / Quitter
- Options de taille: 4, 6 ou 8 paires
- Grille de jeu dynamique
- Retournement des cartes avec delai
- Compteur d'essais
- Chronometre
- Message de victoire en fin de partie

## Structure
- Classes/Carte.cs: modele d'une carte et son etat
- Classes/JeuMemory.cs: logique du jeu
- Classes/AppConfig.cs: configuration runtime (nombre de paires)
- Forms/MenuForm.cs: menu principal
- Forms/GameForm.cs: interface de jeu
- Assets/Images/: images des cartes (optionnelles)

## Images
Place des images `*.png` dans `Assets/Images/`:
- `back.png` pour le dos des cartes
- Les autres images pour les paires

Si des images sont manquantes, le jeu cree automatiquement des cartes de secours pour eviter les erreurs au lancement.
# Projet-Memory_SecurIT
