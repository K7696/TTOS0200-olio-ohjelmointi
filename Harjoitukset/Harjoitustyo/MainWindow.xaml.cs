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
        #region Fields

        private Customer selectedCustomer;

        #endregion Fields

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
            try
            {
                Customers customers = new Customers();
                dataGrid.ItemsSource = customers.GetCustomers();
            }
            catch (Exception ex)
            {
                showError("Virhe: Asiakkaiden haku ei onnistunut.", "Asiakkaiden haku");
            }
            
        }

        /// <summary>
        /// Filter customer list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
            try
            {
                if (selectedCustomer == null)
                {
                    Customer customer = new Customer();
                    customer.Firstname = tbFirstname.Text;
                    customer.Lastname = tbLastname.Text;
                    customer.AddCustomer();

                    loadCustomers();
                }
                else
                {
                    selectedCustomer.Firstname = tbFirstname.Text;
                    selectedCustomer.Lastname = tbLastname.Text;
                    selectedCustomer.UpdateCustomer();

                    loadCustomers();
                }
            }
            catch (Exception ex)
            {
                showError("Virhe: Asiakkaan lisäys ei onnistunut.", "Asiakkaan lisäys");        
            }
        }

        /// <summary>
        /// Select customer from a list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Row_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Ensure row was clicked and not empty space
            DataGridRow row = ItemsControl.ContainerFromElement((DataGrid)sender, e.OriginalSource as DependencyObject) as DataGridRow;
            if (row == null) return;

            selectedCustomer = (Customer)row.DataContext;
            selectedCustomer.GetCustomer();

            tbFirstname.Text = selectedCustomer.Firstname;
            tbLastname.Text = selectedCustomer.Lastname;
        }

        /// <summary>
        /// Clear customer form
        /// </summary>
        private void clearCustomerForm()
        {
            tbFirstname.Text = string.Empty;
            tbLastname.Text = string.Empty;
            tbCompany.Text = string.Empty;
            tbStreetAddress.Text = string.Empty;
            tbPostalCode.Text = string.Empty;
            tbPostOffice.Text = string.Empty;
            tbEmail.Text = string.Empty;
            tbPhonenumber.Text = string.Empty;
        }

        /// <summary>
        /// Cancel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            clearCustomerForm();
            selectedCustomer = null;
        }

        /// <summary>
        /// Show custom error
        /// </summary>
        /// <param name="error">Error message</param>
        /// <param name="functionName">Name of a function</param>
        private void showError(string error, string functionName)
        {
            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show(error, functionName, System.Windows.MessageBoxButton.OK);
        }

        /// <summary>
        /// Delete customer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if(selectedCustomer == null)
            {
                showError("Valitse poistettava asiakas listalta.", "Asiakkaan poisto");
            }
            else
            {
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Haluatko varmasti poistaa asiakkaan?", "Asiakkaan poisto", System.Windows.MessageBoxButton.YesNo);

                if(messageBoxResult == MessageBoxResult.Yes)
                {
                    try
                    {
                        selectedCustomer.DeleteCustomer();
                        selectedCustomer = null;
                        clearCustomerForm();
                        loadCustomers();
                    }
                    catch (Exception ex)
                    {
                        showError("Virhe: Asiakkaan poisto ei onnistunut.", "Asiakkaan poisto");
                    }
                }               
            }     
        }


        #endregion Customer methods

    }
}
