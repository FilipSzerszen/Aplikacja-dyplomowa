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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void button43_Click(object sender, EventArgs e)
        {
            DateTime date1 = DateTime.Now;
            DateTime date2 = DateTime.UtcNow;
            DateTime date3 = DateTime.Today;

            DateTime data = DateTime.Today;
            int dzień = data.Day;
            int miesiąc = data.Month;
            int rok = data.Year;

            tb.Text = date1 + "\r\n" + date2 + "\r\n" + date3.ToLocalTime().ToString("f") + "\r\n" + data.Month.ToString();
            switch (data.Month)
            {
                case 1: LBLaktData.Text = "styczeń"; break;
                case 2: LBLaktData.Text = "luty"; break;
                case 3: LBLaktData.Text = "marzec"; break;
                case 4: LBLaktData.Text = "kwiecień"; break;
                case 5: LBLaktData.Text = "maj"; break;
                case 6: LBLaktData.Text = "czerwiec"; break;
                case 7: LBLaktData.Text = "lipiec"; break;
                case 8: LBLaktData.Text = "sierkień"; break;
                case 9: LBLaktData.Text = "wrzesień"; break;
                case 10: LBLaktData.Text = "październik"; break;
                case 11: LBLaktData.Text = "listopad"; break;
                case 12: LBLaktData.Text = "grudzień"; break;
            }
            LBLaktData.Text += " " + data.Year.ToString();
            int dzieńTygodniaStart = (int)((new DateTime(data.Year, data.Month, 01)).DayOfWeek);
            if (dzieńTygodniaStart == 0) dzieńTygodniaStart = 7;

            tb.Text = dzieńTygodniaStart.ToString();

            var Buttons = this.gBox.Controls.OfType<Button>();
            for (int i = 1; i < 43; i++)
            {

                var b = Buttons.Where(p => p.Name == "button" + i).FirstOrDefault();
                if (i >= dzieńTygodniaStart && i <= dzieńTygodniaStart - 1 + DateTime.DaysInMonth(data.Year, data.Month))
                {
                    b.Enabled = true;
                    b.Text = (i - dzieńTygodniaStart + 1).ToString();
                }
                else if (i < dzieńTygodniaStart)
                {
                    b.Enabled = false;
                    b.Text = (DateTime.DaysInMonth(data.Month == 1 ? 12 : data.Year - 1, data.Month == 1 ? 12 : data.Month - 1) - dzieńTygodniaStart + i + 1).ToString();
                }
                else
                {
                    b.Enabled = false;
                    b.Text = (i - dzieńTygodniaStart + 1- DateTime.DaysInMonth(data.Year, data.Month)).ToString();
                }
            }
        }
    }
}
