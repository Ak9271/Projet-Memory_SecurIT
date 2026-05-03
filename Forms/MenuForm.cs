using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace MemorySecurIT.Forms
{
    public partial class MenuForm : Form
    {
        private Button btnDemarrer;
        private Label lblTitre;
        private Label lblSousTitre;
        private Label lblVersion;
        private Panel panelCard;

        public MenuForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Memory SecurIT";
            this.Size = new Size(480, 400);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.BackColor = Color.FromArgb(10, 10, 18);

            panelCard = new Panel()
            {
                Size = new Size(340, 260),
                Location = new Point((this.ClientSize.Width - 340) / 2, 50),
                BackColor = Color.FromArgb(18, 24, 40),
            };
            this.Controls.Add(panelCard);

            lblTitre = new Label()
            {
                Text = "MEMORY",
                Font = new Font("Segoe UI", 32, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = false,
                Size = new Size(340, 55),
                Location = new Point(0, 25),
                TextAlign = ContentAlignment.MiddleCenter
            };
            panelCard.Controls.Add(lblTitre);

            lblSousTitre = new Label()
            {
                Text = "S E C U R I T",
                Font = new Font("Segoe UI", 11, FontStyle.Regular),
                ForeColor = Color.FromArgb(0, 122, 204),
                AutoSize = false,
                Size = new Size(340, 24),
                Location = new Point(0, 80),
                TextAlign = ContentAlignment.MiddleCenter
            };
            panelCard.Controls.Add(lblSousTitre);

            Panel separator = new Panel()
            {
                Size = new Size(60, 2),
                Location = new Point(140, 114),
                BackColor = Color.FromArgb(0, 122, 204)
            };
            panelCard.Controls.Add(separator);

            btnDemarrer = new Button()
            {
                Text = "JOUER",
                Size = new Size(200, 48),
                Location = new Point(70, 145),
                Font = new Font("Segoe UI", 13, FontStyle.Bold),
                BackColor = Color.FromArgb(0, 122, 204),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnDemarrer.FlatAppearance.BorderSize = 0;
            btnDemarrer.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 145, 235);
            btnDemarrer.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 90, 160);
            btnDemarrer.Click += BtnDemarrer_Click;
            panelCard.Controls.Add(btnDemarrer);

            lblVersion = new Label()
            {
                Text = "v1.0",
                Font = new Font("Segoe UI", 8, FontStyle.Regular),
                ForeColor = Color.FromArgb(60, 60, 80),
                AutoSize = false,
                Size = new Size(340, 20),
                Location = new Point(0, 225),
                TextAlign = ContentAlignment.MiddleCenter
            };
            panelCard.Controls.Add(lblVersion);
        }

        private void BtnDemarrer_Click(object sender, EventArgs e)
        {
            GameForm gameForm = new GameForm();
            gameForm.Show();
            this.Hide();
        }
    }
}