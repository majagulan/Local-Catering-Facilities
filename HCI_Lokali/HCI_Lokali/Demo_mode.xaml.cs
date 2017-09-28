using System;
using System.Windows;


namespace HCI_Lokali
{
    public partial class Demo_mode : Window
    {
        public Demo_mode()
        {
            InitializeComponent(); 
            MediaPlayer.Source = new Uri("demo_hci.webm", UriKind.Relative);
            btnPlay.IsEnabled = true;
        }

        private void IsPlaying(bool flag)
        {
            btnPlay.IsEnabled = flag;
        }

        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            IsPlaying(true);
            if (btnPlay.Content.ToString() == "Počni demo mod")
            {
                MediaPlayer.Play();
                btnPlay.Content = "Pauziraj demo mod";
            }
            else
            {
                MediaPlayer.Pause();
                btnPlay.Content = "Počni demo mod";
            }
        }

        private void btnIzadji_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
