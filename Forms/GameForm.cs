using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.IO;

namespace MemorySecurIT.Forms
{
    public partial class GameForm : Form
    {
        private Label lblTitre;
        private Button btnRetour;
        private Button btnPause;
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
        private int joueurActuel = 1;
        private List<Button> paireActuelle = new List<Button>();
        private bool tourEnCours = false;

        public GameForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Memory SecurIT - Jeu";
            this.Size = new Size(900, 750);
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

            btn4x4 = CreerBoutonMenu("4 x 4", 30, 4);
            btn6x6 = CreerBoutonMenu("6 x 6", 190, 6);
            btn8x8 = CreerBoutonMenu("8 x 8", 350, 8);

            panelBoutons.Controls.Add(btn4x4);
            panelBoutons.Controls.Add(btn6x6);
            panelBoutons.Controls.Add(btn8x8);

            panelGrille = new Panel()
            {
                Location = new Point(0, 180),
                Size = new Size(884, 400),
                AutoScroll = true,
                BackColor = Color.Transparent
            };
            this.Controls.Add(panelGrille);

            btnPause = new Button()
            {
                Text = "Pause",
                Size = new Size(120, 40),
                Location = new Point(740, 30),
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                BackColor = Color.FromArgb(204, 120, 0),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnPause.FlatAppearance.BorderSize = 0;
            btnPause.Click += BtnPause_Click;
            this.Controls.Add(btnPause);

            btnRetour = new Button()
            {
                Text = "Retour au Menu",
                Size = new Size(150, 40),
                Location = new Point(375, 620),
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

        private Button CreerBoutonMenu(string texte, int x, int taille)
        {
            Button btn = new Button()
            {
                Text = texte,
                Size = new Size(120, 50),
                Location = new Point(x, 5),
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                BackColor = Color.FromArgb(0, 122, 204),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btn.FlatAppearance.BorderSize = 0;
            btn.Click += (s, e) => CreerGrille(taille);
            return btn;
        }

        private void CreerGrille(int taille)
        {
            tailleGrille = taille;
            if (taille <= 4) tailleCarte = 80;
            else if (taille <= 6) tailleCarte = 70;
            else tailleCarte = 55;

            panelGrille.Controls.Clear();
            cartesSelectionnees.Clear();
            paireActuelle.Clear();
            tourEnCours = false;

            grilleCartes = new Button[tailleGrille, tailleGrille];

            List<string> symboles = new List<string>();
            int nbPaires = (tailleGrille * tailleGrille) / 2;
            
            string pathImages = Path.Combine(Application.StartupPath, "Assets", "Images");

            if (!Directory.Exists(pathImages))
            {
                MessageBox.Show("Dossier Assets/Images introuvable !");
                return;
            }

            string[] imageFiles = Directory.GetFiles(pathImages, "*.*");

            for (int i = 0; i < nbPaires; i++)
            {
                string img = imageFiles[i % imageFiles.Length];
                symboles.Add(img);
                symboles.Add(img);
            }

            Shuffle(symboles);

            int startX = (panelGrille.Width - (tailleGrille * tailleCarte + (tailleGrille - 1) * 10)) / 2;
            int startY = 20;
            int index = 0;

            for (int row = 0; row < tailleGrille; row++)
            {
                for (int col = 0; col < tailleGrille; col++)
                {
                    Button carte = new Button()
                    {
                        Text = "",
                        Size = new Size(tailleCarte, tailleCarte),
                        Location = new Point(startX + col * (tailleCarte + 10), startY + row * (tailleCarte + 10)),
                        BackColor = Color.FromArgb(0, 122, 204),
                        FlatStyle = FlatStyle.Flat,
                        Cursor = Cursors.Hand,
                        Tag = symboles[index],
                        BackgroundImageLayout = ImageLayout.Stretch
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
            if (tourEnCours) return;

            Button carte = sender as Button;
            
            if (carte != null && !cartesSelectionnees.Contains(carte) && !paireActuelle.Contains(carte))
            {
                carte.BackgroundImage = Image.FromFile(carte.Tag.ToString());
                paireActuelle.Add(carte);

                carte.FlatAppearance.BorderSize = 3;
                carte.FlatAppearance.BorderColor = (joueurActuel == 1) ? Color.Lime : Color.Red;

                if (paireActuelle.Count == 2)
                {
                    tourEnCours = true;
                    Button carte1 = paireActuelle[0];
                    Button carte2 = paireActuelle[1];

                    if (carte1.Tag.ToString() == carte2.Tag.ToString())
                    {
                        cartesSelectionnees.Add(carte1);
                        cartesSelectionnees.Add(carte2);
                        paireActuelle.Clear();
                        tourEnCours = false;
                        VerifierVictoire();
                    }
                    else
                    {
                        System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
                        timer.Interval = 1000;
                        timer.Tick += (s, args) =>
                        {
                            timer.Stop();
                            carte1.BackgroundImage = null;
                            carte2.BackgroundImage = null;
                            carte1.FlatAppearance.BorderSize = 0;
                            carte2.FlatAppearance.BorderSize = 0;
                            paireActuelle.Clear();
                            tourEnCours = false;
                            
                            joueurActuel = (joueurActuel == 1) ? 2 : 1;
                            timer.Dispose();
                        };
                        timer.Start();
                    }
                }
            }
        }

        private void VerifierVictoire()
        {
            if (cartesSelectionnees.Count == tailleGrille * tailleGrille)
            {
                MessageBox.Show("Félicitations ! La partie est terminée.");
            }
        }

        private void BtnPause_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Jeu en pause !", "Pause", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void BtnRetour_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void GameForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}