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
        private Button btnRetour;
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
        private JeuMemory jeu = new JeuMemory();

        private System.Windows.Forms.Timer chronoTimer;
        private int tempsEcouleSecondes = 0;
        private Label lblChrono;

        private int essaisJ1 = 0;
        private int essaisJ2 = 0;
        private Label lblEssaisJ1;
        private Label lblEssaisJ2;

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

            chronoTimer = new System.Windows.Forms.Timer();
            chronoTimer.Interval = 1000;
            chronoTimer.Tick += ChronoTimer_Tick;

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
                Size = new Size(300, 70),
                Location = new Point(this.ClientSize.Width / 2 - 150, 0),
                TextAlign = ContentAlignment.MiddleCenter
            };
            panelHeader.Controls.Add(lblTourActuel);

            lblChrono = new Label()
            {
                Text = "⏱️ 00:00",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = TexteBlanc,
                AutoSize = false,
                Size = new Size(150, 70),
                Location = new Point(this.ClientSize.Width / 2 + 150, 0),
                TextAlign = ContentAlignment.MiddleCenter
            };
            panelHeader.Controls.Add(lblChrono);

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
                Size = new Size(170, 100),
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

            lblEssaisJ1 = new Label()
            {
                Text = "Essais : 0",
                Font = new Font("Segoe UI", 8, FontStyle.Regular),
                ForeColor = TexteGris,
                AutoSize = false,
                Size = new Size(170, 20),
                Location = new Point(0, 75),
                TextAlign = ContentAlignment.MiddleCenter
            };
            panelScoreJ1.Controls.Add(lblEssaisJ1);

            panelScoreJ2 = new Panel()
            {
                Size = new Size(170, 100),
                Location = new Point(20, 150),
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

            lblEssaisJ2 = new Label()
            {
                Text = "Essais : 0",
                Font = new Font("Segoe UI", 8, FontStyle.Regular),
                ForeColor = TexteGris,
                AutoSize = false,
                Size = new Size(170, 20),
                Location = new Point(0, 75),
                TextAlign = ContentAlignment.MiddleCenter
            };
            panelScoreJ2.Controls.Add(lblEssaisJ2);

            panelGrille = new Panel()
            {
                Location = new Point(220, 70),
                Size = new Size(this.ClientSize.Width - 220, this.ClientSize.Height - 70),
                AutoScroll = true,
                BackColor = Color.FromArgb(10, 10, 18)
            };
            this.Controls.Add(panelGrille);

            this.Load += (s, e) => CreerGrille(AppConfig.TailleGrille);
        }

        private void ChronoTimer_Tick(object sender, EventArgs e)
        {
            tempsEcouleSecondes++;
            TimeSpan time = TimeSpan.FromSeconds(tempsEcouleSecondes);
            lblChrono.Text = "⏱️ " + time.ToString(@"mm\:ss");
        }

        private void CreerGrille(int taille)
        {
            tailleGrille = taille;
            scoreJ1 = 0;
            scoreJ2 = 0;
            essaisJ1 = 0;
            essaisJ2 = 0;
            joueurActuel = 1;
            lblScoreJ1.Text = "0";
            lblScoreJ2.Text = "0";
            lblEssaisJ1.Text = "Essais : 0";
            lblEssaisJ2.Text = "Essais : 0";
            MettreAJourUiJoueur();

            tempsEcouleSecondes = 0;
            lblChrono.Text = "⏱️ 00:00";
            chronoTimer.Start();

            if (taille <= 4) tailleCarte = 90;
            else if (taille <= 6) tailleCarte = 75;
            else tailleCarte = 58;

            panelGrille.Controls.Clear();
            cartesSelectionnees.Clear();
            paireActuelle.Clear();
            tourEnCours = false;

            grilleCartes = new Button[tailleGrille, tailleGrille];

            string pathImages = Path.Combine(Application.StartupPath, "Assets", "Images");

            if (!Directory.Exists(pathImages))
            {
                MessageBox.Show("Dossier Assets/Images introuvable !");
                return;
            }

            jeu.Initialiser(taille, pathImages);

            int espacement = 8;
            int grilleLargeur = tailleGrille * tailleCarte + (tailleGrille - 1) * espacement;
            int grilleHauteur = tailleGrille * tailleCarte + (tailleGrille - 1) * espacement;
            int startX = (panelGrille.Width - grilleLargeur) / 2;
            int startY = (panelGrille.Height - grilleHauteur) / 2;
            if (startY < 20) startY = 20;

            for (int row = 0; row < tailleGrille; row++)
            {
                for (int col = 0; col < tailleGrille; col++)
                {
                    int index = row * tailleGrille + col;
                    Carte carteModele = jeu.Cartes[index];

                    Button carteBtn = new Button()
                    {
                        Text = "",
                        Size = new Size(tailleCarte, tailleCarte),
                        Location = new Point(startX + col * (tailleCarte + espacement), startY + row * (tailleCarte + espacement)),
                        BackColor = FondPanelClair,
                        FlatStyle = FlatStyle.Flat,
                        Cursor = Cursors.Hand,
                        Tag = carteModele,
                        BackgroundImageLayout = ImageLayout.Stretch,
                        BackgroundImage = lockImage
                    };
                    carteBtn.FlatAppearance.BorderSize = 1;
                    carteBtn.FlatAppearance.BorderColor = Color.FromArgb(40, 50, 80);
                    carteBtn.FlatAppearance.MouseOverBackColor = Color.FromArgb(30, 45, 75);
                    carteBtn.FlatAppearance.MouseDownBackColor = BleuClic;
                    carteBtn.Click += Carte_Click;
                    grilleCartes[row, col] = carteBtn;
                    panelGrille.Controls.Add(carteBtn);
                }
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

            Button carteBtn = sender as Button;
            if (carteBtn == null) return;

            Carte carteModele = carteBtn.Tag as Carte;
            if (carteModele == null) return;

            if (!cartesSelectionnees.Contains(carteBtn) && !paireActuelle.Contains(carteBtn))
            {
                carteBtn.BackgroundImage = Image.FromFile(carteModele.CheminImage);
                carteModele.Etat = EtatCarte.Revelee;
                paireActuelle.Add(carteBtn);

                carteBtn.FlatAppearance.BorderSize = 2;
                carteBtn.FlatAppearance.BorderColor = (joueurActuel == 1) ? BleuPrimaire : Color.FromArgb(80, 160, 255);

                if (paireActuelle.Count == 2)
                {
                    tourEnCours = true;

                    if (joueurActuel == 1)
                    {
                        essaisJ1++;
                        lblEssaisJ1.Text = $"Essais : {essaisJ1}";
                    }
                    else
                    {
                        essaisJ2++;
                        lblEssaisJ2.Text = $"Essais : {essaisJ2}";
                    }

                    Button btn1 = paireActuelle[0];
                    Button btn2 = paireActuelle[1];
                    Carte modele1 = btn1.Tag as Carte;
                    Carte modele2 = btn2.Tag as Carte;

                    if (jeu.VerifierPaire(modele1, modele2))
                    {
                        modele1.Etat = EtatCarte.Trouvee;
                        modele2.Etat = EtatCarte.Trouvee;

                        cartesSelectionnees.Add(btn1);
                        cartesSelectionnees.Add(btn2);

                        if (joueurActuel == 1) scoreJ1++;
                        else scoreJ2++;

                        lblScoreJ1.Text = scoreJ1.ToString();
                        lblScoreJ2.Text = scoreJ2.ToString();

                        btn1.FlatAppearance.BorderColor = Color.FromArgb(0, 200, 120);
                        btn2.FlatAppearance.BorderColor = Color.FromArgb(0, 200, 120);

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
                            modele1.Etat = EtatCarte.Cachee;
                            modele2.Etat = EtatCarte.Cachee;
                            btn1.BackgroundImage = lockImage;
                            btn2.BackgroundImage = lockImage;
                            btn1.FlatAppearance.BorderSize = 1;
                            btn2.FlatAppearance.BorderSize = 1;
                            btn1.FlatAppearance.BorderColor = Color.FromArgb(40, 50, 80);
                            btn2.FlatAppearance.BorderColor = Color.FromArgb(40, 50, 80);
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
            if (jeu.PartieTerminee())
            {
                chronoTimer.Stop();
                string vainqueur;
                if (scoreJ1 > scoreJ2) vainqueur = "Joueur 1 remporte la partie !";
                else if (scoreJ2 > scoreJ1) vainqueur = "Joueur 2 remporte la partie !";
                else vainqueur = "Égalité !";

                TimeSpan time = TimeSpan.FromSeconds(tempsEcouleSecondes);
                string tempsText = time.ToString(@"mm\:ss");

                MessageBox.Show(
                    $"Partie terminée en {tempsText} !\n\nJoueur 1 : {scoreJ1} paires (en {essaisJ1} essais)\nJoueur 2 : {scoreJ2} paires (en {essaisJ2} essais)\n\n{vainqueur}",
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