using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Net.Cache;
using Microsoft.Win32;
using System.Linq;

namespace HCI_Lokali
{
    public partial class EditTip : Window
    {
        private TipList roditelj;
        private MainWindow parent1;
        private Tip tipp = new Tip();

        //konstruktor
        public EditTip(MainWindow prozor, TipList parent, Tip tip)
        {
            InitializeComponent();
            this.roditelj = parent;
            parent1 = prozor;
            tipp = tip;

            if (tip.slika != null)
            {
                BitmapImage _image = new BitmapImage();
                _image.BeginInit();
                _image.CacheOption = BitmapCacheOption.None;
                _image.UriCachePolicy = new RequestCachePolicy(RequestCacheLevel.BypassCache);
                _image.CacheOption = BitmapCacheOption.OnLoad;
                _image.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
                _image.UriSource = new Uri(tip.slika, UriKind.RelativeOrAbsolute);
                _image.EndInit();
                image.Source = _image;
            }

            this.DataContext = tipp;
        }

        //izadji bez promena
        private void izlaz_Click(object sender, RoutedEventArgs e)
        {
            grid.BindingGroup.CancelEdit();
            this.Close();
        }

        //snimi promene
        private void u_redu_Click(object sender, RoutedEventArgs e)
        {
            if (!validacija1() && !validacija2())
            {
                return;
            }

            Boolean pr = true;

            if (oznakat.Text.Equals("")) pr = false;
            if (imet.Text.Equals("")) pr = false;
            if (opist.Text.Equals("")) pr = false;
            if (image.Source == null) pr = false; // provera za ikonicu


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
                    grid.BindingGroup.CommitEdit();
                    roditelj.tabela.Items.Refresh();
                    this.Close();
                }

            }
            else
            {
                MessageBox.Show("Sva polja moraju biti popunjena!", "Upozorenje!");
            }


            parent1.tabela1.Items.Refresh();
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

            tipp.slika = dg.FileName;
            image.Source = new BitmapImage(new Uri(tipp.slika));
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            grid.BindingGroup.CancelEdit();
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

        private void oznakat_TextChanged(object sender, TextChangedEventArgs e)
        {
            validacija1();
        }

        private void imet_TextChanged(object sender, TextChangedEventArgs e)
        {
            validacija2();
        }

        private void help_Click_1(object sender, RoutedEventArgs e)
        {
            HelpViewer hh = new HelpViewer("modifyType", parent1);
            hh.Show();
        }
    }
}
