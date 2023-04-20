using System;
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

        private void Form6_Shown(object sender, EventArgs e)
        {
            string tekst = "Hej!\r" +
                "Jestem absolwentem Politechniki Wrocławskiej wydziału\r" +
                "Budownictwa oraz Wrocławskiej Wyższej Szkoły Informatyki\r" +
                "Stosowanej Horyzont wydziału Programowanie. Aplikacja\r" +
                "powstała z połączenia trzech głównych pasji (rowerowego\r" +
                "zacięcia, elektroniki, programowania) oraz na potrzeby pracy\r" +
                "dyplomowej. Projekt jest wciąż rozwijany... \r\r" +

                @"Mój github: https://github.com/FilipSzerszen";

            richTextBox1.Text = "";
            pictureBox1.Refresh();
            for (int i = 0; i < tekst.Length; i++)
            {
                richTextBox1.Text += tekst[i];
                System.Threading.Thread.Sleep(1);
                richTextBox1.Refresh();
            }
        }

        private void richTextBox1_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(e.LinkText);
        }
    }
}
