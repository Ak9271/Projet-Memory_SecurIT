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
        private Button btn4x4;
        private Button btn6x6;
        private Button btn8x8;
        private Button[,] grilleCartes;
        private int tailleGrille = 0;
        private int tailleCarte = 70;
        private Random random = new Random();
        private Panel panelGrille;
        private Panel panelBoutons;
        private List<Button> cartesSelectionnees = new List<Button>();
        private int joueurActuel = 1; // 1 = vert, 2 = rouge
        private List<Button> paireActuelle = new List<Button>();
        private bool tourEnCours = false;

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

            lblTitre = new Label()
            {
                Text = "Choisissez la taille de grille",
                Font = new Font("Segoe UI", 20, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(250, 30)
            };
            this.Controls.Add(lblTitre);

            panelBoutons = new Panel()
            {
                Size = new Size(500, 60),
                Location = new Point(200, 100),
                BackColor = Color.Transparent
            };
            this.Controls.Add(panelBoutons);

            //4x4
            btn4x4 = new Button()
            {
                Text = "4 x 4",
                Size = new Size(120, 50),
                Location = new Point(30, 5),
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                BackColor = Color.FromArgb(0, 122, 204),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btn4x4.FlatAppearance.BorderSize = 0;
            btn4x4.Click += (s, e) => CreerGrille(4);
            panelBoutons.Controls.Add(btn4x4);

            //6x6
            btn6x6 = new Button()
            {
                Text = "6 x 6",
                Size = new Size(120, 50),
                Location = new Point(190, 5),
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                BackColor = Color.FromArgb(0, 122, 204),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btn6x6.FlatAppearance.BorderSize = 0;
            btn6x6.Click += (s, e) => CreerGrille(6);
            panelBoutons.Controls.Add(btn6x6);

            //8x8
            btn8x8 = new Button()
            {
                Text = "8 x 8",
                Size = new Size(120, 50),
                Location = new Point(350, 5),
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                BackColor = Color.FromArgb(0, 122, 204),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btn8x8.FlatAppearance.BorderSize = 0;
            btn8x8.Click += (s, e) => CreerGrille(8);
            panelBoutons.Controls.Add(btn8x8);

            panelGrille = new Panel()
            {
                Location = new Point(0, 180),
                Size = new Size(884, 380),
                AutoScroll = true,
                BackColor = Color.Transparent
            };
            this.Controls.Add(panelGrille);

            // Bouton Retour
            btnRetour = new Button()
            {
                Text = "Retour au Menu",
                Size = new Size(150, 40),
                Location = new Point(375, 600),
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

        private void CreerGrille(int taille)
        {
            tailleGrille = taille;
            
            //Adapter taille cartes selon grille
            if (taille <= 4) tailleCarte = 80;
            else if (taille <= 6) tailleCarte = 70;
            else tailleCarte = 55;

            //Vider grille existante
            panelGrille.Controls.Clear();

            grilleCartes = new Button[tailleGrille, tailleGrille];

            //Créer les paires 
            List<string> symboles = new List<string>();
            int nbPaires = (tailleGrille * tailleGrille) / 2;
            string[] symbols = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
            
            for (int i = 0; i < nbPaires; i++)
            {
                symboles.Add(symbols[i % symbols.Length]);
                symboles.Add(symbols[i % symbols.Length]);
            }

            //Mélanger symboles
            Shuffle(symboles);

            // créer bouton grille
            int startX = (panelGrille.Width - (tailleGrille * tailleCarte + (tailleGrille - 1) * 10)) / 2;
            int startY = 20;

            int index = 0;
            for (int row = 0; row < tailleGrille; row++)
            {
                for (int col = 0; col < tailleGrille; col++)
                {
                    Button carte = new Button()
                    {
                        Text = "?",
                        Size = new Size(tailleCarte, tailleCarte),
                        Location = new Point(startX + col * (tailleCarte + 10), startY + row * (tailleCarte + 10)),
                        Font = new Font("Segoe UI", 14, FontStyle.Bold),
                        BackColor = Color.FromArgb(0, 122, 204),
                        ForeColor = Color.White,
                        FlatStyle = FlatStyle.Flat,
                        Cursor = Cursors.Hand,
                        Tag = symboles[index]
                    };
                    carte.FlatAppearance.BorderSize = 0;
                    carte.Click += Carte_Click;
                    
                    grilleCartes[row, col] = carte;
                    panelGrille.Controls.Add(carte);
                    index++;
                }
            }


            lblTitre.Text = $"Grille {taille} x {taille}";
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
            // Empêcher de cliquer pendant un tour en cours
            if (tourEnCours) return;

            Button carte = sender as Button;
            if (carte != null && !cartesSelectionnees.Contains(carte))
            {
                // Révéler la carte
                carte.Text = carte.Tag?.ToString() ?? "?";
                paireActuelle.Add(carte);

                // Appliquer la couleur selon le joueur actuel
                if (joueurActuel == 1)
                {
                    carte.BackColor = Color.FromArgb(0, 180, 0); // Vert - Joueur 1
                }
                else
                {
                    carte.BackColor = Color.FromArgb(200, 50, 50); // Rouge - Joueur 2
                }

                // Quand 2 cartes sont sélectionnées, vérifier si elles correspondent
                if (paireActuelle.Count == 2)
                {
                    tourEnCours = true;
                    Button carte1 = paireActuelle[0];
                    Button carte2 = paireActuelle[1];

                    if (carte1.Tag?.ToString() == carte2.Tag?.ToString())
                    {
                        // Les cartes correspondent - les garder révélées
                        cartesSelectionnees.Add(carte1);
                        cartesSelectionnees.Add(carte2);
                        paireActuelle.Clear();
                        tourEnCours = false;
                    }
                    else
                    {
                        // Les cartes ne correspondent pas - les retourner après un délai
                        System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
                        timer.Interval = 1000;
                        timer.Tick += (s, args) =>
                        {
                            timer.Stop();
                            timer.Dispose();

                            // Remettre les cartes face cachée
                            carte1.Text = "?";
                            carte1.BackColor = Color.FromArgb(0, 122, 204);
                            carte2.Text = "?";
                            carte2.BackColor = Color.FromArgb(0, 122, 204);

                            paireActuelle.Clear();
                            tourEnCours = false;

                            // Changer de joueur après un échec
                            joueurActuel = (joueurActuel == 1) ? 2 : 1;
                        };
                        timer.Start();
                    }
                }
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