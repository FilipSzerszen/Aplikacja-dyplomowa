using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Aplikacja_dyplomowa
{
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
            //this.SetDesktopLocation(DesktopLocation.X + (Size.Width / 2) , DesktopLocation.Y + (Size.Height / 2) );
        }

        private void ZapiszDoKalendarza_Click(object sender, EventArgs e)
        {
            if (File.Exists(Form3.plik + " " + nazwaPliku.Text + ".aar"))
            {
                DialogResult dr = MessageBox.Show("Plik o podanej nazwie już istnieje. Nadpisać plik?", "Uwaga!", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                if (dr == DialogResult.No) return;
            }
            try
            {
                StreamWriter sw = new StreamWriter(Form3.plik + " " + nazwaPliku.Text + ".aar");
                sw.Write(Aplikacja_dyplomowa.Form1.tBoxTemp.Text);
                sw.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("W nazwie pliku użyto niedozwolonych znaków", "Uwaga!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            this.Close();
        }

        private void NazwaPliku_KeyUp(object sender, KeyEventArgs e)
        {
            if (nazwaPliku.Text == "") ZapiszDoKalendarza.Enabled = false; else ZapiszDoKalendarza.Enabled = true;
        }
    }
}
