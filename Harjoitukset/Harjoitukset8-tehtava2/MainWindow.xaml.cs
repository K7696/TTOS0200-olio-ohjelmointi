using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;
using Microsoft.Win32;
//using Windows.UI.ViewManagement;

namespace Harjoitukset8_tehtava2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //// change the default startup mode
            //ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;
            //// specify the size width:800, height:600 window size
            //ApplicationView.PreferredLaunchViewSize = new Size(800, 600);
            //// disable debugging frame rate counter
            //App.Current.DebugSettings.EnableFrameRateCounter = false;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Name: " + tbName.Text);

            if((bool)cbNormal.IsChecked)
            {
                Debug.WriteLine("Normal User");
            }

            if ((bool)cbAdmin.IsChecked)
            {
                Debug.WriteLine("Admin");
            }

        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a picture";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            if (op.ShowDialog() == true)
            {
                image.Source = new BitmapImage(new Uri(op.FileName));
            }
        }
    }
}
