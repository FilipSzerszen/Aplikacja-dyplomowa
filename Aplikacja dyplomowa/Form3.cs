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
        private int dzień = DateTime.Today.Day;
        private int miesiąc = DateTime.Today.Month;
        private int rok = DateTime.Today.Year;


        public Form3()
        {
            InitializeComponent();

            LBLaktData.Text = DataText(DateTime.Today, true);
            LBLwybranaData.Text = DataText(DateTime.Today, false);
            AktualizujKalendarz(DateTime.Today);
        }
        private string DataText(DateTime data, bool czyPełna)
        {
            string dataString = "";
            if (czyPełna) dataString = data.Day.ToString();
            switch (data.Month)
            {
                case 1: _ = czyPełna ? dataString = " stycznia" : dataString = " styczeń"; break;
                case 2: _ = czyPełna ? dataString += " lutego" : dataString = " luty"; break;
                case 3: _ = czyPełna ? dataString += " marca" : dataString = " marzec"; break;
                case 4: _ = czyPełna ? dataString += " kwietnia" : dataString += " kwiecień"; break;
                case 5: _ = czyPełna ? dataString += " maja" : dataString = " maj"; break;
                case 6: _ = czyPełna ? dataString += " czerwca" : dataString = " czerwiec"; break;
                case 7: _ = czyPełna ? dataString += " lipca" : dataString = " lipiec"; break;
                case 8: _ = czyPełna ? dataString += " sierpnia" : dataString = " sierpień"; break;
                case 9: _ = czyPełna ? dataString += " września" : dataString = " wrzesień"; break;
                case 10: _ = czyPełna ? dataString += " października" : dataString = " październik"; break;
                case 11: _ = czyPełna ? dataString += " listopada" : dataString = " listopad"; break;
                case 12: _ = czyPełna ? dataString += " grudnia" : dataString = " grudzień"; break;
            }
            dataString += " " + data.Year.ToString();
            return dataString;
        }
        private void AktualizujKalendarz(DateTime data)
        {

            int dzieńTygodniaStart = (int)((new DateTime(data.Year, data.Month, 1)).DayOfWeek);
            if (dzieńTygodniaStart == 0) dzieńTygodniaStart = 7;    //niedzielom nadaj index 7, a nie 0
            if (dzieńTygodniaStart == 1) dzieńTygodniaStart = 8;    //jeśli idealnie wypadł od pn to zacznij tydzień później (wzgl. wizualne)   

            tb.Text = dzieńTygodniaStart.ToString();

            var Buttons = this.gBox.Controls.OfType<Button>();
            for (int i = 1; i < 43; i++)
            {
                var b = Buttons.Where(p => p.Name == "button" + i).FirstOrDefault();

                if (i >= dzieńTygodniaStart && i <= dzieńTygodniaStart - 1 + DateTime.DaysInMonth(data.Year, data.Month))
                {   //pokaż dni aktualnego miesiąca
                    if(new DateTime(rok, miesiąc,i - dzieńTygodniaStart+1) <= DateTime.Today) b.Enabled = true;
                    else b.Enabled = false;
                    b.Text = (i - dzieńTygodniaStart + 1).ToString();
                }
                else if (i < dzieńTygodniaStart)
                {   //wyszarz początek kalendarza
                    b.Enabled = false;
                    b.Text = (DateTime.DaysInMonth(data.Month == 1 ? 12 : data.Year - 1, data.Month == 1 ? 12 : data.Month - 1) - dzieńTygodniaStart + i + 1).ToString();
                }
                else
                {   ////wyszarz reszte czyli koniec kalendarza
                    b.Enabled = false;
                    b.Text = (i - dzieńTygodniaStart + 1 - DateTime.DaysInMonth(data.Year, data.Month)).ToString();
                }
            }
            LBLwybranaData.Text = DataText(data, false);
        }
        private void button43_Click(object sender, EventArgs e)
        {
            AktualizujKalendarz(DateTime.Today);
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
    }
}