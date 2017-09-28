using System;
using System.Windows;
using System.Windows.Media;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using Xceed.Wpf.Toolkit;

namespace HCI_Lokali
{
    public partial class EditEtiketa : Window
    {
        private MainWindow parent1;
        private EtiketaList roditelj;
        public ObservableCollection<ColorItem> ColorList { get; set; }
        private Etiketa et = new Etiketa();

        //konstruktor
        public EditEtiketa(MainWindow prozor, EtiketaList parent, Etiketa etiketa)
        {
            InitializeComponent();
            this.roditelj = parent;
            parent1 = prozor;
            PopulateColorList();
            et = etiketa;
            DataContext = et;
            boje.DataContext = this;
            boje.SelectedColor = (Color)ColorConverter.ConvertFromString(etiketa.boje);
        }

        //izadji bez promena
        private void izlaz_Click(object sender, RoutedEventArgs e)
        {
            grid.BindingGroup.CancelEdit();
            this.Close();
        }

        //sacuvaj promene
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
                    et.imeBoje = tips.Name;
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
                    grid.BindingGroup.CommitEdit();
                    roditelj.tabela.Items.Refresh();
                    this.Close();
                }
            }

            else
            {
                Xceed.Wpf.Toolkit.MessageBox.Show("Sva polja moraju biti popunjena!", "Upozorenje!");
            }

        }

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

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            grid.BindingGroup.CancelEdit();
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

        private void help_Click_1(object sender, RoutedEventArgs e)
        {
            HelpViewer hh = new HelpViewer("modifyTag", parent1);
            hh.Show();
        }
    }
}
