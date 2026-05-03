using System;
using System.Drawing;
using System.Windows.Forms;

namespace MemorySecurIT.Forms
{
    public partial class MenuForm : Form
    {
        private Button btnDemarrer;
<<<<<<< HEAD
        private Button btnPause;
=======
        private Button btnOptions;
        private Button btnQuitter;
>>>>>>> 209c3a9077fa706c9428e581ff6c8a0bcfafbefa
        private Label lblTitre;

        public MenuForm()
        {
            InitializeC         omponent();
        }

        private void InitializeComponent()
        {
<<<<<<< HEAD
            this.Text = "Memory SecurIT - Menu";
            this.Size = new Size(400, 300);
=======
            this.Text = "Memory SecurIT";
            this.Size = new Size(480, 500);
>>>>>>> 209c3a9077fa706c9428e581ff6c8a0bcfafbefa
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.BackColor = Color.FromArgb(45, 45, 48);

<<<<<<< HEAD
            int formWidth = this.ClientSize.Width;
=======
            panelCard = new Panel()
            {
                Size = new Size(340, 360),
                Location = new Point((this.ClientSize.Width - 340) / 2, 50),
                BackColor = Color.FromArgb(18, 24, 40),
            };
            this.Controls.Add(panelCard);
>>>>>>> 209c3a9077fa706c9428e581ff6c8a0bcfafbefa

            lblTitre = new Label()
            {
                Text = "Memory SecurIT",
                Font = new Font("Segoe UI", 24, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = false,
                Size = new Size(formWidth, 60),
                Location = new Point(0, 30),
                TextAlign = ContentAlignment.MiddleCenter
            };
            this.Controls.Add(lblTitre);

            //Démarrer bouton
            int btnWidth = 150;
            int centerX = (formWidth - btnWidth) / 2;

            btnDemarrer = new Button()
            {
                Text = "Démarrer",
                Size = new Size(btnWidth, 50),
                Location = new Point(centerX, 100),
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                BackColor = Color.FromArgb(0, 122, 204),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnDemarrer.FlatAppearance.BorderSize = 0;
            btnDemarrer.Click += BtnDemarrer_Click;
            this.Controls.Add(btnDemarrer);

<<<<<<< HEAD
            /*//Pause bouton ???
            btnPause = new Button()
            {
                Text = "Pause",
                Size = new Size(btnWidth, 50),
                Location = new Point(centerX, 170),
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                BackColor = Color.FromArgb(204, 120, 0),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
=======
            btnOptions = new Button()
            {
                Text = "OPTIONS",
                Size = new Size(200, 48),
                Location = new Point(70, 205),
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                BackColor = Color.FromArgb(26, 34, 55),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnOptions.FlatAppearance.BorderSize = 1;
            btnOptions.FlatAppearance.BorderColor = Color.FromArgb(40, 50, 80);
            btnOptions.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 122, 204);
            btnOptions.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 90, 160);
            btnOptions.Click += BtnOptions_Click;
            panelCard.Controls.Add(btnOptions);

            btnQuitter = new Button()
            {
                Text = "QUITTER",
                Size = new Size(200, 48),
                Location = new Point(70, 265),
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                BackColor = Color.FromArgb(26, 34, 55),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnQuitter.FlatAppearance.BorderSize = 1;
            btnQuitter.FlatAppearance.BorderColor = Color.FromArgb(40, 50, 80);
            btnQuitter.FlatAppearance.MouseOverBackColor = Color.FromArgb(200, 50, 50);
            btnQuitter.FlatAppearance.MouseDownBackColor = Color.FromArgb(160, 40, 40);
            btnQuitter.Click += BtnQuitter_Click;
            panelCard.Controls.Add(btnQuitter);

            lblVersion = new Label()
            {
                Text = "v1.0",
                Font = new Font("Segoe UI", 8, FontStyle.Regular),
                ForeColor = Color.FromArgb(60, 60, 80),
                AutoSize = false,
                Size = new Size(340, 20),
                Location = new Point(0, 325),
                TextAlign = ContentAlignment.MiddleCenter
>>>>>>> 209c3a9077fa706c9428e581ff6c8a0bcfafbefa
            };
            btnPause.FlatAppearance.BorderSize = 0;
            btnPause.Click += BtnPause_Click;
            this.Controls.Add(btnPause);
            */
        }


        private void BtnDemarrer_Click(object sender, EventArgs e)
        {
            GameForm gameForm = new GameForm();
            gameForm.Show();
            this.Hide();
        }

<<<<<<< HEAD
        private void BtnPause_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Jeu en pause !", "Pause", MessageBoxButtons.OK, MessageBoxIcon.Warning);
=======
        private void BtnOptions_Click(object sender, EventArgs e)
        {
            OptionsForm optionsForm = new OptionsForm();
            optionsForm.Show();
            this.Hide();
        }

        private void BtnQuitter_Click(object sender, EventArgs e)
        {
            Application.Exit();
>>>>>>> 209c3a9077fa706c9428e581ff6c8a0bcfafbefa
        }
    }
}