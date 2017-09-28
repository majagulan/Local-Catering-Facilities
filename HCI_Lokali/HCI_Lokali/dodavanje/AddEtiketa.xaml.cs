using System.Windows;
using System.Windows.Media;
using Xceed.Wpf.Toolkit;
using System.Collections.ObjectModel;
using System;
using System.Linq;

namespace HCI_Lokali
{
    public partial class AddEtiketa : Window
    {
        public MainWindow parent1;
        //lista boja koje nam se nude
        public ObservableCollection<ColorItem> ColorList { get; set; }
        private Etiketa et = new Etiketa();

        //dodavanje etikete metoda
        public AddEtiketa(MainWindow prozor)
        {
            InitializeComponent();
            this.parent1 = prozor;
            this.DataContext = et;
            PopulateColorList();
            boje.DataContext = this;

        }

        private void izlaz_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        //kada kliknemo na UREDU (treba da snimi etiketu)
        private void uRedu_Click(object sender, RoutedEventArgs e)
        {
            if (!validacija1())
            {
                return;
            }

            et.boje = boje.SelectedColor.ToString();
            foreach (ColorItem tips in ColorList)
            {
                if ((tips.Color.ToString()).Equals(boje.SelectedColor.ToString()))
                {
                    et.imeBoje = tips.Name;
                }
            }


            Boolean pr = true;

            if (oznaka.Text.Equals("")) pr = false;
            if (boje.ToString().Equals("")) pr = false;
            if (opis.Text.Equals("")) pr = false;

            bool indikator_oznake = false;

            if (pr == true)
            {

                //provera jedinstvenosti oznake!!!
                foreach (Etiketa ee in parent1.eti_list)
                {
                    if (oznaka.Text.Equals(ee.oznaka))
                    {
                        System.Windows.MessageBox.Show("Polje oznaka mora biti jedinstveno!", "Upozorenje!");
                        indikator_oznake = true;
                    }
                }

                if (indikator_oznake == false)
                {
                    Color boja = (Color)boje.SelectedColor;
                    et.imeBoje = boja.ToString();

                    parent1.eti_list.Add(et);
                    this.Close();
                }

            }

            else
            {
                System.Windows.MessageBox.Show("Sva polja moraju biti popunjena!", "Upozorenje!");
            }

        }

        //metoda koja dodaje izabranu boju u listu boja
        private void PopulateColorList()
        {
            ColorList = new ObservableCollection<ColorItem>();
            ColorList.Add(new ColorItem(Color.FromRgb(244, 67, 54), "Crvena"));
            ColorList.Add(new ColorItem(Color.FromRgb(233, 30, 99), "Roze"));
            ColorList.Add(new ColorItem(Color.FromRgb(156, 39, 176), "Ljubičasta"));
            ColorList.Add(new ColorItem(Color.FromRgb(103, 58, 183), "Tamno ljubičasta"));
            ColorList.Add(new ColorItem(Color.FromRgb(63, 81, 181), "Indigo"));
            ColorList.Add(new ColorItem(Color.FromRgb(33, 150, 243), "Plava"));
            ColorList.Add(new ColorItem(Color.FromRgb(3, 169, 244), "Svetlo plava"));
            ColorList.Add(new ColorItem(Color.FromRgb(0, 188, 212), "Cijan"));
            ColorList.Add(new ColorItem(Color.FromRgb(0, 150, 136), "Tirkiz"));
            ColorList.Add(new ColorItem(Color.FromRgb(76, 175, 80), "Zelena"));
            ColorList.Add(new ColorItem(Color.FromRgb(139, 195, 74), "Svetlo zelena"));
            ColorList.Add(new ColorItem(Color.FromRgb(205, 220, 57), "Jarko zelena"));
            ColorList.Add(new ColorItem(Color.FromRgb(255, 235, 59), "Žuta"));
            ColorList.Add(new ColorItem(Color.FromRgb(255, 193, 7), "Svetlo narandžasta"));
            ColorList.Add(new ColorItem(Color.FromRgb(255, 152, 0), "Narandžasta"));
            ColorList.Add(new ColorItem(Color.FromRgb(255, 87, 34), "Tamno narandžasta"));
            ColorList.Add(new ColorItem(Color.FromRgb(121, 85, 72), "Braon"));
            ColorList.Add(new ColorItem(Color.FromRgb(158, 158, 158), "Siva"));
            ColorList.Add(new ColorItem(Color.FromRgb(96, 125, 139), "Tamno siva"));
            ColorList.Add(new ColorItem(Color.FromRgb(0, 0, 0), "Crna"));
        }


        private bool validacija1()
        {
            string name1 = oznaka.Text.Replace(" ", "");
            if (!name1.All(Char.IsLetterOrDigit))
            {

                System.Windows.MessageBox.Show("Greska, dozvoljen je unos samo slova i brojeva.", "Greska", MessageBoxButton.OK, MessageBoxImage.Warning);
                oznaka.Focus();
                Dispatcher.BeginInvoke(new Action(() => oznaka.Undo()));
                return false;
            }

            return true;
        }

        private void oznaka_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            validacija1();
        }


        private void help_Click(object sender, RoutedEventArgs e)
        {

            HelpViewer hh = new HelpViewer("newTag", parent1);
            hh.Show();
        }
    }
}
