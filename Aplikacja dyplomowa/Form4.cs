using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                child.SetDesktopLocation(DesktopLocation.X + (Size.Width / 2) - child.Size.Width / 2, DesktopLocation.Y + (Size.Height / 2) - child.Size.Height / 2);

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
