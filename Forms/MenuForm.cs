using System;
using System.Drawing;
using System.Windows.Forms;

namespace MemorySecurIT.Forms
{
    public partial class MenuForm : Form
    {
        private Button btnDemarrer;
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

            lblTitre = new Label()
            {
                Text = "MEMORY SECURIT",
                Font = new Font("Segoe UI", 24, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(80, 30)
            };
            this.Controls.Add(lblTitre);

            //Bouton Démarrer
            btnDemarrer = new Button()
            {
                Text = "Démarrer",
                Size = new Size(150, 50),
                Location = new Point(115, 100),
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                BackColor = Color.FromArgb(0, 122, 204),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnDemarrer.FlatAppearance.BorderSize = 0;
            btnDemarrer.Click += BtnDemarrer_Click;
            this.Controls.Add(btnDemarrer);

        }

        private void BtnDemarrer_Click(object sender, EventArgs e)
        {
            GameForm gameForm = new GameForm();
            gameForm.Show();
            this.Hide();
        }

    }
}