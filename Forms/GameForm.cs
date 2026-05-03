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
        private Label lblGrilleTaille;
        private Label lblScoreJ1;
        private Label lblScoreJ2;
        private Button btnRetour;
        private Button btn4x4;
        private Button btn6x6;
        private Button btn8x8;
        private Button[,] grilleCartes;
        private int tailleGrille = 0;
        private int tailleCarte = 70;
        private Random random = new Random();
        private Panel panelGrille;
        private Panel panelGauche;
        private List<Button> cartesSelectionnees = new List<Button>();
        private int joueurActuel = 1;
        private List<Button> paireActuelle = new List<Button>();
        private bool tourEnCours = false;
        private int scoreJ1 = 0;
        private int scoreJ2 = 0;

        public GameForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Memory SecurIT - Jeu";
            this.Size = new Size(1100, 780);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(45, 45, 48);
            this.FormClosing += GameForm_FormClosing;

            lblTitre = new Label()
            {
                Text = "SecurIT",
                Font = new Font("Segoe UI", 40, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = false,
                Size = new Size(this.ClientSize.Width, 80),
                Location = new Point(0, 10),
                TextAlign = ContentAlignment.MiddleCenter
            };
            this.Controls.Add(lblTitre);

            lblScoreJ1 = new Label()
            {
                Text = "Joueur 1 : 0",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = Color.Lime,
                AutoSize = false,
                Size = new Size(200, 40),
                Location = new Point(this.ClientSize.Width / 2 - 210, 90),
                TextAlign = ContentAlignment.MiddleRight
            };
            this.Controls.Add(lblScoreJ1);

            lblScoreJ2 = new Label()
            {
                Text = "Joueur 2 : 0",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = Color.Red,
                AutoSize = false,
                Size = new Size(200, 40),
                Location = new Point(this.ClientSize.Width / 2 + 10, 90),
                TextAlign = ContentAlignment.MiddleLeft
            };
            this.Controls.Add(lblScoreJ2);

            panelGauche = new Panel()
            {
                Size = new Size(200, 450),
                Location = new Point(20, 150),
                BackColor = Color.Transparent
            };
            this.Controls.Add(panelGauche);

            lblGrilleTaille = new Label()
            {
                Text = "Choisir taille",
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = false,
                Size = new Size(180, 40),
                Location = new Point(10, 10),
                TextAlign = ContentAlignment.MiddleLeft
            };
            panelGauche.Controls.Add(lblGrilleTaille);

            btn4x4 = CreerBoutonMenu("4 x 4", 70, 4);
            btn6x6 = CreerBoutonMenu("6 x 6", 165, 6);
            btn8x8 = CreerBoutonMenu("8 x 8", 260, 8);

            panelGauche.Controls.Add(btn4x4);
            panelGauche.Controls.Add(btn6x6);
            panelGauche.Controls.Add(btn8x8);

            panelGrille = new Panel()
            {
                Location = new Point(250, 150),
                Size = new Size(820, 550),
                AutoScroll = true,
                BackColor = Color.Transparent
            };
            this.Controls.Add(panelGrille);

            btnRetour = new Button()
            {
                Text = "Retour au Menu",
                Size = new Size(150, 40),
                Location = new Point(475, 710),
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

        private Button CreerBoutonMenu(string texte, int y, int taille)
        {
            Button btn = new Button()
            {
                Text = texte,
                Size = new Size(180, 80),
                Location = new Point(10, y),
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
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
            scoreJ1 = 0;
            scoreJ2 = 0;
            joueurActuel = 1;
            lblScoreJ1.Text = "Joueur 1 : 0";
            lblScoreJ2.Text = "Joueur 2 : 0";

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

            lblGrilleTaille.Text = $"Grille {taille} x {taille}";
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

                        if (joueurActuel == 1) scoreJ1++;
                        else scoreJ2++;

                        lblScoreJ1.Text = $"Joueur 1 : {scoreJ1}";
                        lblScoreJ2.Text = $"Joueur 2 : {scoreJ2}";

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
                string vainqueur = "";
                if (scoreJ1 > scoreJ2) vainqueur = "\nVictoire du Joueur 1 !";
                else if (scoreJ2 > scoreJ1) vainqueur = "\nVictoire du Joueur 2 !";
                else vainqueur = "\nÉgalité !";

                MessageBox.Show($"La partie est terminée !\n\nScore Final :\nJoueur 1 : {scoreJ1}\nJoueur 2 : {scoreJ2}{vainqueur}");
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