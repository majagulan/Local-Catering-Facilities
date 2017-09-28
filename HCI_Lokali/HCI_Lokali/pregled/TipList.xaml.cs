using System.Windows;
using System.ComponentModel;

namespace HCI_Lokali
{
    public partial class TipList : Window
    {
        public MainWindow parent1;
        //moram i ovde da ima listu za tip
        public BindingList<Tip> tip_list = new BindingList<Tip>();

        public TipList(MainWindow prozor)
        {
            InitializeComponent();
            this.parent1 = prozor;
            DataContext = prozor;
        }

        private void izlaz_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void izmena_Click(object sender, RoutedEventArgs e)
        {
            if (tabela.SelectedIndex == -1)
            {
                MessageBox.Show("Greska, morate izabrati neki tip u tabeli prije izmene.", "", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            Tip t = (Tip)tabela.SelectedItem;
            EditTip et = new EditTip(parent1, this, t);
            et.ShowDialog();

        }

        private void brisanje_Click(object sender, RoutedEventArgs e)
        {
            if (tabela.SelectedIndex == -1)
            {
                MessageBox.Show("Greska, niste izabrali tip koji želite da izbrišete.", "", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (tabela.SelectedIndex > -1)
                parent1.tip_list.RemoveAt(tabela.SelectedIndex);

        }

        private void novi_Click(object sender, RoutedEventArgs e)
        {
            AddTip add = new AddTip(parent1);
            add.ShowDialog();
        }


        private void help_Click_1(object sender, RoutedEventArgs e)
        {
            HelpViewer hh = new HelpViewer("TypeView", parent1);
            hh.Show();
        }
    }
}

