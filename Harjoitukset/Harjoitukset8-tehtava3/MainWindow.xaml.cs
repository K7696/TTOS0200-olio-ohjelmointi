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

namespace Harjoitukset8_tehtava3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Fields

        private Lotto lotto;

        #endregion Fields

        #region Constructors

        public MainWindow()
        {
            InitializeComponent();

            lotto = new Lotto();
        }

        #endregion Constructors

        #region Public methods

        /// <summary>
        /// Generate lotto rows
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDraw_Click(object sender, RoutedEventArgs e)
        {
            lblInfo.Content = "Info: Set number and press draw.";

            string strRows = tbRows.Text;
            int number;
            bool parse = int.TryParse(strRows, out number);

            if(parse)
            {
                int index = 0;
                string row = "";
                List<int> numbers = new List<int>();
                while (index < number)
                {
                    numbers = lotto.GenerateLottoRow(1);

                    row += (index + 1 < 10) ? "row 0" + (index+1) + ": " : "row " + (index + 1) + ": ";

                    foreach (int item in numbers)
                    {
                        row += (item < 10) ? "0" + item + " " : item + " ";
                    }

                    // Tyhjennä rivi
                    numbers.Clear();

                    row += Environment.NewLine;

                    index++;
                }

                text.Text = row;
            }
            else
            {
                lblInfo.Content = "Info: Invalid number (it must be integer).";
            }         
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            text.Text = "";
        }

        #endregion Public methods
    }
}
