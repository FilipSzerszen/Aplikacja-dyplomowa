using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;


namespace Aplikacja_dyplomowa
{
    public partial class Form3 : Form
    {
        public static string plik = "";

        private int dzień = DateTime.Today.Day;
        private int miesiąc = DateTime.Today.Month;
        private int rok = DateTime.Today.Year;

        private SortedList<DateTime, string> listaAktywności = new SortedList<DateTime, string>();

        public Form3()
        {
            InitializeComponent();
            Aplikacja_dyplomowa.Form1.Enabled = false;

            LBLaktData.Text = DateTime.Today.ToString("D");
            LBLwybranaData.Text = DateTime.Today.ToString("Y");
            listaAktywności = ZbierzDane();
            AktualizujKalendarz(DateTime.Today);
        }

        public SortedList<DateTime, string> ZbierzDane()
        {
            try
            {
                if (!Directory.Exists(@".\Tripy")) Directory.CreateDirectory(@".\Tripy");
                else
                {
                    string[] ścieżki = Directory.GetFiles(@".\Tripy", "*.aar");
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
                        else  listaAktywności.Add(tempData, ścieżki[i].Substring(17));
                    }
                }
            }
            catch (Exception)
            {
                throw new Exception();
            }
            return listaAktywności;
        }
        private void AktualizujListęDnia()
        {
            LBoxListaDnia.Items.Clear();
            int indeks = listaAktywności.IndexOfKey(new DateTime(rok, miesiąc, dzień));
            if (indeks < 0) return;
            string temp = listaAktywności.Values.ElementAt(indeks);
            using (StringReader reader = new StringReader(temp))
            {
                string line;
                while ((line = reader.ReadLine()) != null) LBoxListaDnia.Items.Add(line);
            }
        }

