using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.ComponentModel;

namespace HCI_Lokali
{
    public partial class MainWindow : Window
    {

        public BindingList<Lokal> lk_list { get; set; }
        public BindingList<Tip> tip_list { get; set; }
        public BindingList<Etiketa> eti_list { get; set; } //lista dodatih etiketa
        public BindingList<Ikonica> icon_list { get; set; }

        private Point point = new Point(-1, -1);
        private Lokal zaMapu = new Lokal();
        public Dictionary<Point, Lokal> koordinate = new Dictionary<Point, Lokal>();
        private Canvas can = new Canvas();

        private bool uspesno = false;
        private Lokal za_pregledati = new Lokal();

        #region MainWindow

        public MainWindow()
        {
            InitializeComponent();
            salkohola.ItemsSource = Enumeracije.SLUZENJE_ALKOHOLA;
            katCenacmb.ItemsSource = Enumeracije.KATEGORIJA_CENA;
            this.WindowState = WindowState.Maximized;
            this.Width = SystemParameters.PrimaryScreenWidth;
            this.Height = SystemParameters.PrimaryScreenHeight;

            tip_list = BazaPodataka.getInstance().bTip.getAll();
            eti_list = BazaPodataka.getInstance().bEtiketa.getAll();
            icon_list = BazaPodataka.getInstance().bIkonice.getAll();
            lk_list = BazaPodataka.getInstance().bLokal.getAll();

            ImageBrush ib = new ImageBrush();
            ib.ImageSource = new BitmapImage(new Uri("mapa.jpg", UriKind.Relative));
            //pozadina canvasa je slika mape 
            canvas.Background = ib; 

            prefarbaj();
            nacrtaj();
            DataContext = this;

        }

        #endregion

        #region nacrtaj - crta ikonice

        private void nacrtaj()
        {
            canvas.Children.Clear();
            foreach (Ikonica ik in icon_list)
            {
                koordinate.Clear();
                foreach (KeyValuePair<Point, Lokal> entry in ik.poz)
                {
                    koordinate.Add(entry.Key, entry.Value);
                    Canvas c = new Canvas();
                    c.Width = 35;
                    c.Height = 35;
                    Lokal l = new Lokal();
                    ImageBrush cb = new ImageBrush();

                    foreach (Lokal lo in lk_list)
                    {
                        if (lo.oznaka.Equals(entry.Value.oznaka))
                            cb.ImageSource = new BitmapImage(new Uri(entry.Value.slika, UriKind.Relative));
                    }

                    c.Background = cb;
                    Canvas.SetLeft(c, entry.Key.X);
                    Canvas.SetTop(c, entry.Key.Y);
                    canvas.Children.Add(c);

                    //dodaj dogadjaje: za pomeranje, za levi i za desni klik
                    c.MouseMove += new MouseEventHandler(c_MouseMove);
                    c.MouseRightButtonDown += new MouseButtonEventHandler(c_MouseRightButtonDown);
                    c.MouseLeftButtonDown += new MouseButtonEventHandler(c_MouseLeftButtonDown);
                }
            }
        }

        #endregion

        #region meni: Novi -> Lokal

        private void lok_Click(object sender, RoutedEventArgs e)
        {
            AddLokal add = new AddLokal(this);
            add.parent1 = this;
            add.ShowDialog();
        }

        #endregion

        #region meni: Novi -> Tip

        private void tip_Click(object sender, RoutedEventArgs e)
        {
            AddTip add = new AddTip(this);
            add.parent1 = this;
            add.ShowDialog();
        }

        #endregion

        #region meni: Novi -> Etiketa
       
        private void eti_Click(object sender, RoutedEventArgs e)
        {
            AddEtiketa add = new AddEtiketa(this);
            add.parent1 = this;
            add.ShowDialog();
        }

        #endregion

        #region  meni: Kolekcija -> Etiketa

        private void etikete_Click(object sender, RoutedEventArgs e)
        {
            EtiketaList el = new EtiketaList(this);
            el.ShowDialog();
        }

        #endregion

        #region meni: Kolekcija -> Tipovi
        
        private void tipovi_Click(object sender, RoutedEventArgs e)
        {
            TipList tl = new TipList(this);
            tl.ShowDialog();
        }

        #endregion

