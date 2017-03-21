using Harjoitustyo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
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
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Harjoitustyo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Constructors

        /// <summary>
        /// Default ctor
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            Loaded += MainWindow_Loaded;
        }

        #endregion Constructors

        #region Customer methods

        /// <summary>
        /// On app loaded
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            loadCustomers();
        }

        /// <summary>
        /// Load customers
        /// </summary>
        private void loadCustomers()
        {
            Customers customers = new Customers();
            dataGrid.ItemsSource = customers.GetCustomers();
        }

        private void txtName_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox t = (TextBox)sender;
            string filter = t.Text;
            ICollectionView cv = CollectionViewSource.GetDefaultView(dataGrid.ItemsSource);
            if (filter == "")
                cv.Filter = null;
            else
            {
                cv.Filter = o =>
                {
                    Customer p = o as Customer;
                    if (t.Name == "txtId")
                        return (p.Id == Convert.ToInt32(filter));
                    return (p.Lastname.ToUpper().Contains(filter.ToUpper()));
                };
            }
        }

        /// <summary>
        /// Save customer data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            Customer customer = new Customer();
            customer.Firstname = tbFirstname.Text;
            customer.Lastname = tbLastname.Text;
            customer.AddCustomer();

            loadCustomers();
        }

        private void Row_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Ensure row was clicked and not empty space
            DataGridRow row = ItemsControl.ContainerFromElement((DataGrid)sender, e.OriginalSource as DependencyObject) as DataGridRow;
            if (row == null) return;

            Customer customer = (Customer)row.DataContext;
            customer.GetCustomer();

            tbFirstname.Text = customer.Firstname;
            tbLastname.Text = customer.Lastname;
        }


        #endregion Customer methods

    }
}
