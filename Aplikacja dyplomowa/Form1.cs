using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.IO;
using System.Windows.Forms.DataVisualization.Charting;


struct Dane
{
    public int sekunda;
    public float predkosc;
    public byte korba;
    public byte hl;
    public byte hp;
    public byte przelozenie_p;
    public byte przelozenie_t;
}

namespace Aplikacja_dyplomowa
{
    public partial class Aplikacja_dyplomowa : Form
    {
        private Dane d;
        private List<Dane> lista = new List<Dane>();

        public static Aplikacja_dyplomowa Form1;
        public TextBox tb;
        public static string port_com = "";

        public string sciezka_pliku;
        string linia;
        bool wgrane = false;

        byte[,] Tablica_przelorzenia = new byte[3, 9];
        byte[] Tablica_kadencja;
        float[] Tablica_przyspieszenie;

        int temp;
        float lpredkosc = 0;
        float lpredkoscmax = 0;
        float lpredkoscsr = 0;
        float lGmax = 0;
        float lGmin = 0;
        byte lKmax = 0;
        ushort lKsr = 0;
        ushort lObrK = 0;
        byte lUlub = 0;
        ushort lHamL = 0;
        ushort lHamP = 0;


        


        public void Wypełnij_wykresy()
        {
            

            Czysc_t_przelozenia();
            wypelnij_t_przelozenia();
            wypelnij_t_kadencji();
            wypelnij_t_przyspieszenia();

            chPP.Series["chPP"].Points.Clear();
            chPP.Series["chPP"].Points.AddXY("1", (Tablica_przelorzenia[0, 0] + Tablica_przelorzenia[0, 1] + Tablica_przelorzenia[0, 2] + Tablica_przelorzenia[0, 3] + Tablica_przelorzenia[0, 4] + Tablica_przelorzenia[0, 5] + Tablica_przelorzenia[0, 6] + Tablica_przelorzenia[0, 7] + Tablica_przelorzenia[0, 8]));
            chPP.Series["chPP"].Points.AddXY("2", (Tablica_przelorzenia[1, 0] + Tablica_przelorzenia[1, 1] + Tablica_przelorzenia[1, 2] + Tablica_przelorzenia[1, 3] + Tablica_przelorzenia[1, 4] + Tablica_przelorzenia[1, 5] + Tablica_przelorzenia[1, 6] + Tablica_przelorzenia[1, 7] + Tablica_przelorzenia[1, 8]));
            chPP.Series["chPP"].Points.AddXY("3", (Tablica_przelorzenia[2, 0] + Tablica_przelorzenia[2, 1] + Tablica_przelorzenia[2, 2] + Tablica_przelorzenia[2, 3] + Tablica_przelorzenia[2, 4] + Tablica_przelorzenia[2, 5] + Tablica_przelorzenia[2, 6] + Tablica_przelorzenia[2, 7] + Tablica_przelorzenia[2, 8]));

            chPT.Series["chPT"].Points.Clear();
            chPT.Series["chPT"].Points.AddXY("1", Tablica_przelorzenia[0, 0] + Tablica_przelorzenia[1, 0] + Tablica_przelorzenia[2, 0]);
            chPT.Series["chPT"].Points.AddXY("2", Tablica_przelorzenia[0, 1] + Tablica_przelorzenia[1, 1] + Tablica_przelorzenia[2, 1]);
            chPT.Series["chPT"].Points.AddXY("3", Tablica_przelorzenia[0, 2] + Tablica_przelorzenia[1, 2] + Tablica_przelorzenia[2, 2]);
            chPT.Series["chPT"].Points.AddXY("4", Tablica_przelorzenia[0, 3] + Tablica_przelorzenia[1, 3] + Tablica_przelorzenia[2, 3]);
            chPT.Series["chPT"].Points.AddXY("5", Tablica_przelorzenia[0, 4] + Tablica_przelorzenia[1, 4] + Tablica_przelorzenia[2, 4]);
            chPT.Series["chPT"].Points.AddXY("6", Tablica_przelorzenia[0, 5] + Tablica_przelorzenia[1, 5] + Tablica_przelorzenia[2, 5]);
            chPT.Series["chPT"].Points.AddXY("7", Tablica_przelorzenia[0, 6] + Tablica_przelorzenia[1, 6] + Tablica_przelorzenia[2, 6]);
            chPT.Series["chPT"].Points.AddXY("8", Tablica_przelorzenia[0, 7] + Tablica_przelorzenia[1, 7] + Tablica_przelorzenia[2, 7]);
            chPT.Series["chPT"].Points.AddXY("9", Tablica_przelorzenia[0, 8] + Tablica_przelorzenia[1, 8] + Tablica_przelorzenia[2, 8]);

            chV.Series["chV"].Points.Clear();
            chK.Series["chK"].Points.Clear();
            chHL.Series["chHL"].Points.Clear();
            chHP.Series["chHP"].Points.Clear();
            chG.Series["chG"].Points.Clear();

            

            lpredkosc = 0;
             lpredkoscmax = 0;
             lpredkoscsr = 0;
             lGmax = 0;
             lGmin = 0;
             lKmax = 0;
             lKsr = 0;
             lObrK = 0;
             lUlub = 0;
             lHamL = 0;
             lHamP = 0;


            for (int i = 0; i < lista.Count; i++)
            {
                chV.Series["chV"].Points.AddXY(lista[i].sekunda, lista[i].predkosc);
                chK.Series["chK"].Points.AddXY(lista[i].sekunda, Tablica_kadencja[i]);
                chHL.Series["chHL"].Points.AddXY(lista[i].sekunda, lista[i].hl);
                chHP.Series["chHP"].Points.AddXY(lista[i].sekunda, lista[i].hp);
                chG.Series["chG"].Points.AddXY(lista[i].sekunda, Tablica_przyspieszenie[i]);

                lpredkosc += lista[i].predkosc / 3600;                                        //dystans
                if (lista[i].predkosc > lpredkoscmax) lpredkoscmax = lista[i].predkosc;       //predkosc max
                lpredkoscsr += lista[i].predkosc;                                             //predkosc sr
                if (Tablica_przyspieszenie[i] > lGmax) lGmax = Tablica_przyspieszenie[i];     //przyspieszenie max
                if (Tablica_przyspieszenie[i] < lGmin) lGmin = Tablica_przyspieszenie[i];     //opóźnienie max
                if (Tablica_kadencja[i] > lKmax) lKmax = Tablica_kadencja[i];                 //kadencja max
                lKsr += Tablica_kadencja[i];                                                  //kadencja sr
                lObrK += lista[i].korba;                                                      //obrotów korby
                lHamL += lista[i].hl;                                                         //liczba użyć hamulca lewego
                lHamP += lista[i].hp;                                                         //liczba użyć hamulca prawego
            }

            labDyst.Text = String.Format("{0:N2}", lpredkosc) + " km";
            labVmax.Text = String.Format("{0:N1}", lpredkoscmax) + " km/h";
            labVsr.Text = String.Format("{0:N1}", lpredkoscsr / lista.Count) + " km/h";
            labGmax.Text = String.Format("{0:N1}", lGmax) + " G";
            labGmin.Text = String.Format("{0:N1}", lGmin) + " G";
            labKmax.Text = lKmax.ToString() + " obr/min";
            labKsr.Text = String.Format("{0:N0}", lKsr / lista.Count) + " obr/min";
            labObr.Text = lObrK.ToString();
            labHamL.Text = lHamL.ToString();
            labHamP.Text = lHamP.ToString();

            //ulubione przełożenie
            for (int x = 0; x < 3; x++)
            {
                for (int y = 0; y < 9; y++)
                {
                    if (Tablica_przelorzenia[x, y] > lUlub)
                    {
                        lUlub = Tablica_przelorzenia[x, y];
                        labUlubBieg.Text = (x + 1).ToString() + "x" + (y + 1).ToString();
                    }
                }
            }

            //czas
            labCzas.Text = TimeSpan.FromSeconds(lista.Count).ToString();
            Ile_rekordów();

            chV.Visible = true;
            chG.Visible = true;
            chK.Visible = true;
            chHL.Visible = true;
            chHP.Visible = true;
            chPP.Visible = true;
            chPT.Visible = true;
            Form1.BackgroundImage = null;
            PanelTylny.Visible = true;

            //chV.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
            //chV.ChartAreas[0].CursorX.AutoScroll = true;
            //chV.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;

            //chG.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
            //chG.ChartAreas[0].CursorX.AutoScroll = true;
            //chG.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;



        }
        void Ile_rekordów()
        {
            StripStatus.Text = "Rekordów: " + lista.Count.ToString() + " (" + TimeSpan.FromSeconds(lista.Count).ToString() + ") ["+ Path.GetFileName(sciezka_pliku) + "]";
        }

