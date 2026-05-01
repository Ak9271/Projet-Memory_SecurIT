using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

namespace MemorySecurIT.Forms
{
    public partial class GameForm : Form
    {
        private Label lblTitre;
        private Button btnRetour;
        private Button[,] grilleCartes;
        private const int TAILLE_GRILLE = 6;
        private const int TAILLE_CARTE = 70;
        private Random random = new Random();

        public GameForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Memory SecurIT - Jeu";
            this.Size = new Size(900, 700);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(45, 45, 48);
            this.FormClosing += GameForm_FormClosing;

            // Titre
            lblTitre = new Label()
            {
                Text = "Jeu en cours...",
                Font = new Font("Segoe UI", 20, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(300, 30)
            };
            this.Controls.Add(lblTitre);

            // Créer la grille 6x6
            CreerGrille();

            // Bouton Retour
            btnRetour = new Button()
            {
                Text = "Retour au Menu",
                Size = new Size(150, 40),
                Location = new Point(315, 600),
                Font = new Font("Segoe UI", 12),
                BackColor = Color.FromArgb(100, 100, 100),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnRetour.FlatAppearance.BorderSize = 0;
            btnRetour.Click += BtnRetour_Click;
            this.Controls.Add(btnRetour);
        }

        private void CreerGrille()
        {
            grilleCartes = new Button[TAILLE_GRILLE, TAILLE_GRILLE];

            //Créer les paire
            List<string> symboles = new List<string>();
            string[] symbols = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M" };
            
            for (int i = 0; i < 18; i++)
            {
                symboles.Add(symbols[i % symbols.Length]);
                symboles.Add(symbols[i % symbols.Length]);
            }

            //Mélange  symboles
            Shuffle(symboles);

            //Créer boutons de la grille
            int startX = (this.ClientSize.Width - (TAILLE_GRILLE * TAILLE_CARTE + (TAILLE_GRILLE - 1) * 10)) / 2;
            int startY = 80;

            int index = 0;
            for (int row = 0; row < TAILLE_GRILLE; row++)
            {
                for (int col = 0; col < TAILLE_GRILLE; col++)
                {
                    Button carte = new Button()
                    {
                        Text = "?",
                        Size = new Size(TAILLE_CARTE, TAILLE_CARTE),
                        Location = new Point(startX + col * (TAILLE_CARTE + 10), startY + row * (TAILLE_CARTE + 10)),
                        Font = new Font("Segoe UI", 16, FontStyle.Bold),
                        BackColor = Color.FromArgb(0, 122, 204),
                        ForeColor = Color.White,
                        FlatStyle = FlatStyle.Flat,
                        Cursor = Cursors.Hand,
                        Tag = symboles[index]
                    };
                    carte.FlatAppearance.BorderSize = 0;
                    carte.Click += Carte_Click;
                    
                    grilleCartes[row, col] = carte;
                    this.Controls.Add(carte);
                    index++;
                }
            }
        }

        private void Shuffle<T>(List<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = random.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        private void Carte_Click(object sender, EventArgs e)
        {
            Button carte = sender as Button;
            if (carte != null)
            {
                // Révéler la carte
                carte.Text = carte.Tag?.ToString() ?? "?";
                carte.BackColor = Color.FromArgb(0, 180, 0);
            }
        }

        private void BtnRetour_Click(object sender, EventArgs e)
        {
            this.Hide();
            MenuForm menu = new MenuForm();
            menu.Show();
        }

        private void GameForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}