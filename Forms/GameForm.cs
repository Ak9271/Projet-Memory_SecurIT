using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.IO;
using MemorySecurIT.Classes;

namespace MemorySecurIT.Forms
{
    public partial class GameForm : Form
    {
        private Label lblTitre;
        private Label lblTourActuel;
        private Panel panelScoreJ1;
        private Panel panelScoreJ2;
        private Label lblScoreJ1;
        private Label lblScoreJ2;
        private Label lblNomJ1;
        private Label lblNomJ2;
        private Label lblSectionTaille;
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
        private Panel panelHeader;
        private List<Button> cartesSelectionnees = new List<Button>();
        private int joueurActuel = 1;
        private List<Button> paireActuelle = new List<Button>();
        private bool tourEnCours = false;
        private int scoreJ1 = 0;
        private int scoreJ2 = 0;
        private Image lockImage = null;

        private readonly Color BleuPrimaire = Color.FromArgb(0, 122, 204);
        private readonly Color BleuSurvol = Color.FromArgb(0, 145, 235);
        private readonly Color BleuClic = Color.FromArgb(0, 90, 160);
        private readonly Color FondPrincipal = Color.FromArgb(10, 10, 18);
        private readonly Color FondPanel = Color.FromArgb(18, 24, 40);
        private readonly Color FondPanelClair = Color.FromArgb(26, 34, 55);
        private readonly Color TexteBlanc = Color.White;
        private readonly Color TexteGris = Color.FromArgb(140, 150, 170);

        public GameForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Memory SecurIT — Jeu";
            this.Size = new Size(1150, 800);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(10, 10, 18);
            this.FormClosing += GameForm_FormClosing;

            string logoPath = Path.Combine(Application.StartupPath, "Assets", "Images", "Logo.png");
            if (File.Exists(logoPath))
            {
                lockImage = Image.FromFile(logoPath);
            }

            panelHeader = new Panel()
            {
                Size = new Size(this.ClientSize.Width, 70),
                Location = new Point(0, 0),
                BackColor = Color.FromArgb(14, 18, 30)
            };
            this.Controls.Add(panelHeader);

            lblTitre = new Label()
            {
                Text = "MEMORY SECURIT",
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                ForeColor = TexteBlanc,
                AutoSize = false,
                Size = new Size(400, 70),
                Location = new Point(30, 0),
                TextAlign = ContentAlignment.MiddleLeft
            };
            panelHeader.Controls.Add(lblTitre);

            Label lblAccent = new Label()
            {
                Text = "●",
                Font = new Font("Segoe UI", 10),
                ForeColor = BleuPrimaire,
                AutoSize = true,
                Location = new Point(295, 27)
            };
            panelHeader.Controls.Add(lblAccent);

            lblTourActuel = new Label()
            {
                Text = "Tour du Joueur 1",
                Font = new Font("Segoe UI", 11, FontStyle.Regular),
                ForeColor = BleuPrimaire,
                AutoSize = false,
                Size = new Size(700, 70),
                Location = new Point(this.ClientSize.Width / 2 - 350, 0),
                TextAlign = ContentAlignment.MiddleCenter
            };
            panelHeader.Controls.Add(lblTourActuel);

