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
        private Label lblGrilleTaille;
        private Label lblScoreJ1;
        private Label lblScoreJ2;
        private Button btnRetour;
        private Button btn4x4;
        private Button btn6x6;
        private Button btn8x8;
        private Panel panelGrille;
        private Panel panelGauche;

        private JeuMemory _jeu;
        private List<Button> _boutonsSelectionnes = new List<Button>();
        private bool _bloquerClic = false;

        public GameForm()
        {
            _jeu = new JeuMemory();
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
            btn.Click += (s, e) => NouvellePartie(taille);
            return btn;
        }

        private void NouvellePartie(int taille)
        {
            string path = Path.Combine(Application.StartupPath, "Assets", "Images");
            _jeu.PreparerPartie(taille, path);
            
            lblScoreJ1.Text = "Joueur 1 : 0";
            lblScoreJ2.Text = "Joueur 2 : 0";
            lblGrilleTaille.Text = $"Grille {taille} x {taille}";
            
            MajAffichageGrille(taille);
        }

        private void MajAffichageGrille(int taille)
        {
            panelGrille.Controls.Clear();
            _boutonsSelectionnes.Clear();
            _bloquerClic = false;

            int tailleCarte = (taille <= 4) ? 80 : (taille <= 6) ? 70 : 55;
            int startX = (panelGrille.Width - (taille * tailleCarte + (taille - 1) * 10)) / 2;
            int startY = 20;

            for (int i = 0; i < _jeu.ListeCartes.Count; i++)
            {
                Button btn = new Button()
                {
                    Size = new Size(tailleCarte, tailleCarte),
                    Location = new Point(startX + (i % taille) * (tailleCarte + 10), startY + (i / taille) * (tailleCarte + 10)),
                    BackColor = Color.FromArgb(0, 122, 204),
                    FlatStyle = FlatStyle.Flat,
                    Cursor = Cursors.Hand,
                    Tag = i,
                    BackgroundImageLayout = ImageLayout.Stretch
                };
                btn.FlatAppearance.BorderSize = 0;
                btn.Click += Carte_Click;
                panelGrille.Controls.Add(btn);
            }
        }

        private void Carte_Click(object sender, EventArgs e)
        {
            if (_bloquerClic) return;

            Button btn = sender as Button;
            int index = (int)btn.Tag;

            if (_jeu.ListeCartes[index].Etat != EtatCarte.Cachee || _boutonsSelectionnes.Contains(btn)) return;

            btn.BackgroundImage = Image.FromFile(_jeu.ListeCartes[index].ImagePath);
            btn.FlatAppearance.BorderSize = 3;
            btn.FlatAppearance.BorderColor = (_jeu.JoueurActuel == 1) ? Color.Lime : Color.Red;
            _boutonsSelectionnes.Add(btn);

            if (_boutonsSelectionnes.Count == 2)
            {
                _bloquerClic = true;
                int i1 = (int)_boutonsSelectionnes[0].Tag;
                int i2 = (int)_boutonsSelectionnes[1].Tag;

                if (_jeu.VerifierPaire(i1, i2))
                {
                    lblScoreJ1.Text = $"Joueur 1 : {_jeu.ScoreJ1}";
                    lblScoreJ2.Text = $"Joueur 2 : {_jeu.ScoreJ2}";
                    _boutonsSelectionnes.Clear();
                    _bloquerClic = false;
                    
                    if (_jeu.EstFini())
                    {
                        MessageBox.Show($"Terminé !\nJ1: {_jeu.ScoreJ1} | J2: {_jeu.ScoreJ2}");
                    }
                }
                else
                {
                    System.Windows.Forms.Timer t = new System.Windows.Forms.Timer { Interval = 1000 };
                    t.Tick += (s, args) =>
                    {
                        t.Stop();
                        _boutonsSelectionnes[0].BackgroundImage = null;
                        _boutonsSelectionnes[0].FlatAppearance.BorderSize = 0;
                        _boutonsSelectionnes[1].BackgroundImage = null;
                        _boutonsSelectionnes[1].FlatAppearance.BorderSize = 0;
                        _boutonsSelectionnes.Clear();
                        _bloquerClic = false;
                        t.Dispose();
                    };
                    t.Start();
                }
            }
        }

        private void BtnRetour_Click(object sender, EventArgs e)
        {
            this.Hide();
            new MenuForm().Show();
        }

        private void GameForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}