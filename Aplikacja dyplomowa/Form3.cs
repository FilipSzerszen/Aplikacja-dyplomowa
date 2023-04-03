using System;
using System.IO;
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
        private int dzień = DateTime.Today.Day;
        private int miesiąc = DateTime.Today.Month;
        private int rok = DateTime.Today.Year;

        private SortedList<DateTime, string> listaAktywności = new SortedList<DateTime, string>();

        public Form3()
        {
            InitializeComponent();

            LBLaktData.Text = DateTime.Today.ToString("D");
            LBLwybranaData.Text = DateTime.Today.ToString("U");
            listaAktywności = zbierzDane();
            AktualizujKalendarz(DateTime.Today);
        }

        public SortedList<DateTime, string> zbierzDane()
        {
            try
            {
                string[] ścieżki = Directory.GetFiles(@".\Tripy", "*.aar");
                TBoxListaDnia2.Text += "\r\nElementów: " + ścieżki.Length;
                listaAktywności.Clear();
                for (int i = 0; i < ścieżki.Length; i++)
                {
                    DateTime tempData = new DateTime(int.Parse(ścieżki[i].Substring(8, 4)), int.Parse(ścieżki[i].Substring(12, 2)), int.Parse(ścieżki[i].Substring(14, 2)));
                    if (listaAktywności.ContainsKey(tempData))
                    {
                        string tempString = listaAktywności.Values.ElementAt(listaAktywności.IndexOfKey(tempData));
                        listaAktywności.RemoveAt(listaAktywności.IndexOfKey(tempData));
                        tempString += "\r\n" + ścieżki[i].Substring(17);
                        listaAktywności.Add(tempData, tempString);
                    }
                    else
                    {
                        listaAktywności.Add(tempData, ścieżki[i].Substring(17));
                    }
                }
            }
            catch (Exception)
            {
                throw new Exception();
            }
            return listaAktywności;
        }



        private void AktualizujKalendarz(DateTime data)
        {
            int dzieńTygodniaStart = (int)((new DateTime(data.Year, data.Month, 1)).DayOfWeek);
            if (dzieńTygodniaStart == 0) dzieńTygodniaStart = 7;    //niedzielom nadaj index 7, a nie 0
            if (dzieńTygodniaStart == 1) dzieńTygodniaStart = 8;    //jeśli idealnie wypadł od pn to zacznij tydzień później (wzgl. wizualne)   

            var Buttons = this.gBox.Controls.OfType<Button>();
            for (int i = 1; i < 43; i++)
            {
                var b = Buttons.Where(p => p.Name == "button" + i).FirstOrDefault();

                if (i >= dzieńTygodniaStart && i <= dzieńTygodniaStart - 1 + DateTime.DaysInMonth(data.Year, data.Month))
                {   //pokaż dni aktualnego miesiąca
                    DateTime aktualna = new DateTime(rok, miesiąc, i - dzieńTygodniaStart + 1);
                    if (listaAktywności.ContainsKey(aktualna)) b.ForeColor = Color.Red; else b.ForeColor = Color.Black;
                    b.Enabled = (aktualna <= DateTime.Today);
                    b.Text = (i - dzieńTygodniaStart + 1).ToString();
                }
                else if (i < dzieńTygodniaStart)
                {   //wyszarz początek kalendarza
                    b.ForeColor = Color.Black;
                    b.Enabled = false;
                    b.Text = (DateTime.DaysInMonth(data.Month == 1 ? 12 : data.Year - 1, data.Month == 1 ? 12 : data.Month - 1) - dzieńTygodniaStart + i + 1).ToString();
                }
                else
                {   ////wyszarz reszte czyli koniec kalendarza
                    b.ForeColor = Color.Black;
                    b.Enabled = false;
                    b.Text = (i - dzieńTygodniaStart + 1 - DateTime.DaysInMonth(data.Year, data.Month)).ToString();
                }

            }
            LBLwybranaData.Text = data.ToString("U");
        }

        private void BTNdataWstecz_Click(object sender, EventArgs e)
        {
            if (miesiąc == 1) { miesiąc = 12; rok -= 1; }
            else miesiąc -= 1;
            AktualizujKalendarz(new DateTime(rok, miesiąc, dzień));
        }

        private void BTNdataPrzód_Click(object sender, EventArgs e)
        {
            if (miesiąc == 12) { miesiąc = 1; rok += 1; }
            else miesiąc += 1;
            AktualizujKalendarz(new DateTime(rok, miesiąc, dzień));
        }

        private void LBLaktData_Click(object sender, EventArgs e)
        {
            dzień = DateTime.Today.Day;
            miesiąc = DateTime.Today.Month;
            rok = DateTime.Today.Year;
            AktualizujKalendarz(new DateTime(rok, miesiąc, dzień));
        }
        private void wczytajListęDnia(Control sender) {
            LBoxListaDnia.Items.Clear();
            if (sender.ForeColor == Color.Red)
            {
                string temp = listaAktywności.Values.ElementAt(listaAktywności.IndexOfKey(new DateTime(rok, miesiąc, int.Parse(sender.Text))));
                using (StringReader reader = new StringReader(temp))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null) LBoxListaDnia.Items.Add(line);
                }              
            }
        }
        #region Podłączenie przycisków groupboxa
        private void button1_Click(object sender, EventArgs e) { wczytajListęDnia((Control)sender); }
        private void button2_Click(object sender, EventArgs e) { wczytajListęDnia((Control)sender); }
        private void button3_Click(object sender, EventArgs e) { wczytajListęDnia((Control)sender); }
        private void button4_Click(object sender, EventArgs e) { wczytajListęDnia((Control)sender); }
        private void button5_Click(object sender, EventArgs e) { wczytajListęDnia((Control)sender); }
        private void button6_Click(object sender, EventArgs e) { wczytajListęDnia((Control)sender); }
        private void button7_Click(object sender, EventArgs e) { wczytajListęDnia((Control)sender); }
        private void button8_Click(object sender, EventArgs e) { wczytajListęDnia((Control)sender); }
        private void button9_Click(object sender, EventArgs e) { wczytajListęDnia((Control)sender); }
        private void button10_Click(object sender, EventArgs e) { wczytajListęDnia((Control)sender); }
        private void button11_Click(object sender, EventArgs e) { wczytajListęDnia((Control)sender); }
        private void button12_Click(object sender, EventArgs e) { wczytajListęDnia((Control)sender); }
        private void button13_Click(object sender, EventArgs e) { wczytajListęDnia((Control)sender); }
        private void button14_Click(object sender, EventArgs e) { wczytajListęDnia((Control)sender); }
        private void button15_Click(object sender, EventArgs e) { wczytajListęDnia((Control)sender); }
        private void button16_Click(object sender, EventArgs e) { wczytajListęDnia((Control)sender); }
        private void button17_Click(object sender, EventArgs e) { wczytajListęDnia((Control)sender); }
        private void button18_Click(object sender, EventArgs e) { wczytajListęDnia((Control)sender); }
        private void button19_Click(object sender, EventArgs e) { wczytajListęDnia((Control)sender); }
        private void button20_Click(object sender, EventArgs e) { wczytajListęDnia((Control)sender); }
        private void button21_Click(object sender, EventArgs e) { wczytajListęDnia((Control)sender); }
        private void button22_Click(object sender, EventArgs e) { wczytajListęDnia((Control)sender); }
        private void button23_Click(object sender, EventArgs e) { wczytajListęDnia((Control)sender); }
        private void button24_Click(object sender, EventArgs e) { wczytajListęDnia((Control)sender); }
        private void button25_Click(object sender, EventArgs e) { wczytajListęDnia((Control)sender); }
        private void button26_Click(object sender, EventArgs e) { wczytajListęDnia((Control)sender); }
        private void button27_Click(object sender, EventArgs e) { wczytajListęDnia((Control)sender); }
        private void button28_Click(object sender, EventArgs e) { wczytajListęDnia((Control)sender); }
        private void button29_Click(object sender, EventArgs e) { wczytajListęDnia((Control)sender); }
        private void button30_Click(object sender, EventArgs e) { wczytajListęDnia((Control)sender); }
        private void button31_Click(object sender, EventArgs e) { wczytajListęDnia((Control)sender); }
        private void button32_Click(object sender, EventArgs e) { wczytajListęDnia((Control)sender); }
        private void button33_Click(object sender, EventArgs e) { wczytajListęDnia((Control)sender); }
        private void button34_Click(object sender, EventArgs e) { wczytajListęDnia((Control)sender); }
        private void button35_Click(object sender, EventArgs e) { wczytajListęDnia((Control)sender); }
        private void button36_Click(object sender, EventArgs e) { wczytajListęDnia((Control)sender); }
        private void button37_Click(object sender, EventArgs e) { wczytajListęDnia((Control)sender); }
        private void button38_Click(object sender, EventArgs e) { wczytajListęDnia((Control)sender); }
        private void button39_Click(object sender, EventArgs e) { wczytajListęDnia((Control)sender); }
        private void button40_Click(object sender, EventArgs e) { wczytajListęDnia((Control)sender); }
        private void button41_Click(object sender, EventArgs e) { wczytajListęDnia((Control)sender); }
        private void button42_Click(object sender, EventArgs e) { wczytajListęDnia((Control)sender); }
        #endregion

        private void TBoxListaDnia_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sItem = System.Convert.ToString(this.LBoxListaDnia.SelectedItem);
            System.Windows.Forms.MessageBox.Show(sItem);
            TBoxListaDnia2.Text = ((Control)sender).ToString();
            TBoxListaDnia2.Text += "\r\n"+(e.ToString());
        }

        private void button43_Click(object sender, EventArgs e)
        {
            listaAktywności = zbierzDane();
            foreach (var aktywność in listaAktywności)
            {
                LBoxListaDnia.Items.Add("\r\n" + aktywność.Key.ToString("dd-MM-yyyy") + " " + aktywność.Value);
                // TBoxListaDnia.Text += "\r\n" + aktywność.Key.ToString("dd-MM-yyyy") + " " + aktywność.Value;
            }
        }
    }
}