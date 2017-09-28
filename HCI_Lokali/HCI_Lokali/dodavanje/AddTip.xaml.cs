using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using System.Linq;

namespace HCI_Lokali
{
    public partial class AddTip : Window
    {
        public MainWindow parent1;
        private Tip tip = new Tip();


        public AddTip(MainWindow prozor)
        {
            InitializeComponent();
            this.parent1 = prozor;
            this.DataContext = tip;
        }

        private void izlaz_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void u_redu_Click(object sender, RoutedEventArgs e)
        {
            if (!validacija1() && !validacija2())
            {
                return;
            }


            Boolean pr = true;
            if (string.IsNullOrWhiteSpace(oznakat.Text)) pr = false;
            if (string.IsNullOrWhiteSpace(imet.Text)) pr = false;
            if (string.IsNullOrWhiteSpace(opist.Text)) pr = false;
            if (image.Source == null) pr = false;    // provera za ikonicu

            if (pr == true)
            {
                bool indikator_oznake = false;

                //provera jedinstvenosti oznake!!!
                foreach (Tip t in parent1.tip_list)
                {
                    if (oznakat.Text.Equals(t.oznaka))
                    {
                        MessageBox.Show("Polje oznaka mora biti jedinstveno!", "Upozorenje!");
                        indikator_oznake = true;
                    }
                }

                if (indikator_oznake == false)
                {
                    parent1.tip_list.Add(tip);
                    this.Close();
                }

            }

            else
            {
                MessageBox.Show("Sva polja moraju biti popunjena!", "Upozorenje!");
            }


        }

        //ucitavanje ikonice
        private void ucitavanje_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dg = new OpenFileDialog();

            dg.Title = "Otvori sliku";
            dg.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";

            if (dg.ShowDialog() == DialogResult.Equals("OK"))

                image = new Image();

            tip.slika = dg.FileName;
            image.Source = new BitmapImage(new Uri(tip.slika));

        }

        private bool validacija1()
        {
            string name1 = oznakat.Text.Replace(" ", "");
            if (!name1.All(Char.IsLetterOrDigit))
            {

                MessageBox.Show("Greska, dozvoljen je unos samo slova i brojeva.", "Greska", MessageBoxButton.OK, MessageBoxImage.Warning);
                oznakat.Focus();
                Dispatcher.BeginInvoke(new Action(() => oznakat.Undo()));
                return false;
            }

            return true;
        }

        private bool validacija2()
        {
            string name2 = imet.Text.Replace(" ", "");
            if (!name2.All(Char.IsLetterOrDigit))
            {

                MessageBox.Show("Greška, dozvoljen je unos samo slova i brojeva.", "Greška", MessageBoxButton.OK, MessageBoxImage.Warning);
                imet.Focus();
                Dispatcher.BeginInvoke(new Action(() => imet.Undo()));
                return false;
            }

            return true;
        }



        private void oznakat_TextChanged(object sender, TextChangedEventArgs e)
        {
            validacija1();
        }

        private void imet_TextChanged(object sender, TextChangedEventArgs e)
        {
            validacija2();
        }


        private void help_Click(object sender, RoutedEventArgs e)
        {

            HelpViewer hh = new HelpViewer("newType", parent1);
            hh.Show();
        }


    }
}

