using System;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Data.SqlClient;
using Organizator;
using Organizacija;
using System.Collections.Generic;

namespace Organizator
{
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            
        }


        private void vlBox1_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void mjBox1_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void katBox1_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void bikBox1_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void dateBox1_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void output_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void vlBox2_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void mjBox2_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void bikBox2_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void btn1_Click(object sender, RoutedEventArgs e)
        {
          
          try
            {
                BorbaPom borba = new BorbaPom()
                {
                 
                 vlasnik1 = vlBox1.Text,
                 mjesto1 = mjBox1.Text,
                 ime1 = bikBox1.Text,
                 kategorija = katBox1.Text,
                 vrijeme = DateTime.Parse(dateBox1.Text),
                 vlasnik2 = vlBox2.Text,
                 mjesto2 = mjBox2.Text,
                 ime2 = bikBox2.Text,

                 };

                
                if (borba.vlasnik1 == "" | borba.mjesto1 == "" | borba.ime1 == "" | borba.kategorija == "" | borba.vlasnik2 == "" | borba.mjesto2 == "" | borba.ime2 == "")
                   output.AppendText("Ne smije biti praznih polja! \n");
                else Borba.UbaciBorbu(borba);

            }
             catch (Exception x)
             {
                 output.AppendText(x.Message + "\n");
             }

        } 
        
        private void btn2_Click(object sender, RoutedEventArgs e)
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

        private void btn4_Click(object sender, RoutedEventArgs e)
        {
            var secondWindow = new Window2();
            secondWindow.Show();
        }

        private void btn3_Click(object sender, RoutedEventArgs e)
        {
            int id = int.Parse(izbr.Text);
            Borba.IzbrišiBorbu(id);
        }

        private void izbr_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }

}