        void wypelnij_t_przyspieszenia()
        {
            Tablica_przyspieszenie = new float[lista.Count];

            Tablica_przyspieszenie[0] = 0;
            for (int i = 1; i < lista.Count; i++)
            {
                Tablica_przyspieszenie[i] = (float)((lista[i].predkosc - lista[i - 1].predkosc) / 35.316); //    /(3,6*9,81)  =>  /35,316
            }
        }

        void Czysc_t_przelozenia()
        {
            for (int x = 0; x < 3; x++)
            {
                for (int y = 0; y < 9; y++)
                {
                    Tablica_przelorzenia[x, y] = 0;
                }
            }
        }

        void wypelnij_t_przelozenia()
        {
            for (int i = 0; i < lista.Count; i++)
            {
                Tablica_przelorzenia[lista[i].przelozenie_p - 1, lista[i].przelozenie_t - 1] += 1;
            }
        }

        void wypelnij_t_kadencji()
        {
            Tablica_kadencja = new byte[lista.Count];
            int licznik = 0;
            int temp = 0;

            for (int i = 0; i < lista.Count; i++)
            {
                if (lista[i].predkosc == 0)
                {
                    Tablica_kadencja[i] = 0;
                    licznik = 0;
                    temp = 0;
                }
                else
                {
                    if (licznik < 59)
                    {
                        temp += lista[i].korba;
                        Tablica_kadencja[i] = (byte)(temp * 60 / (1 + licznik));
                    }
                    else
                    {
                        Tablica_kadencja[i] = (byte)(Tablica_kadencja[i - 1] + lista[i].korba - lista[i - 59].korba);
                    }
                }
                licznik++;
            }
        }