            btnRetour = new Button()
            {
                Text = "← Menu",
                Size = new Size(100, 36),
                Location = new Point(this.ClientSize.Width - 130, 17),
                Font = new Font("Segoe UI", 10),
                BackColor = Color.FromArgb(30, 40, 60),
                ForeColor = TexteGris,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnRetour.FlatAppearance.BorderSize = 1;
            btnRetour.FlatAppearance.BorderColor = Color.FromArgb(50, 60, 90);
            btnRetour.FlatAppearance.MouseOverBackColor = Color.FromArgb(40, 55, 85);
            btnRetour.FlatAppearance.MouseDownBackColor = Color.FromArgb(20, 28, 45);
            btnRetour.Click += BtnRetour_Click;
            panelHeader.Controls.Add(btnRetour);

            panelGauche = new Panel()
            {
                Size = new Size(210, this.ClientSize.Height - 70),
                Location = new Point(0, 70),
                BackColor = Color.FromArgb(14, 18, 30)
            };
            this.Controls.Add(panelGauche);

            Panel separateurVertical = new Panel()
            {
                Size = new Size(1, this.ClientSize.Height - 70),
                Location = new Point(210, 70),
                BackColor = Color.FromArgb(30, 35, 55)
            };
            this.Controls.Add(separateurVertical);

            panelScoreJ1 = new Panel()
            {
                Size = new Size(170, 80),
                Location = new Point(20, 30),
                BackColor = FondPanelClair
            };
            panelGauche.Controls.Add(panelScoreJ1);

            lblNomJ1 = new Label()
            {
                Text = "JOUEUR 1",
                Font = new Font("Segoe UI", 8, FontStyle.Bold),
                ForeColor = BleuPrimaire,
                AutoSize = false,
                Size = new Size(170, 20),
                Location = new Point(0, 10),
                TextAlign = ContentAlignment.MiddleCenter
            };
            panelScoreJ1.Controls.Add(lblNomJ1);

            lblScoreJ1 = new Label()
            {
                Text = "0",
                Font = new Font("Segoe UI", 28, FontStyle.Bold),
                ForeColor = TexteBlanc,
                AutoSize = false,
                Size = new Size(170, 45),
                Location = new Point(0, 28),
                TextAlign = ContentAlignment.MiddleCenter
            };
            panelScoreJ1.Controls.Add(lblScoreJ1);

            panelScoreJ2 = new Panel()
            {
                Size = new Size(170, 80),
                Location = new Point(20, 130),
                BackColor = FondPanel
            };
            panelGauche.Controls.Add(panelScoreJ2);

            lblNomJ2 = new Label()
            {
                Text = "JOUEUR 2",
                Font = new Font("Segoe UI", 8, FontStyle.Bold),
                ForeColor = TexteGris,
                AutoSize = false,
                Size = new Size(170, 20),
                Location = new Point(0, 10),
                TextAlign = ContentAlignment.MiddleCenter
            };
            panelScoreJ2.Controls.Add(lblNomJ2);

            lblScoreJ2 = new Label()
            {
                Text = "0",
                Font = new Font("Segoe UI", 28, FontStyle.Bold),
                ForeColor = TexteGris,
                AutoSize = false,
                Size = new Size(170, 45),
                Location = new Point(0, 28),
                TextAlign = ContentAlignment.MiddleCenter
            };
            panelScoreJ2.Controls.Add(lblScoreJ2);

            Panel separateurH = new Panel()
            {
                Size = new Size(150, 1),
                Location = new Point(30, 240),
                BackColor = Color.FromArgb(30, 35, 55)
            };
            panelGauche.Controls.Add(separateurH);

            lblSectionTaille = new Label()
            {
                Text = "TAILLE DE GRILLE",
                Font = new Font("Segoe UI", 8, FontStyle.Bold),
                ForeColor = TexteGris,
                AutoSize = false,
                Size = new Size(170, 20),
                Location = new Point(20, 260),
                TextAlign = ContentAlignment.MiddleLeft
            };
            panelGauche.Controls.Add(lblSectionTaille);

            btn4x4 = CreerBoutonTaille("4 × 4", 290, 4);
            btn6x6 = CreerBoutonTaille("6 × 6", 345, 6);
            btn8x8 = CreerBoutonTaille("8 × 8", 400, 8);
            panelGauche.Controls.Add(btn4x4);
            panelGauche.Controls.Add(btn6x6);
            panelGauche.Controls.Add(btn8x8);

            panelGrille = new Panel()
            {
                Location = new Point(220, 70),
                Size = new Size(this.ClientSize.Width - 220, this.ClientSize.Height - 70),
                AutoScroll = true,
                BackColor = Color.FromArgb(10, 10, 18)
            };
            this.Controls.Add(panelGrille);
        }

