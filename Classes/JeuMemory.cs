using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace MemorySecurIT.Classes
{
    public class JeuMemory
    {
        private List<Carte> cartes;
        private Random random;

        public List<Carte> Cartes { get { return cartes; } }

        // initialise la liste de cartes
        public JeuMemory()
        {
            cartes = new List<Carte>();
            random = new Random();
        }

        // crée et mélange les cartes
        public void Initialiser(int taille, string pathImages)
        {
            cartes.Clear();

            string[] imageFiles = Directory.GetFiles(pathImages, "Icones_*.*");
            int nbPaires = (taille * taille) / 2;

            for (int i = 0; i < nbPaires; i++)
            {
                string img = imageFiles[i % imageFiles.Length];
                cartes.Add(new Carte(i, img));
                cartes.Add(new Carte(i, img));
            }

            Melanger(cartes);
        }

        // mélange aléatoirement la liste
        private void Melanger(List<Carte> liste)
        {
            int n = liste.Count;
            while (n > 1)
            {
                n--;
                int k = random.Next(n + 1);
                Carte valeur = liste[k];
                liste[k] = liste[n];
                liste[n] = valeur;
            }
        }

        // vérifie si deux cartes correspondent
        public bool VerifierPaire(Carte c1, Carte c2)
        {
            return c1.Id == c2.Id;
        }

        // vérifie si toutes les cartes sont trouvées
        public bool PartieTerminee()
        {
            foreach (Carte c in cartes)
            {
                if (c.Etat != EtatCarte.Trouvee)
                    return false;
            }
            return true;
        }
    }
}