        public Aplikacja_dyplomowa()
        {
            InitializeComponent();
            Form1 = this;
            tb = tBoxTemp;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cBoxPortCom.Items.Clear();
            string[] ports = SerialPort.GetPortNames();
            cBoxPortCom.Items.AddRange(ports);
            Form1.SetDesktopLocation((Screen.PrimaryScreen.Bounds.Width - Form1.Size.Width) / 2, (Screen.PrimaryScreen.Bounds.Height - Form1.Size.Height) / 2);

        }

        void Otworz_port()
        {
            try
            {
                serialPort1.DtrEnable = false;
                serialPort1.RtsEnable = false;
                serialPort1.PortName = cBoxPortCom.Text;
                serialPort1.BaudRate = 38400;
                serialPort1.DataBits = 8;
                serialPort1.StopBits = (StopBits)1;
                serialPort1.Parity = Parity.None;
                serialPort1.Open();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                rBtnON.Enabled = false;
                lblStatus.Text = "OFF";
            }
        }

        public void Zamknij_port()
        {
            if (serialPort1.IsOpen)
            {
                serialPort1.Close();
            }
        }

        private void zToolStripMenuItem_Click(object sender, EventArgs e)
        {


            //'I  |  I  |  HL |  HP |  K  |  K  |  V  |  V          
            //'inf       hamulec L  P  Obr. korby
            //'V  |  V  |  V  |  V  |  V  |  V  |  V  |  V
            //'prędkość od 4 do 63km/h (10 bitów 349 - 34.9km/h)

            //'I  |  I  |  P  |  P  |  P  |  P  |  P  |  P
            //'inf                przelozenie max 3x9 - 39

            //'inf:
            //'00 - hamulce, obroty korby, prędkość
            //'01 - przełożenie
            //'10 - niewykorzystane
            //'11 - niewykorzystane

            openFileDialog1.InitialDirectory = "";
            openFileDialog1.Title = "Otwórz plik programu";
            openFileDialog1.Filter = "Plik analizatora aktywności rowerowej |*.aar|Wszystkie pliki|*.*";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                sciezka_pliku = openFileDialog1.FileName;
                
                try
                {
                    StreamReader sr = new StreamReader(sciezka_pliku);
                    tBoxTemp.Text = sr.ReadToEnd();
                    sr.Close();
                    Konwertuj_dane();
                    Wypełnij_wykresy();
                    
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


            }
        }
        //Format struktury:
        // V
        // K
        // HL
        // HP
        // PP
        // PT
        public void Konwertuj_dane()
        {
            byte temp;
            byte temp2;
            int xk;
            int x = 0;

            if (lista.Count != 0) lista.Clear();

            linia = tBoxTemp.Lines[x++];
            xk = Convert.ToInt32(linia) * 256;
            linia = tBoxTemp.Lines[x++];
            xk += Convert.ToInt32(linia);

            d.sekunda = 0;
            while (x < xk + 2)
            {

                linia = tBoxTemp.Lines[x++];
                
                temp = Convert.ToByte(linia);

                //HL
                temp2 = (byte)(temp & 0b00100000);
                d.hl = (byte)(temp2 >> 5);

                //HP
                temp2 = (byte)(temp & 0b00010000);
                d.hp = (byte)(temp2 >> 4);

                //Korba
                temp2 = (byte)(temp & 0b00001100);
                d.korba = (byte)(temp2 >> 2);

                //Prędkość
                temp2 = (byte)(temp & 0b00000011);
                temp2 = (byte)(temp2 * 256);

                linia = tBoxTemp.Lines[x++];

                temp = Convert.ToByte(linia);
                temp2 += temp;
                d.predkosc = (float)temp2 / 10;

                if (x < xk + 1)               
                {
                    linia = tBoxTemp.Lines[x++];
                    temp = Convert.ToByte(linia);

                    //Przełożenie
                    if ((temp & 0b11000000) == 64)
                    {
                        temp2 = (byte)(temp - 64);
                        d.przelozenie_p = (byte)(temp2 / 10);
                        temp2 = (byte)(temp - 64);
                        d.przelozenie_t = (byte)(temp2 % 10);
                    }
                    else
                    {
                        x--;
                    }
                }

                lista.Add(d);
                d.sekunda++;
            }


        }

