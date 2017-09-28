using System.Windows;
using System.ComponentModel;

namespace HCI_Lokali
{
    public partial class EtiketaList : Window
    {
        public MainWindow parent1;
        public BindingList<Etiketa> eti_list = new BindingList<Etiketa>();

        public EtiketaList(MainWindow prozor)
        {
            InitializeComponent();
            this.parent1 = prozor;
            DataContext = prozor;
        }

        private void izlazak_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void novi_Click(object sender, RoutedEventArgs e)
        {
            AddEtiketa add = new AddEtiketa(parent1);
            add.Show();
        }

        private void brisanje_Click(object sender, RoutedEventArgs e)
        {
            if (tabela.SelectedIndex == -1)
            {
                MessageBox.Show("Greska, niste izabrali etiketu koju želite da izbrišete.", "", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (tabela.SelectedIndex > -1)
                parent1.eti_list.RemoveAt(tabela.SelectedIndex);
        }


        private void izmena_Click(object sender, RoutedEventArgs e)
        {
            if (tabela.SelectedIndex == -1)
            {
                MessageBox.Show("Greska, morate izabrati neku etiketu u tabeli prije izmjene.", "", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            Etiketa et = (Etiketa)tabela.SelectedItem;
            EditEtiketa eti = new EditEtiketa(parent1, this, et);
            eti.ShowDialog();
        }


        private void help_Click(object sender, RoutedEventArgs e)
        {
            HelpViewer hh = new HelpViewer("TagView", parent1);
            hh.Show();
        }
    }
}
