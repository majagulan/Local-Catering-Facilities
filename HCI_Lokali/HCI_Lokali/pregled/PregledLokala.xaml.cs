using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Net.Cache;

namespace HCI_Lokali
{
    public partial class PregledLokala : Window
    {
        private MainWindow parent1;
        private Lokal lo = new Lokal();


        public PregledLokala(MainWindow roditelj, Lokal l)
        {
            InitializeComponent();
            this.parent1 = roditelj;
            lo = l;

            List<String> lista = new List<String>();

            oznakabox.Text += l.oznaka;
            imebox.Text += l.ime;
            opisbox.Text += l.opis;
            combo.Text += l.tip.ime;
            cenabox.Text += l.kategorijaCena;

            if (l.rezervacije == true)
                rezervacijebox.Text += "Da";

            else if (l.rezervacije == false)
                rezervacijebox.Text += "Ne";

            if (l.hendikepirani == true)
                hendikepiranibox.Text += "Da";

            else if (l.hendikepirani == false)
                hendikepiranibox.Text += "Ne";

            if (l.pusenje == true)
                pusenjebox.Text += "Da";

            else if (l.pusenje == false)
                pusenjebox.Text += "Ne";

            alkoholbox.Text += l.statusalkohola;
            dat.Text += l.datum;
            kapacitetbox.Text += l.kapacitet;

            foreach (Etiketa ets in l.etikete)
            {
                lista.Add(ets.oznaka.ToString());
            }

            foreach (String s in lista)
            {
                TextBlock tb = new TextBlock();
                tb.Text += s;
                listBox.Items.Add(tb);
            }


            BitmapImage _image = new BitmapImage();
            _image.BeginInit();
            _image.CacheOption = BitmapCacheOption.None;
            _image.UriCachePolicy = new RequestCachePolicy(RequestCacheLevel.BypassCache);
            _image.CacheOption = BitmapCacheOption.OnLoad;
            _image.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
            _image.UriSource = new Uri(l.slika, UriKind.RelativeOrAbsolute);
            _image.EndInit();
            image1.Source = _image;
            this.DataContext = lo;
        }

        private void izlaz_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


    }
}
