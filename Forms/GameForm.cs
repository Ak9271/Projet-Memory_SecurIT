using System;
using System.Drawing;
using System.Windows.Forms;

namespace MemorySecurIT.Forms
{
    public partial class GameForm : Form
    {
        private Label lblTitre;
        private Button btnRetour;

        public GameForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Memory SecurIT - Jeu";
            this.Size = new Size(800, 600);
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
                Location = new Point(300, 50)
            };
            this.Controls.Add(lblTitre);

            // Bouton Retour
            btnRetour = new Button()
            {
                Text = "Retour au Menu",
                Size = new Size(150, 40),
                Location = new Point(315, 500),
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