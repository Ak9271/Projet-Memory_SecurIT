using System;
using System.Drawing;
using System.Windows.Forms;
using MemorySecurIT.Classes;

namespace MemorySecurIT.Forms
{
    public partial class OptionsForm : Form
    {
        private Label lblTitre;
        private Label lblDescription;
        private Button btn4x4;
        private Button btn6x6;
        private Button btn8x8;
        private Button btnRetour;

        private readonly Color BleuPrimaire = Color.FromArgb(0, 122, 204);
        private readonly Color FondPrincipal = Color.FromArgb(10, 10, 18);
        private readonly Color FondPanel = Color.FromArgb(26, 34, 55);
        private readonly Color TexteBlanc = Color.White;

        // initialise la page options
        public OptionsForm()
        {
            InitializeComponent();
        }

        // construit les composants de la page
        private void InitializeComponent()
        {
            this.Text = "Memory SecurIT — Options";
            this.Size = new Size(480, 500);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.BackColor = FondPrincipal;

            lblTitre = new Label()
            {
                Text = "OPTIONS",
                Font = new Font("Segoe UI", 24, FontStyle.Bold),
                ForeColor = TexteBlanc,
                AutoSize = false,
                Size = new Size(480, 50),
                Location = new Point(0, 30),
                TextAlign = ContentAlignment.MiddleCenter
            };
            this.Controls.Add(lblTitre);

            lblDescription = new Label()
            {
                Text = "Choisissez la taille de la grille (Difficulté) :",
                Font = new Font("Segoe UI", 12),
                ForeColor = Color.FromArgb(140, 150, 170),
                AutoSize = false,
                Size = new Size(480, 30),
                Location = new Point(0, 100),
                TextAlign = ContentAlignment.MiddleCenter
            };
            this.Controls.Add(lblDescription);

            btn4x4 = CreerBoutonTaille("4 × 4 (Facile)", 150, 4);
            btn6x6 = CreerBoutonTaille("6 × 6 (Moyen)", 220, 6);
            btn8x8 = CreerBoutonTaille("8 × 8 (Difficile)", 290, 8);
            
            this.Controls.Add(btn4x4);
            this.Controls.Add(btn6x6);
            this.Controls.Add(btn8x8);

            MettreAJourBoutonActif();

            btnRetour = new Button()
            {
                Text = "RETOUR AU MENU",
                Size = new Size(200, 48),
                Location = new Point((this.ClientSize.Width - 200) / 2, 380),
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                BackColor = FondPanel,
                ForeColor = TexteBlanc,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnRetour.FlatAppearance.BorderSize = 1;
            btnRetour.FlatAppearance.BorderColor = Color.FromArgb(40, 50, 80);
            btnRetour.FlatAppearance.MouseOverBackColor = Color.FromArgb(40, 55, 85);
            btnRetour.FlatAppearance.MouseDownBackColor = Color.FromArgb(20, 28, 45);
            btnRetour.Click += BtnRetour_Click;
            this.Controls.Add(btnRetour);
        }

        // crée un bouton de sélection de taille
        private Button CreerBoutonTaille(string texte, int y, int taille)
        {
            Button btn = new Button()
            {
                Text = texte,
                Size = new Size(260, 50),
                Location = new Point((this.ClientSize.Width - 260) / 2, y),
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                BackColor = FondPanel,
                ForeColor = TexteBlanc,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btn.FlatAppearance.BorderSize = 2;
            btn.FlatAppearance.BorderColor = Color.FromArgb(40, 50, 80);
            btn.FlatAppearance.MouseOverBackColor = BleuPrimaire;
            btn.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 90, 160);
            btn.Click += (s, e) => 
            {
                AppConfig.TailleGrille = taille;
                MettreAJourBoutonActif();
            };
            return btn;
        }

        // surligne le bouton sélectionné
        private void MettreAJourBoutonActif()
        {
            ResetBouton(btn4x4);
            ResetBouton(btn6x6);
            ResetBouton(btn8x8);

            if (AppConfig.TailleGrille == 4) ActiverBouton(btn4x4);
            else if (AppConfig.TailleGrille == 6) ActiverBouton(btn6x6);
            else if (AppConfig.TailleGrille == 8) ActiverBouton(btn8x8);
        }

        // réinitialise l'apparence d'un bouton
        private void ResetBouton(Button btn)
        {
            btn.BackColor = FondPanel;
            btn.FlatAppearance.BorderColor = Color.FromArgb(40, 50, 80);
        }

        // active visuellement un bouton
        private void ActiverBouton(Button btn)
        {
            btn.BackColor = BleuPrimaire;
            btn.FlatAppearance.BorderColor = Color.FromArgb(0, 160, 255);
        }

        // revient au menu principal
        private void BtnRetour_Click(object sender, EventArgs e)
        {
            MenuForm menu = new MenuForm();
            menu.Show();
            this.Hide();
        }
        
        // ferme l'application à la fermeture
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
