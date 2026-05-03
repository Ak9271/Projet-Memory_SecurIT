using System;
using System.Collections.Generic;
using System.IO;

namespace MemorySecurIT.Classes
{
    public class JeuMemory
    {
        private List<Carte> _listeCartes;
        private int _scoreJ1;
        private int _scoreJ2;
        private int _joueurActuel;

        public List<Carte> ListeCartes => _listeCartes;
        public int ScoreJ1 => _scoreJ1;
        public int ScoreJ2 => _scoreJ2;
        public int JoueurActuel => _joueurActuel;

        public JeuMemory()
        {
            _listeCartes = new List<Carte>();
            _joueurActuel = 1;
        }

        public void PreparerPartie(int taille, string folderPath)
        {
            _listeCartes.Clear();
            _scoreJ1 = 0;
            _scoreJ2 = 0;
            _joueurActuel = 1;

            if (!Directory.Exists(folderPath)) return;

            string[] imageFiles = Directory.GetFiles(folderPath, "*.*");
            int nbPaires = (taille * taille) / 2;

            for (int i = 0; i < nbPaires; i++)
            {
                string img = imageFiles[i % imageFiles.Length];
                _listeCartes.Add(new Carte(i, img));
                _listeCartes.Add(new Carte(i, img));
            }

            Random rng = new Random();
            int n = _listeCartes.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                Carte temp = _listeCartes[k];
                _listeCartes[k] = _listeCartes[n];
                _listeCartes[n] = temp;
            }
        }

        public bool VerifierPaire(int index1, int index2)
        {
            if (_listeCartes[index1].Id == _listeCartes[index2].Id)
            {
                _listeCartes[index1].Etat = EtatCarte.Trouvee;
                _listeCartes[index2].Etat = EtatCarte.Trouvee;
                
                if (_joueurActuel == 1) _scoreJ1++;
                else _scoreJ2++;
                
                return true;
            }

            _listeCartes[index1].Etat = EtatCarte.Cachee;
            _listeCartes[index2].Etat = EtatCarte.Cachee;
            _joueurActuel = (_joueurActuel == 1) ? 2 : 1;
            return false;
        }

        public bool EstFini()
        {
            return _listeCartes.Count > 0 && _listeCartes.TrueForAll(c => c.Etat == EtatCarte.Trouvee);
        }
    }
}