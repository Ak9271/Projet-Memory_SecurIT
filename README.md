# 🔒 SecurIT Memory

Bienvenue dans **SecurIT Memory** ! Un jeu de Memory captivant sur le thème de la cybersécurité, développé en C# avec Windows Forms. Testez votre mémoire et retrouvez les paires d'images liées à l'univers de la sécurité informatique !

---

## ✨ Fonctionnalités

- **Menu Principal Élégant** : Interface claire pour démarrer une partie, modifier les options ou quitter.
- **Difficulté Modulable** : Choisissez la taille de votre grille selon votre niveau de défi :
  - 4 paires (Grille 2x4)
  - 6 paires (Grille 3x4)
  - 8 paires (Grille 4x4)
- **Gameplay Fluide** :
  - Grille de jeu générée dynamiquement.
  - Animations de retournement de cartes avec délai naturel.
- **Suivi des Performances** :
  - Compteur d'essais en temps réel.
  - Chronomètre intégré pour mesurer votre rapidité.
- **Écran de Victoire** : Message de félicitations affichant votre temps et le nombre d'essais une fois toutes les paires trouvées.

---

## 🛠️ Technologies Utilisées

- **Langage** : C# (.NET 5.0)
- **Interface Graphique** : Windows Forms (WinForms)
- **Environnement** : Visual Studio / VS Code

---

## 🚀 Installation & Lancement

1. **Prérequis** : Assurez-vous d'avoir le SDK .NET installé sur votre machine.
2. **Cloner ou Télécharger** le projet sur votre ordinateur.
3. Ouvrez un terminal dans le dossier racine du projet (là où se trouve `MemorySecurIT.csproj`).
4. Lancez la commande suivante :

```bash
dotnet run --project MemorySecurIT.csproj
```

> **⚠️ Dépannage / Problèmes fréquents :**
> Selon la configuration de votre PC ou la version de Visual Studio installée, le fichier `MemorySecurIT.csproj` peut parfois poser problème (notamment des erreurs de version cible `.NET`). 
> Si la commande ci-dessus échoue, essayez de :
> 1. Ouvrir le projet directement dans **Visual Studio** en double-cliquant sur la solution `Projet-C#.sln` (ou le fichier `.csproj`).
> 2. Lancer le projet via le bouton **"Démarrer"** (Play) de Visual Studio. Visual Studio gérera automatiquement la restauration des dépendances et les ajustements nécessaires.

---

## 📁 Structure du Projet

L'architecture du projet est organisée de manière claire et modulaire :

- 📂 **Classes/** : Contient la logique métier
  - `Carte.cs` : Représente le modèle d'une carte (état, image, id).
  - `JeuMemory.cs` : Gère la logique principale du jeu (mélange, paires, vérifications).
  - `AppConfig.cs` : Gère la configuration globale (ex: nombre de paires choisi).
- 📂 **Forms/** : Contient les interfaces graphiques
  - `MenuForm.cs` : Fenêtre du menu principal et gestion des options.
  - `GameForm.cs` : Fenêtre principale du jeu où l'action se déroule.
- 📂 **Assets/Images/** : Dossier contenant les ressources visuelles du jeu.

---

## 🖼️ Gestion des Images

Pour personnaliser le jeu, ajoutez vos images au format `.png` dans le dossier `Assets/Images/` :

- `back.png` : Image utilisée pour le dos de toutes les cartes.
- *Autres images* : Les images qui formeront les paires à trouver (ex: `firewall.png`, `virus.png`, `hacker.png`, etc.).

> **💡 Note :** Le jeu est conçu pour être résilient. Si les images sont manquantes, le système génère automatiquement des cartes de secours colorées avec du texte pour éviter tout crash et garantir que le jeu se lance correctement !

---

## 🎮 Comment Jouer ?

1. Lancez le jeu.
2. Cliquez sur **Options** (depuis le menu principal) si vous souhaitez ajuster la difficulté.
3. Cliquez sur **Jouer** !
4. Cliquez sur deux cartes pour les retourner. Si elles sont identiques, la paire reste face visible. Sinon, elles se retournent face cachée après un court instant.
5. Retrouvez toutes les paires le plus rapidement possible avec le moins d'essais !

---

*Projet développé dans le cadre de la formation Ynov B2.*
