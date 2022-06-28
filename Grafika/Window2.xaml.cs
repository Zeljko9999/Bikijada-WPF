using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Organizacija;

namespace Organizator
{

    public partial class Window2 : Window
    {

        public Window2()
        {
            InitializeComponent();

        }

        private void klad_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void upl_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void bik_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void vl_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void btnOkl1_Click(object sender, RoutedEventArgs e)

            
        {
            try
            {
                Bik bik1 = new Bik()
            {
                Ime = bik.Text,
            };

            Vlasnik vlasnik1 = new Vlasnik()
            {
                Ime = vl.Text,
            };

            Oklada oklada = new Oklada()
            {
                Ime = klad.Text,
                Iznos = float.Parse(upl.Text),
                bik = bik1,
                vlasnik = vlasnik1, 

            };
                 if (oklada.Ime == "" | oklada.Iznos == 0 | bik1.Ime == "" | vlasnik1.Ime == "")
                    output1.AppendText("Ne smije biti praznih polja! \n");
                 else Oklada.UbaciOkladu(oklada); 
                

            }
            catch (Exception x)
            {
                output1.AppendText(x.Message + "\n");
            }
        }

        private void output1_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void btnOkl2_Click(object sender, RoutedEventArgs e)
        {
            int oklIDmax = Oklada.DohvatioklIDmax();

            List<Oklada> oklade = new List<Oklada>();
            oklade = Oklada.DohvatiOklade(oklIDmax);

            output1.AppendText("\nID oklade      Kladioničar           Iznos(kn)            Bik           Vlasnik     \n");
            foreach (Oklada o in oklade) 
            {
                output1.AppendText(o.ID + "                  " + o.Ime + "            " + o.Iznos + "               " + o.bik.Ime + "        " + o.vlasnik.Ime + "\n");
            }
        }

        private void bikokl_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void btnOkl3_Click(object sender, RoutedEventArgs e)
        {
           double iznos = Oklada.DohvatiUkupniIznos(bikokl.Text, vlokl.Text);
            output1.AppendText("\nBik           Vlasnik           Ukupni Iznos (kn) \n");
            output1.AppendText(bikokl.Text +"       " + vlokl.Text + "          " + iznos + "\n");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
         int brbIDmax = Borba.DohvatibrbIDmax();
         int brbIDmin = Borba.DohvatibrbIDmin();

         List<BorbaPom> borbe = new List<BorbaPom>();
         borbe = Borba.DohvatiBorbu(brbIDmin, brbIDmax);

            output.AppendText("\nID borbe    1.Bik             Vlasnik1                 Mjesto1                  2.Bik                  Vlasnik2                   Mjesto2           Kategorija                 Vrijeme  \n");
            foreach (BorbaPom b in borbe)
            {
                output.AppendText(b.bID + "                " + b.ime1 + "           " + b.vlasnik1 + "           " + b.mjesto1 + "             " + b.ime2 + "             " + b.vlasnik2 + "             " + b.mjesto2 + "             " + b.kategorija + "                      " + b.vrijeme + "\n");
            }

        }

        private void output_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void pivacVl_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void pivac_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void pivac1_Click(object sender, RoutedEventArgs e)
        {
            int pvcIDmax = Pivac.DohvatipvcIDmax();

            List<Pivac> pivci = new List<Pivac>();
            pivci = Pivac.DohvatiPivce(pvcIDmax);

            output.AppendText("\nID pivca      Pivac          Vlasnik     \n");
            foreach (Pivac p in pivci)
            {
                output.AppendText(p.ID + "                " + p.Ime + "             " + p.Vlasnik + "\n");
            }
        }

        private void pivac2_Click(object sender, RoutedEventArgs e)
        {
            try
            {
             
                Pivac pivac1 = new Pivac()
                {
                    Ime = pivac.Text,
                    Vlasnik = pivacVl.Text,

                };

                if (pivac1.Ime == "" | pivac1.Vlasnik == "")
                    output1.AppendText("Ne smije biti praznih polja! \n");
                else Pivac.UbaciPivca(pivac1);


            }
            catch (Exception x)
            {
                output1.AppendText(x.Message + "\n");
            }
        }
        
        private void imageBik_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }

        private void Image_Click(object sender, RoutedEventArgs e)
        {
            string slika = Bik.DohvatiSliku(imageBik.Text);
            if (slika == "Golub")
            {
                var secondWindow = new Window1();
                secondWindow.Show();
            }
            if (slika == "Brizan")
            {
                var thirdWindow = new Window11();
                thirdWindow.Show();
            }
            if (slika == "Vakciner")
            {
                var ffWindow = new Window21();
                ffWindow.Show();
            }
           
        }

    }
}
//{ Binding Source}