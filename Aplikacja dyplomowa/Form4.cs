using System;
using System.Windows.Forms;

namespace Aplikacja_dyplomowa
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
            Aplikacja_dyplomowa.Form1.Enabled = false;
        }

        private void BtnAnuluj_Click(object sender, EventArgs e)
        {
            if (Aplikacja_dyplomowa.port_com != "")
            {
                Form2 child = new Form2();
                this.Close();    
                child.Show();
            }
            else
            {
                MessageBox.Show("Najpierw wybierz port COM!", "Uwaga!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Aplikacja_dyplomowa.Form1.Enabled = true;
                Aplikacja_dyplomowa.Form1.Focus();
                this.Close();
            }
        }

        private void Form4_FormClosed(object sender, FormClosedEventArgs e)
        {
            Aplikacja_dyplomowa.Form1.Enabled = true;
            Aplikacja_dyplomowa.Form1.Focus();
        }
    }
}