        private void zapiszPlikToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tBoxTemp.Text!="")
            {
                saveFileDialog1.InitialDirectory = "";
                saveFileDialog1.Title = "Otwórz plik programu";
                saveFileDialog1.Filter = "Plik analizatora aktywności rowerowej |*.aar";
                saveFileDialog1.FileName = "nowy trip";

                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    sciezka_pliku = saveFileDialog1.FileName;
                    try
                    {
                        StreamWriter sw = new StreamWriter(sciezka_pliku);
                        sw.Write(tBoxTemp.Text);
                        sw.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Błąd: " + ex.Message);
                        return;
                    }
                }
            }
            else
            {
                MessageBox.Show("Nie można zapisać pustego pliku.\r\nWczytaj lub zaimportuj dane z urządzenia.");
            }
        }

        private void importujDaneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form4 child = new Form4();
            child.Show();
            child.SetDesktopLocation(DesktopLocation.X + (Size.Width / 2) - child.Size.Width / 2, DesktopLocation.Y + (Size.Height / 2) - child.Size.Height / 2);

        }

        private void Otworz_port_cBox(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen == false)
            {
                Otworz_port();
                if (serialPort1.IsOpen)
                {
                    port_com = cBoxPortCom.Text;
                    rBtnON.Enabled = true;
                    lblStatus.Text = "ON";
                }
            }
            Zamknij_port();
        }


        void Wartosc_chwilowa(int x, int y)
        {
            if (wgrane)
            {
                Point Punkt_myszy = new Point(x, y);




                chV.ChartAreas[0].CursorX.Interval = 1;
                chV.ChartAreas[0].CursorY.Interval = 0;

                chV.ChartAreas[0].CursorX.SetCursorPixelPosition(Punkt_myszy, true);
                //chV.ChartAreas[0].CursorY.SetCursorPixelPosition(Punkt_myszy, true);

                chG.ChartAreas[0].CursorX.SetCursorPixelPosition(Punkt_myszy, true);
                chK.ChartAreas[0].CursorX.SetCursorPixelPosition(Punkt_myszy, true);
                chHL.ChartAreas[0].CursorX.SetCursorPixelPosition(Punkt_myszy, true);
                chHP.ChartAreas[0].CursorX.SetCursorPixelPosition(Punkt_myszy, true);

                //chV.ChartAreas[0].CursorY.SetCursorPixelPosition(Punkt_myszy, true);

                //textx.Text = "x: " + String.Format("{0:N0}", chV.ChartAreas[0].AxisX.PixelPositionToValue(e.X));
                //texty.Text = "y: " + String.Format("{0:N1}", chV.ChartAreas[0].AxisY.PixelPositionToValue(e.Y));

                temp = (int)(chV.ChartAreas[0].AxisX.PixelPositionToValue(x));

                //    temp3 = (int)(chV.ChartAreas[0].AxisX.PixelPositionToValue(0));
                //    MessageBox.Show("Exception: " + ex.Message);
                //}
                if (temp < 0) temp = 0;
                if (temp >= lista.Count) temp = lista.Count - 1;

                StripStatus.Text = TimeSpan.FromSeconds(temp).ToString() + "  Prędkość: " + String.Format("{0:N1}", lista[temp].predkosc) + 
                    "km/h  Kadencja: " + Tablica_kadencja[temp].ToString() + "obr/min  Przyśpieszenie: " + String.Format("{0:N1}", Tablica_przyspieszenie[temp]) + "G  ";
                if (lista[temp].hl == 1) { StripStatus.Text += "Hamulec przedni: ON  "; } else { StripStatus.Text += "Hamulec przedni: OFF  "; }
                if (lista[temp].hp == 1) { StripStatus.Text += "Hamulec tylny: ON"; } else { StripStatus.Text += "Hamulec tylny: OFF"; }


                //HitTestResult wynik = chV.HitTest(x, y);

                //if (wynik.PointIndex > -1 && wynik.ChartArea != null)
                //{
                //    punktx.Text = String.Format("{0:N1}", wynik.Series.Points[wynik.PointIndex].XValue);
                //    punkty.Text = String.Format("{0:N1}", wynik.Series.Points[wynik.PointIndex].YValues[0]);
            }
        }
        private void chV_MouseMove(object sender, MouseEventArgs e)
        {
            Wartosc_chwilowa(e.X, e.Y);
        }

        private void chK_MouseMove(object sender, MouseEventArgs e)
        {
            Wartosc_chwilowa(e.X, e.Y);
        }

        private void chG_MouseMove(object sender, MouseEventArgs e)
        {
            Wartosc_chwilowa(e.X, e.Y);
        }

        private void chHP_MouseMove(object sender, MouseEventArgs e)
        {
            Wartosc_chwilowa(e.X, e.Y);
        }

        private void chHL_MouseMove(object sender, MouseEventArgs e)
        {
            Wartosc_chwilowa(e.X, e.Y);
        }

        private void chV_MouseLeave(object sender, EventArgs e)
        {
            Ile_rekordów();
            wgrane = true;
        }

        private void chK_MouseLeave(object sender, EventArgs e)
        {
            Ile_rekordów();
        }

        private void chG_MouseLeave(object sender, EventArgs e)
        {
            Ile_rekordów();
        }

        private void chHP_MouseLeave(object sender, EventArgs e)
        {
            Ile_rekordów();
        }

        private void chHL_MouseLeave(object sender, EventArgs e)
        {
            Ile_rekordów();
        }

    }
}