        private void AktualizujKalendarz(DateTime data)
        {
            int dzieńTygodniaStart = (int)new DateTime(data.Year, data.Month, 1).DayOfWeek;
            if (dzieńTygodniaStart == 0) dzieńTygodniaStart = 7;    //niedzielom nadaj index 7, a nie 0
            if (dzieńTygodniaStart == 1) dzieńTygodniaStart = 8;    //jeśli idealnie wypadł od pn to zacznij tydzień później (wzgl. wizualne)   

            var Buttons = this.gBox.Controls.OfType<Button>();
            for (int i = 1; i < 43; i++)
            {
                var b = Buttons.Where(p => p.Name == "button" + i).FirstOrDefault();
                if (b == null) break;
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
            LBLwybranaData.Text = data.ToString("Y"); ;
        }

        private void WyczyśćMałeMenu()
        {
            LBoxListaDnia.Items.Clear();
            BtPlus.Enabled = false;
            BtMinus.Enabled = false;
            BtWczytaj.Enabled = false;
        }

        private void WczytajListęDnia(Control sender)
        {
            dzień = int.Parse(sender.Text);
            if (sender.ForeColor == Color.Red)
            {
                AktualizujListęDnia();
            }
            else LBoxListaDnia.Items.Clear();
            BtMinus.Enabled = false;
            BtWczytaj.Enabled = false;
            if (Aplikacja_dyplomowa.Form1.wgrane) BtPlus.Enabled = true; else BtPlus.Enabled = false;
        }

        private void TBoxListaDnia_SelectedIndexChanged(object sender, EventArgs e)
        {
            string tripLBox = Convert.ToString(this.LBoxListaDnia.SelectedItem);
            if (tripLBox != null) { BtMinus.Enabled = true; BtWczytaj.Enabled = true; }
            else { BtMinus.Enabled = false; BtWczytaj.Enabled = false; }
        }

        private void LBLaktData_Click(object sender, EventArgs e)
        {
            dzień = DateTime.Today.Day;
            miesiąc = DateTime.Today.Month;
            rok = DateTime.Today.Year;
            AktualizujKalendarz(new DateTime(rok, miesiąc, dzień));
            WyczyśćMałeMenu();
        }

        private void BTNdataWstecz_Click(object sender, EventArgs e)
        {
            if (miesiąc == 1) { miesiąc = 12; rok -= 1; }
            else miesiąc -= 1;
            dzień = 1;
            AktualizujKalendarz(new DateTime(rok, miesiąc, dzień));
            WyczyśćMałeMenu();
        }

        private void BTNdataPrzód_Click(object sender, EventArgs e)
        {
            if (miesiąc == 12) { miesiąc = 1; rok += 1; }
            else miesiąc += 1;
            dzień = 1;
            AktualizujKalendarz(new DateTime(rok, miesiąc, dzień));
            WyczyśćMałeMenu();
        }

        private void BtPlus_Click(object sender, EventArgs e)
        {
            if (Aplikacja_dyplomowa.Form1.wgrane)
            {
                plik = Application.StartupPath + @"\Tripy\" + rok.ToString() + (miesiąc < 10 ? ("0" + miesiąc.ToString()) : miesiąc.ToString())
                                                                             + ((dzień < 10) ? ("0" + dzień.ToString()) : dzień.ToString());
                Form5 child = new Form5();
                child.ShowDialog();

                listaAktywności = ZbierzDane();
                AktualizujKalendarz(new DateTime(rok, miesiąc, dzień));
                AktualizujListęDnia();
            }
            else
            {
                MessageBox.Show("Nie można zapisać pustego pliku.\r\nWczytaj lub zaimportuj dane z urządzenia.", "Uwaga!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtMinus_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Na pewno chcesz usunąć zapis z wycieczki?", "Uwaga!", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            if (dr == DialogResult.Yes)
            {
                try
                {
                    plik = Application.StartupPath + @"\Tripy\" + rok.ToString() + (miesiąc < 10 ? ("0" + miesiąc.ToString()) : miesiąc.ToString()) + ((dzień < 10) ? ("0" + dzień.ToString()) : dzień.ToString()) + " " + this.LBoxListaDnia.SelectedItem.ToString();
                    if (File.Exists(plik)) File.Delete(plik);
                    else MessageBox.Show("Nie odnaleziono pliku!", "Uwaga!", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    listaAktywności = ZbierzDane();
                    AktualizujKalendarz(new DateTime(rok, miesiąc, dzień));
                    AktualizujListęDnia();

                    BtMinus.Enabled = false;
                    BtWczytaj.Enabled = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private void BtWczytaj_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Czy chcesz wczytać wybrany zapis wycieczki z kalendarza?", "Uwaga!", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            if (dr == DialogResult.Yes)
            {
                string plik = Application.StartupPath + @"\Tripy\" + rok.ToString() + (miesiąc < 10 ? ("0" + miesiąc.ToString()) : miesiąc.ToString()) + ((dzień < 10) ? ("0" + dzień.ToString()) : dzień.ToString()) + " " + this.LBoxListaDnia.SelectedItem.ToString();
                if (File.Exists(plik))
                {
                    try
                    {
                        StreamReader sr = new StreamReader(plik);
                        Aplikacja_dyplomowa.Form1.tBoxTemp.Text = sr.ReadToEnd();
                        sr.Close();
                        Aplikacja_dyplomowa.Form1.Konwertuj_dane();
                        Aplikacja_dyplomowa.Form1.Wypełnij_wykresy();
                    }
                    catch (IndexOutOfRangeException)
                    {
                        MessageBox.Show("Plik pusty lub nieprawidłowe dane!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Exception: " + ex.Message);
                        return;
                    }
                    Aplikacja_dyplomowa.Form1.Enabled = true;
                    Aplikacja_dyplomowa.Form1.Focus();
                    this.Close();
                }
                else MessageBox.Show("Nie odnaleziono pliku!", "Uwaga!", MessageBoxButtons.OK, MessageBoxIcon.Error);

                listaAktywności = ZbierzDane();
                AktualizujKalendarz(new DateTime(rok, miesiąc, dzień));
                AktualizujListęDnia();

                BtMinus.Enabled = false;
                BtWczytaj.Enabled = false;
            }
        }

        #region Podłączenie przycisków groupboxa
        private void Button1_Click(object sender, EventArgs e) { WczytajListęDnia((Control)sender); }
        private void Button2_Click(object sender, EventArgs e) { WczytajListęDnia((Control)sender); }
        private void Button3_Click(object sender, EventArgs e) { WczytajListęDnia((Control)sender); }
        private void Button4_Click(object sender, EventArgs e) { WczytajListęDnia((Control)sender); }
        private void Button5_Click(object sender, EventArgs e) { WczytajListęDnia((Control)sender); }
        private void Button6_Click(object sender, EventArgs e) { WczytajListęDnia((Control)sender); }
        private void Button7_Click(object sender, EventArgs e) { WczytajListęDnia((Control)sender); }
        private void Button8_Click(object sender, EventArgs e) { WczytajListęDnia((Control)sender); }
        private void Button9_Click(object sender, EventArgs e) { WczytajListęDnia((Control)sender); }
        private void Button10_Click(object sender, EventArgs e) { WczytajListęDnia((Control)sender); }
        private void Button11_Click(object sender, EventArgs e) { WczytajListęDnia((Control)sender); }
        private void Button12_Click(object sender, EventArgs e) { WczytajListęDnia((Control)sender); }
        private void Button13_Click(object sender, EventArgs e) { WczytajListęDnia((Control)sender); }
        private void Button14_Click(object sender, EventArgs e) { WczytajListęDnia((Control)sender); }
        private void Button15_Click(object sender, EventArgs e) { WczytajListęDnia((Control)sender); }
        private void Button16_Click(object sender, EventArgs e) { WczytajListęDnia((Control)sender); }
        private void Button17_Click(object sender, EventArgs e) { WczytajListęDnia((Control)sender); }
        private void Button18_Click(object sender, EventArgs e) { WczytajListęDnia((Control)sender); }
        private void Button19_Click(object sender, EventArgs e) { WczytajListęDnia((Control)sender); }
        private void Button20_Click(object sender, EventArgs e) { WczytajListęDnia((Control)sender); }
        private void Button21_Click(object sender, EventArgs e) { WczytajListęDnia((Control)sender); }
        private void Button22_Click(object sender, EventArgs e) { WczytajListęDnia((Control)sender); }
        private void Button23_Click(object sender, EventArgs e) { WczytajListęDnia((Control)sender); }
        private void Button24_Click(object sender, EventArgs e) { WczytajListęDnia((Control)sender); }
        private void Button25_Click(object sender, EventArgs e) { WczytajListęDnia((Control)sender); }
        private void Button26_Click(object sender, EventArgs e) { WczytajListęDnia((Control)sender); }
        private void Button27_Click(object sender, EventArgs e) { WczytajListęDnia((Control)sender); }
        private void Button28_Click(object sender, EventArgs e) { WczytajListęDnia((Control)sender); }
        private void Button29_Click(object sender, EventArgs e) { WczytajListęDnia((Control)sender); }
        private void Button30_Click(object sender, EventArgs e) { WczytajListęDnia((Control)sender); }
        private void Button31_Click(object sender, EventArgs e) { WczytajListęDnia((Control)sender); }
        private void Button32_Click(object sender, EventArgs e) { WczytajListęDnia((Control)sender); }
        private void Button33_Click(object sender, EventArgs e) { WczytajListęDnia((Control)sender); }
        private void Button34_Click(object sender, EventArgs e) { WczytajListęDnia((Control)sender); }
        private void Button35_Click(object sender, EventArgs e) { WczytajListęDnia((Control)sender); }
        private void Button36_Click(object sender, EventArgs e) { WczytajListęDnia((Control)sender); }
        private void Button37_Click(object sender, EventArgs e) { WczytajListęDnia((Control)sender); }
        private void Button38_Click(object sender, EventArgs e) { WczytajListęDnia((Control)sender); }
        private void Button39_Click(object sender, EventArgs e) { WczytajListęDnia((Control)sender); }
        private void Button40_Click(object sender, EventArgs e) { WczytajListęDnia((Control)sender); }
        private void Button41_Click(object sender, EventArgs e) { WczytajListęDnia((Control)sender); }
        private void Button42_Click(object sender, EventArgs e) { WczytajListęDnia((Control)sender); }
        #endregion

        private void Form3_FormClosed(object sender, FormClosedEventArgs e)
        {
            Aplikacja_dyplomowa.Form1.Enabled = true;
            Aplikacja_dyplomowa.Form1.Focus();
        }
    }
}