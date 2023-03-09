using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Aplikacja_dyplomowa
{
    static class Program
    {
        /// <summary>
        /// Główny punkt wejścia dla aplikacji.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Aplikacja_dyplomowa());
            Aplikacja_dyplomowa.Form1.SetDesktopLocation((Screen.PrimaryScreen.Bounds.Width - Aplikacja_dyplomowa.Form1.Size.Width) / 2, (Screen.PrimaryScreen.Bounds.Height - Aplikacja_dyplomowa.Form1.Size.Height) / 2);
        }
    }
}