        private Button CreerBoutonTaille(string texte, int y, int taille)
        {
            Button btn = new Button()
            {
                Text = texte,
                Size = new Size(170, 42),
                Location = new Point(20, y),
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                BackColor = FondPanelClair,
                ForeColor = TexteBlanc,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btn.FlatAppearance.BorderSize = 1;
            btn.FlatAppearance.BorderColor = Color.FromArgb(40, 50, 80);
            btn.FlatAppearance.MouseOverBackColor = BleuPrimaire;
            btn.FlatAppearance.MouseDownBackColor = BleuClic;
            btn.Click += (s, e) => CreerGrille(taille);
            return btn;
        }

        private void CreerGrille(int taille)
        {
            tailleGrille = taille;
            scoreJ1 = 0;
            scoreJ2 = 0;
            joueurActuel = 1;
            lblScoreJ1.Text = "0";
            lblScoreJ2.Text = "0";
            MettreAJourUiJoueur();

            if (taille <= 4) tailleCarte = 90;
            else if (taille <= 6) tailleCarte = 75;
            else tailleCarte = 58;

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

            string[] imageFiles = Directory.GetFiles(pathImages, "Icones_*.*");

            for (int i = 0; i < nbPaires; i++)
            {
                string img = imageFiles[i % imageFiles.Length];
                symboles.Add(img);
                symboles.Add(img);
            }

            Shuffle(symboles);

            int espacement = 8;
            int grilleLargeur = tailleGrille * tailleCarte + (tailleGrille - 1) * espacement;
            int grilleHauteur = tailleGrille * tailleCarte + (tailleGrille - 1) * espacement;
            int startX = (panelGrille.Width - grilleLargeur) / 2;
            int startY = (panelGrille.Height - grilleHauteur) / 2;
            if (startY < 20) startY = 20;
            int index = 0;

            for (int row = 0; row < tailleGrille; row++)
            {
                for (int col = 0; col < tailleGrille; col++)
                {
                    Button carte = new Button()
                    {
                        Text = "",
                        Size = new Size(tailleCarte, tailleCarte),
                        Location = new Point(startX + col * (tailleCarte + espacement), startY + row * (tailleCarte + espacement)),
                        BackColor = FondPanelClair,
                        FlatStyle = FlatStyle.Flat,
                        Cursor = Cursors.Hand,
                        Tag = symboles[index],
                        BackgroundImageLayout = ImageLayout.Stretch,
                        BackgroundImage = lockImage
                    };
                    carte.FlatAppearance.BorderSize = 1;
                    carte.FlatAppearance.BorderColor = Color.FromArgb(40, 50, 80);
                    carte.FlatAppearance.MouseOverBackColor = Color.FromArgb(30, 45, 75);
                    carte.FlatAppearance.MouseDownBackColor = BleuClic;
                    carte.Click += Carte_Click;
                    grilleCartes[row, col] = carte;
                    panelGrille.Controls.Add(carte);
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

        private void MettreAJourUiJoueur()
        {
            if (joueurActuel == 1)
            {
                panelScoreJ1.BackColor = FondPanelClair;
                lblScoreJ1.ForeColor = TexteBlanc;
                lblNomJ1.ForeColor = BleuPrimaire;
                panelScoreJ2.BackColor = FondPanel;
                lblScoreJ2.ForeColor = TexteGris;
                lblNomJ2.ForeColor = TexteGris;
                lblTourActuel.Text = "Tour du Joueur 1";
            }
            else
            {
                panelScoreJ2.BackColor = FondPanelClair;
                lblScoreJ2.ForeColor = TexteBlanc;
                lblNomJ2.ForeColor = BleuPrimaire;
                panelScoreJ1.BackColor = FondPanel;
                lblScoreJ1.ForeColor = TexteGris;
                lblNomJ1.ForeColor = TexteGris;
                lblTourActuel.Text = "Tour du Joueur 2";
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

                carte.FlatAppearance.BorderSize = 2;
                carte.FlatAppearance.BorderColor = (joueurActuel == 1) ? BleuPrimaire : Color.FromArgb(80, 160, 255);

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

                        lblScoreJ1.Text = scoreJ1.ToString();
                        lblScoreJ2.Text = scoreJ2.ToString();

                        carte1.FlatAppearance.BorderColor = Color.FromArgb(0, 200, 120);
                        carte2.FlatAppearance.BorderColor = Color.FromArgb(0, 200, 120);

                        paireActuelle.Clear();
                        tourEnCours = false;
                        VerifierVictoire();
                    }
                    else
                    {
                        System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
                        timer.Interval = 900;
                        timer.Tick += (s, args) =>
                        {
                            timer.Stop();
                            carte1.BackgroundImage = lockImage;
                            carte2.BackgroundImage = lockImage;
                            carte1.FlatAppearance.BorderSize = 1;
                            carte2.FlatAppearance.BorderSize = 1;
                            carte1.FlatAppearance.BorderColor = Color.FromArgb(40, 50, 80);
                            carte2.FlatAppearance.BorderColor = Color.FromArgb(40, 50, 80);
                            paireActuelle.Clear();
                            tourEnCours = false;
                            joueurActuel = (joueurActuel == 1) ? 2 : 1;
                            MettreAJourUiJoueur();
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
                string vainqueur;
                if (scoreJ1 > scoreJ2) vainqueur = "Joueur 1 remporte la partie !";
                else if (scoreJ2 > scoreJ1) vainqueur = "Joueur 2 remporte la partie !";
                else vainqueur = "Égalité !";

                MessageBox.Show(
                    $"Partie terminée !\n\nJoueur 1 : {scoreJ1} paires\nJoueur 2 : {scoreJ2} paires\n\n{vainqueur}",
                    "Résultat",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
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