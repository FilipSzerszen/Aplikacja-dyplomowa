using System;
using System.IO.Ports;
using System.Windows.Forms;

namespace Aplikacja_dyplomowa
{
    public partial class Form2 : Form
    {
        public void Zamknij_port()
        {
            if (serialPort1.IsOpen)
            {
                serialPort1.Close();
            }
        }

        public void Otworz_port()
        {
            try
            {
                serialPort1.DtrEnable = false;
                serialPort1.RtsEnable = false;
                serialPort1.PortName = Aplikacja_dyplomowa.port_com;
                serialPort1.BaudRate = 38400;
                serialPort1.DataBits = 8;
                serialPort1.StopBits = (StopBits)1;
                serialPort1.Parity = Parity.None;
                serialPort1.Open();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        public void Zgraj_dane()
        {
            byte dana;
            byte czyok;
            int adres = 0;

            if (serialPort1.IsOpen == false)
            {
                Otworz_port();
            }
            if (serialPort1.IsOpen)
            {
                serialPort1.Write(new byte[] { 0b11001100 }, 0, 1);
                while (true)
                {
                    if (serialPort1.BytesToRead > 0)
                    {
                        if ((byte)serialPort1.ReadByte() == 51)
                        {
                            serialPort1.Write(new byte[] { 0b01010101 }, 0, 1);
                            break;
                        }
                    }
                }

                Aplikacja_dyplomowa.Form1.tb.Text = "";
                for (int i = 1; i > -1; i--)
                {
                    while (true)
                    {
                        if (serialPort1.BytesToRead > 0)
                        {
                            dana = (byte)serialPort1.ReadByte();
                            Aplikacja_dyplomowa.Form1.tb.Text += dana.ToString() + "\r\n";
                            serialPort1.Write(new byte[] { dana }, 0, 1);
                            break;
                        }
                    }
                    while (true)
                    {
                        if (serialPort1.BytesToRead > 0)
                        {
                            czyok = (byte)serialPort1.ReadByte();
                            if (czyok != 1)
                            {
                                i++;
                            }
                            else
                            {
                                adres += dana * (int)Math.Pow(256, i);
                            }
                            break;
                        }
                    }
                }



                for (int i = 0; i <= adres; i++)
                {
                    progBar.Value = 100 * i / adres;
                    while (true)
                    {
                        if (serialPort1.BytesToRead > 0)
                        {
                            dana = (byte)serialPort1.ReadByte();
                            Aplikacja_dyplomowa.Form1.tb.Text += dana.ToString() + "\r\n";
                            serialPort1.Write(new byte[] { dana }, 0, 1);
                            break;
                        }
                    }
                    while (true)
                    {
                        if (serialPort1.BytesToRead > 0)
                        {
                            dana = (byte)serialPort1.ReadByte();
                            if (dana != 1)
                            {
                                i--;
                            }
                            break;
                        }
                    }
                }
            }
            Zamknij_port();
        }

        public Form2()
        {
            InitializeComponent();
        }

        private void BtnAnuluj_Click(object sender, EventArgs e)
        {
            this.Close();
        }
             
        private void Form2_Shown(object sender, EventArgs e)
        {
            Zgraj_dane();
            Aplikacja_dyplomowa.Form1.Konwertuj_dane();
            Aplikacja_dyplomowa.Form1.Wypełnij_wykresy();
            this.Close();
            Aplikacja_dyplomowa.Form1.Enabled = true;
            Aplikacja_dyplomowa.Form1.Focus();
        }
    }
}
