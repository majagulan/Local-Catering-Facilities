using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using System.ComponentModel;
using System.Net.Cache;
using System.Linq;

namespace HCI_Lokali
{
    public partial class EditLokal : Window
    {
        private MainWindow parent1;
        private Lokal l = new Lokal();
        private BindingList<Tip> tip_list = new BindingList<Tip>();
        private BindingList<Etiketa> eti_list = new BindingList<Etiketa>();

        //konstruktor
        public EditLokal(MainWindow prozor, Lokal lok, BindingList<Tip> Lista, BindingList<Etiketa> ListaE)
        {
            InitializeComponent();
            alkoholbox.ItemsSource = Enumeracije.SLUZENJE_ALKOHOLA;
            cenabox.ItemsSource = Enumeracije.KATEGORIJA_CENA;
            this.parent1 = prozor;
            l = lok;
            tip_list = Lista;
            eti_list = ListaE;
            combo.DataContext = prozor;

            foreach (Tip tips in prozor.tip_list)
            {
                if (lok.tip.oznaka.Equals(tips.oznaka))
                {
                    combo.SelectedValue = tips.ime;
                    l.slika = tips.slika;
                }
            }

            combo.SelectionChanged += new SelectionChangedEventHandler(lostFocus);
            BitmapImage _image = new BitmapImage();
            _image.BeginInit();
            _image.CacheOption = BitmapCacheOption.None;
            _image.UriCachePolicy = new RequestCachePolicy(RequestCacheLevel.BypassCache);
            _image.CacheOption = BitmapCacheOption.OnLoad;
            _image.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
            _image.UriSource = new Uri(l.slika, UriKind.RelativeOrAbsolute);
            _image.EndInit();
            image1.Source = _image;

            this.DataContext = l;
            listBox.DataContext = prozor;

            foreach (Etiketa ets in prozor.eti_list)
            {
                listBox.SelectedItems.Add(ets);
            }

        }   //kraj konstruktora


        private void noviTip_Click(object sender, RoutedEventArgs e)
        {
            AddTip add = new AddTip(parent1);
            add.ShowDialog();
        }

        private void izlaz_Click(object sender, RoutedEventArgs e)
        {
            grid.BindingGroup.CancelEdit();
            this.Close();
        }

        private void ucitavanje_Click(object sender, RoutedEventArgs e)
        {

            OpenFileDialog dlg = new OpenFileDialog();

            dlg.Title = "Otvori sliku";
            dlg.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";

            if (dlg.ShowDialog() == DialogResult.Equals("OK"))

                image1 = new Image();
            image1.Source = new BitmapImage(new Uri(dlg.FileName));
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

        private void u_redu_Click(object sender, RoutedEventArgs e)
        {
            if (!validacija1() && !validacija2() && !validacija3())
            {
                return;
            }

            if (oznakat.Text.Equals("") || imet.Text.Equals("") || opist.Text.Equals("") || kapacitetText.Text.Equals("") || image1.Source == null || combo.SelectedIndex == -1 || alkoholbox.SelectedIndex == -1 || cenabox.SelectedIndex == -1)
            {
                MessageBox.Show("Greska, morate popuniti sva prethodna polja.", "Greska", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            foreach (Etiketa et in listBox.SelectedItems)
            {
                l.etikete.Add(et);
            }

            if (datePicker1.SelectedDate == null)
            {
                MessageBox.Show("Greska, morate izabrati datum.", "Greska", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            bool indikator_oznake = false;

            //provera jedinstvenosti oznake!!!
            foreach (Lokal l in parent1.lk_list)
            {
                if (oznakat.Text.Equals(l.oznaka))
                {
                    MessageBox.Show("Polje oznaka mora biti jedinstveno!", "Upozorenje!");
                    indikator_oznake = true;
                }
            }

            if (indikator_oznake == false)
            {
                grid.BindingGroup.CommitEdit();
                parent1.tabela1.Items.Refresh();
                this.Close();
            }

            //dodaje lokal
            foreach (Tip tips in parent1.tip_list)
            {
                if (tips.ime.Equals(combo.SelectedValue.ToString()))
                    l.tip = tips;
            }
        }


        private void EditLokal_FormClosing(object sender, CancelEventArgs e)
        {
            grid.BindingGroup.CancelEdit();
        }

        private void datePicker1_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            datePicker1.DisplayDateEnd = DateTime.Today;
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

                MessageBox.Show("Greska, dozvoljen je unos samo slova i brojeva.", "Greska", MessageBoxButton.OK, MessageBoxImage.Warning);
                imet.Focus();
                Dispatcher.BeginInvoke(new Action(() => imet.Undo()));
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


        private void oznakat_TextChanged(object sender, TextChangedEventArgs e)
        {
            validacija1();
        }

        private void imet_TextChanged(object sender, TextChangedEventArgs e)
        {
            validacija2();
        }

        private void kapacitetText_TextChanged(object sender, TextChangedEventArgs e)
        {
            validacija3();
        }


        private void help_Click_1(object sender, RoutedEventArgs e)
        {

            HelpViewer hh = new HelpViewer("lokaliView", parent1);
            hh.Show();
        }


    }
}
