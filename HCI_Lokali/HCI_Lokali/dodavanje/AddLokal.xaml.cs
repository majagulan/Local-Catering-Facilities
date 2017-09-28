using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Net.Cache;
using System.ComponentModel;
using Microsoft.Win32;
using System.Linq;

namespace HCI_Lokali
{
    public partial class AddLokal : Window
    {

        public MainWindow parent1;
        public Lokal l = new Lokal();

        public AddLokal(MainWindow prozor)
        {
            InitializeComponent();
            cenaBox.ItemsSource = Enumeracije.KATEGORIJA_CENA;
            alkohol.ItemsSource = Enumeracije.SLUZENJE_ALKOHOLA;
            this.parent1 = prozor;
            this.DataContext = l;
            l.etikete = new BindingList<Etiketa>();
            combo.DataContext = parent1;    //za tip
            listBox.DataContext = parent1;

        }

        //dugme pored tipa ako zelimo ipak da kreiramo novi tip
        private void noviTip_Click(object sender, RoutedEventArgs e)
        {
            AddTip add = new AddTip(parent1);
            add.Owner = this;
            add.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            add.ShowDialog();
        }

        private void izlaz_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        //ucitavanje ikonice
        private void ucitavanje_Click(object sender, RoutedEventArgs e)
        {

            OpenFileDialog dlg = new OpenFileDialog();

            dlg.Title = "Otvori sliku";
            dlg.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";

            if (dlg.ShowDialog() == DialogResult.Equals("OK"))

                image1 = new Image();

            l.slika = dlg.FileName;
            //ovo prikazuje sliku koju smo odabrali iz foldera
            image1.Source = new BitmapImage(new Uri(l.slika));

        }

        //povuci ikonicu iz tipa cim odaberem tip
        private void lostFocus(object sender, RoutedEventArgs e)
        {

            foreach (Tip tips in parent1.tip_list)
            {
                if ((combo.SelectedValue.ToString()).Equals(tips.ime))
                {
                    l.slika = tips.slika;
                }
            }

            BitmapImage _image = new BitmapImage();
            _image.BeginInit();
            _image.CacheOption = BitmapCacheOption.None;
            _image.UriCachePolicy = new RequestCachePolicy(RequestCacheLevel.BypassCache);
            _image.CacheOption = BitmapCacheOption.Default;
            _image.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
            _image.UriSource = new Uri(l.slika, UriKind.RelativeOrAbsolute);
            _image.EndInit();
            image1.Source = _image;
        }

        //---------------------ovo dodaje lokal
        private void u_redu_Click(object sender, RoutedEventArgs e)
        {

            if (!validacija1() && !validacija2() && !validacija3())
            {
                return;
            }


            if (oznakabox.Text.Equals("") || imebox.Text.Equals("") || opisbox.Text.Equals("") || kapacitetText.Text.Equals("") || image1.Source == null || combo.SelectedIndex == -1 || alkohol.SelectedIndex == -1 || cenaBox.SelectedIndex == -1)
            {
                MessageBox.Show("Greska, morate popuniti sva prethodna polja.", "Greska", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            //dodaje lokal
            foreach (Tip tips in parent1.tip_list)
            {
                if (tips.ime.Equals(combo.SelectedValue.ToString()))
                    l.tip = tips;
            }

            //dodaj etikete
            foreach (Etiketa et in listBox.SelectedItems)
            {
                l.etikete.Add(et);
            }

            bool indikator_oznake = false;

            if (datePicker1.SelectedDate == null)
            {
                MessageBox.Show("Greska, morate izabrati datum.", "Greska", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            //provera jedinstvenosti oznake!!!
            foreach (Lokal v in parent1.lk_list)
            {
                if (oznakabox.Text.Equals(v.oznaka))
                {
                    MessageBox.Show("Polje oznaka mora biti jedinstveno!", "Upozorenje!");
                    indikator_oznake = true;
                }
            }

            if (indikator_oznake == false)
            {
                parent1.tabela1.Items.Refresh();
                parent1.lk_list.Add(l);
                this.Close();
            }
        }

        //-----------------------------------------------------------------

        private bool validacija1()
        {
            string name1 = oznakabox.Text.Replace(" ", "");
            if (!name1.All(Char.IsLetterOrDigit))
            {

                MessageBox.Show("Greska, dozvoljen je unos samo slova i brojeva.", "Greska", MessageBoxButton.OK, MessageBoxImage.Warning);
                oznakabox.Focus();
                Dispatcher.BeginInvoke(new Action(() => oznakabox.Undo()));
                return false;
            }

            return true;
        }

        private bool validacija2()
        {
            string name2 = imebox.Text.Replace(" ", "");
            if (!name2.All(Char.IsLetterOrDigit))
            {

                MessageBox.Show("Greska, dozvoljen je unos samo slova i brojeva.", "Greska", MessageBoxButton.OK, MessageBoxImage.Warning);
                imebox.Focus();
                Dispatcher.BeginInvoke(new Action(() => imebox.Undo()));
                return false;
            }

            return true;
        }

        private bool validacija3()
        {
            if (!kapacitetText.Text.All(char.IsNumber))
            {

                MessageBox.Show("Greska, dozvoljen je unos samo brojne vrednosti.", "Greska", MessageBoxButton.OK, MessageBoxImage.Warning);
                kapacitetText.Focus();
                Dispatcher.BeginInvoke(new Action(() => kapacitetText.Undo()));
                return false;
            }

            return true;
        }


        private void oznakabox_TextChanged(object sender, TextChangedEventArgs e)
        {
            validacija1();
        }

        private void imebox_TextChanged(object sender, TextChangedEventArgs e)
        {
            validacija2();
        }

        private void kapacitetText_TextChanged(object sender, TextChangedEventArgs e)
        {
            validacija3();
        }

        private void datePicker1_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            datePicker1.DisplayDateEnd = DateTime.Today;
        }


        private void help_Click_1(object sender, RoutedEventArgs e)
        {

            HelpViewer hh = new HelpViewer("newLokal", parent1);
            hh.Show();
        }


    }
}
