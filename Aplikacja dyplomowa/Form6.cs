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
    public partial class Form6 : Form
    {
        public Form6()
        {
            InitializeComponent();
            Aplikacja_dyplomowa.Form1.Enabled = false;
        }

        private void Form6_FormClosed(object sender, FormClosedEventArgs e)
        {
            Aplikacja_dyplomowa.Form1.Enabled = true;
            Aplikacja_dyplomowa.Form1.Focus();
        }
    }
}
