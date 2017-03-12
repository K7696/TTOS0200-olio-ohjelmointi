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

namespace Harjoitukset8_tehtava4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Fields

        private string strValue { get; set; }
        private Heater heater = new Heater();

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Default ctor
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            strValue = string.Empty;
        }

        #endregion Constructors

        #region Private methods

        /// <summary>
        /// Validate number
        /// </summary>
        private bool showInvalidErrorMessage()
        {
            bool answer = true;

            lblError.Content = "Info: Set values and click OK";

            if (string.IsNullOrEmpty(strValue) == false && strValue.Equals("."))
            {
                lblError.Content = "Invalid number";
                answer = false;
            }
            else
            {
                if (strValue.Length > 0)
                {
                    double number;
                    bool nbParse = double.TryParse(strValue.Replace('.', ','), out number);
                    // Check how many dots
                    int count = strValue.Count(x => x == '.');

                    if (nbParse == false && count != 1)
                    {
                        lblError.Content = "Invalid number";
                        answer = false;
                    }
                }
            }

            return answer;
        }

        /// <summary>
        /// Reset stuff
        /// </summary>
        private void resetStuff()
        {
            tbValue.Text = "";
            strValue = "";
        }

        #endregion Private methods

        #region Logic

        /// <summary>
        /// Button 0-9 and.Click event handling, add chars to valueTextBloxk
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_Click(object sender, RoutedEventArgs e)
        {
            // get button Content string 0-9 and .
            string buttonString = (((Button)sender).Content).ToString();
            strValue += buttonString;
            tbValue.Text = strValue;
            showInvalidErrorMessage();
        }

        /// <summary>
        /// Remove Button Click event handling, remove last char from valueTextBlock
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonRemove_Click(object sender, RoutedEventArgs e)
        {
            // remove last number (or comma) from value
            string line = strValue;
            if (line.Length > 0)
            {
                line = line.Substring(0, line.Length - 1);
                tbValue.Text = line;
                strValue = line;
                showInvalidErrorMessage();
            }
        }

        /// <summary>
        /// Show temperature or humidity
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OK_Click(object sender, RoutedEventArgs e)
        {
            double number;
            bool parse = double.TryParse(strValue.Replace('.', ','), out number);

            if (parse)
            {
                try
                {
                    if ((bool)rbHumidity.IsChecked)
                    {
                        heater.SetHumidity(number);
                        lblHumidity.Content = heater.GetHumidity().ToString("f2");
                    }
                    else if ((bool)rbTemperature.IsChecked)
                    {
                        heater.SetTemperature(number);
                        lblTemperature.Content = heater.GetTemperature().ToString("f2");
                    }

                    resetStuff();
                }
                catch (Exception ex)
                {
                    lblError.Content = ex.Message;
                }                
            }       
        }

        #endregion Logic
    }
}
