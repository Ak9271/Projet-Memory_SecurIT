using System;
using System.Drawing;
using System.Windows.Forms;

namespace MemorySecurIT.Forms
{
    public partial class MenuForm : Form
    {
        private Button btnDemarrer;
        private Button btnPause;
        private Label lblTitre;

        public MenuForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Memory SecurIT - Menu";
            this.Size = new Size(400, 300);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.BackColor = Color.FromArgb(45, 45, 48);

            int formWidth = this.ClientSize.Width;

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

        private void BtnPause_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Jeu en pause !", "Pause", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}