        #region dugme: Izbrisi

        private void brisi_Click(object sender, RoutedEventArgs e)
        {
            if (tabela1.SelectedIndex == -1)
            {
                MessageBox.Show("Greska, morate izabrati neku vrstu u tabeli pre izmene.", "", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            else if (tabela1.SelectedIndex > -1)
            {
                Lokal lo = (Lokal)tabela1.SelectedItem;
                Ikonica ik = new Ikonica();
                icon_list.Clear();

                foreach (KeyValuePair<Point, Lokal> entry in koordinate.Where(entry => entry.Value.oznaka == lo.oznaka).ToList())
                {
                    koordinate.Remove(entry.Key);
                }

                ik.poz = koordinate;
                icon_list.Add(ik);
                prefarbaj();

                //brise iz tabele
                lk_list.RemoveAt(tabela1.SelectedIndex);
            }

            tabela1.Items.Refresh();
        }

        #endregion

        #region dugme: Izmeni

        private void izmeni_Click(object sender, RoutedEventArgs e)
        {
            while (tabela1.SelectedIndex == -1)
            {
                MessageBox.Show("Greska, morate izabrati vrstu u tabeli pre izmene.", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            Lokal l = (Lokal)tabela1.SelectedItem;
            EditLokal el = new EditLokal(this, l, tip_list, eti_list);
            el.ShowDialog();
        }

        #endregion

        #region zatvaranje MainWindow-a

        private void MainWindow_FormClosing(object sender, CancelEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Da li želite da sačuvate izmene?", "Upozorenje", MessageBoxButton.YesNoCancel);

            if (result == MessageBoxResult.Yes)
            {
                BazaPodataka.getInstance().bLokal.Dodaj(this.lk_list);
                BazaPodataka.getInstance().bTip.Dodaj(this.tip_list);
                BazaPodataka.getInstance().bEtiketa.Dodaj(this.eti_list);
                BazaPodataka.getInstance().bIkonice.Dodaj(this.icon_list);
            }
            else if (result == MessageBoxResult.Cancel)
            {
                e.Cancel = true;
            }
        }

        #endregion
        
        #region povlacenje lokala iz tabele

        //klik na lokal iz liste - pozicija
        private void tabela1_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            point = e.GetPosition(null);
        }

        private void tabela1_MouseMove(object sender, MouseEventArgs e)
        {
            Point mousePos = e.GetPosition(null);
            Vector diff = point - mousePos;

            if (e.LeftButton == MouseButtonState.Pressed &&
                (Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance ||
                Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance))
            {
                if (!tabela1.SelectedIndex.Equals(-1))
                {
                    //ono sto saljem - selektivani lokal iz tabele
                    zaMapu = (Lokal)tabela1.SelectedItem; 

                     DataObject dataObject = new DataObject("iz tabele", zaMapu);
                     DragDrop.DoDragDrop(tabela1, dataObject, DragDropEffects.Move);

                }
            }
        }

        #endregion

        #region postavljanje ikonice na canvas 

        private void canvas_Drop(object sender, DragEventArgs e)
        {
            //ako smo povukli lokal iz tabele
            if (e.Data.GetDataPresent("iz tabele"))
            {
                Lokal lester = e.Data.GetData("iz tabele") as Lokal;
                Ikonica ik = new Ikonica();
                Canvas c = new Canvas();
                c.Width = 35;
                c.Height = 35;
                ImageBrush cb = new ImageBrush();


                cb.ImageSource = new BitmapImage(new Uri(lester.slika, UriKind.Relative));
                c.Background = cb;
                Point p = e.GetPosition(canvas);
                Canvas.SetLeft(c, p.X);
                Canvas.SetTop(c, p.Y);

                canvas.Children.Add(c);

                c.MouseLeftButtonDown += new MouseButtonEventHandler(c_MouseLeftButtonDown);
                c.MouseRightButtonDown += new MouseButtonEventHandler(c_MouseRightButtonDown);
                c.MouseMove += new MouseEventHandler(c_MouseMove);

                koordinate.Add(p, lester);
                ik.poz = koordinate;
                icon_list.Add(ik);

            }
            else if (e.Data.GetDataPresent("na mapu"))
            {
                Canvas x = e.Data.GetData("na mapu") as Canvas;

                Ikonica ik = new Ikonica();
                icon_list.Clear();

                Point p = e.GetPosition(canvas);
                Canvas.SetLeft(x, p.X);
                Canvas.SetTop(x, p.Y);
                int index = canvas.Children.IndexOf(x);
                KeyValuePair<Point, Lokal> par = koordinate.ElementAt(index);

                Lokal tr = par.Value;

                foreach (KeyValuePair<Point, Lokal> entry in koordinate.Where(entry => entry.Value == tr).ToList())
                {
                    koordinate.Remove(entry.Key);
                }

                koordinate.Add(p, par.Value);

                ik.poz = koordinate;
                icon_list.Add(ik);
            }
        }

        #endregion

        #region desni klik na ikonicu na canvasu - opcije

        void c_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            can = (Canvas)sender;

            for (int x = 0; x < canvas.Children.Count; x++)
            {
                if (canvas.Children[x].Equals(can))
                {
                    koordinate.TryGetValue(koordinate.Keys.ElementAt(x), out za_pregledati);
                }
            }

            can.ContextMenu = new ContextMenu();
            MenuItem menu1 = new MenuItem();
            MenuItem menu2 = new MenuItem();
            menu1.Header = "Obriši ikonicu lokala sa mape";
            menu1.Click += new RoutedEventHandler(menu1_Click);
            can.ContextMenu.Items.Add(menu1);

            menu2.Header = "Pregled lokala";
            menu2.Click += new RoutedEventHandler(menu2_Click);
            can.ContextMenu.Items.Add(menu2);
        }


        //opcija pregleda lokala
        void menu2_Click(object sender, RoutedEventArgs e)
        {

            PregledLokala pl = new PregledLokala(this, za_pregledati);
            pl.ShowDialog();
        }

        //opcija brisanje ikonice
        void menu1_Click(object sender, RoutedEventArgs e)
        {

            foreach (Canvas c in canvas.Children)
            {
                if (c.Equals(can))
                {
                    canvas.Children.Remove(c);
                    Point point = new Point();
                    point.X = Canvas.GetLeft(c);
                    point.Y = Canvas.GetTop(c);

                    koordinate.Remove(point);
                    icon_list.Clear();
                    Ikonica ik = new Ikonica();
                    ik.poz = koordinate;
                    icon_list.Add(ik);

                    break;
                }
            }
        }

        #endregion

        #region pomeranje ikonice na canvasu

        //klik na ikonicu na canvasu - pozicija
        void c_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            point = e.GetPosition(null);
        }

        void c_MouseMove(object sender, MouseEventArgs e)
        {
            Point mousePos = e.GetPosition(null);
            Vector diff = point - mousePos;

            if (e.LeftButton == MouseButtonState.Pressed &&
                (Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance ||
                Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance))
            {
                Canvas pero = (Canvas)sender;
                DataObject dataObject = new DataObject("na mapu", pero);
                DragDrop.DoDragDrop(canvas, dataObject, DragDropEffects.Move);

            }
        }

        #endregion

        #region prefarbaj

        public void prefarbaj()
        {
            canvas.Children.Clear();
            foreach (Point p in koordinate.Keys)
            {
                Canvas c = new Canvas();
                c.Width = 35;
                c.Height = 35;
                Lokal l = new Lokal();
                koordinate.TryGetValue(p, out l);
                ImageBrush cb = new ImageBrush();

                foreach (Lokal lo in lk_list)
                {
                    if (lo.oznaka.Equals(l.oznaka))

                        cb.ImageSource = new BitmapImage(new Uri(lo.slika, UriKind.Relative));
                }

                c.Background = cb;
                Canvas.SetLeft(c, p.X);
                Canvas.SetTop(c, p.Y);
                canvas.Children.Add(c);
                c.MouseMove += new MouseEventHandler(c_MouseMove);

            }
        }

        #endregion

        #region dugme: Filtriraj

        private void btnFilter_Click(object sender, RoutedEventArgs e)
        {
            tabela1.ItemsSource = null;
            tabela1.Items.Clear();

            foreach (Lokal lok in lk_list)
            {
                //samo tip
                if (cbTip.IsChecked.Value == true && lok.tip.ime.Contains(tipCmb.Text))
                {
                    tabela1.Items.Add(lok);
                }

                //samo alkohol
                else if (calkohol.IsChecked.Value == true && lok.statusalkohola.Contains(salkohola.Text))
                {
                    tabela1.Items.Add(lok);
                }

                //samo cena
                else if (ccena.IsChecked.Value == true && lok.kategorijaCena.Contains(katCenacmb.Text))
                {
                    tabela1.Items.Add(lok);
                }

                //tip i alkohol
                else if (cbTip.IsChecked.Value == true && lok.tip.ime.Contains(tipCmb.Text))
                {
                    if (calkohol.IsChecked.Value == true && lok.statusalkohola.Contains(salkohola.Text))
                    {
                        tabela1.Items.Add(lok);
                    }
                }

                //tip i cena
                else if (cbTip.IsChecked.Value == true && lok.tip.ime.Contains(tipCmb.Text))
                {
                    if (ccena.IsChecked.Value == true && lok.kategorijaCena.Contains(katCenacmb.Text))
                    {
                        tabela1.Items.Add(lok);
                    }
                }

                //alkohol i cena
                else if (calkohol.IsChecked.Value == true && lok.statusalkohola.Contains(salkohola.Text))
                {
                    if (ccena.IsChecked.Value == true && lok.kategorijaCena.Contains(katCenacmb.Text))
                    {
                        tabela1.Items.Add(lok);
                    }
                }

                //tip, alkohol i cena
                else if (cbTip.IsChecked.Value == true && lok.tip.ime.Contains(tipCmb.Text))
                {
                    if (calkohol.IsChecked.Value == true && lok.statusalkohola.Contains(salkohola.Text))
                    {
                        if (ccena.IsChecked.Value == true && lok.kategorijaCena.Contains(katCenacmb.Text))
                        {
                            tabela1.Items.Add(lok);
                        }
                    }
                }

            }
        }

        #endregion

        #region dugme: Ponisti

        private void btnUndo_Click(object sender, RoutedEventArgs e)
        {
            calkohol.IsChecked = false;
            ccena.IsChecked = false;
            cbTip.IsChecked = false;
            salkohola.SelectedValue = " ";
            katCenacmb.SelectedValue = " ";
            tipCmb.SelectedValue = " ";
            tabela1.Items.Clear();
            tabela1.ItemsSource = lk_list;
        }

        #endregion

        #region pretraga po imenu tipa

        private void txtTrazi_TextChanged(object sender, TextChangedEventArgs e)
        {
            tabela1.ItemsSource = null;
            tabela1.Items.Clear();

            if (!(txtTrazi.Text.Equals("")))
            {
                foreach (Lokal lok in lk_list)
                {
                    if (lok.ime.Contains(txtTrazi.Text))
                    {
                        tabela1.Items.Add(lok);
                        uspesno = true;
                    }
                }
            }
            else
            {
                foreach (Lokal loki in lk_list)
                {
                    tabela1.Items.Add(loki);
                    uspesno = true;
                }
            }

            if (uspesno)
            {
                tabela1.SelectedItem = tabela1.Items[0];
                uspesno = false;
            }
        }

        #endregion

        #region dugme: Novi

        private void novi_Click(object sender, RoutedEventArgs e)
        {
            AddLokal al = new AddLokal(this);
            al.Show();
        }

        #endregion

        #region help, dugme: Pomoc

        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var focusedControl = FocusManager.GetFocusedElement(Application.Current.Windows[0]);
            var obj = focusedControl as DependencyObject;
            if (obj == null) return;
            var str = HelpProvider.GetHelpKey(obj);
            HelpProvider.ShowHelp(str, this);
        }

        public void doThings(string param)
        {
            //
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            IInputElement focusedControl = FocusManager.GetFocusedElement(Application.Current.Windows[0]);
            if (focusedControl is DependencyObject)
            {
                string str = HelpProvider.GetHelpKey((DependencyObject)focusedControl);
                HelpProvider.ShowHelp(str, this);
            }

            var help = new HelpViewer("index", this);
        }

        #endregion

        #region dugme: Demo mod

        private void btndemo_Click(object sender, RoutedEventArgs e)
        {
            Demo_mode dm = new Demo_mode();
            dm.Show();
        }

        #endregion

    }
}
