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

        /// <summary>
        /// Helper for update
        /// </summary>
        private Customer selectedCustomer;

        /// <summary>
        /// Helper for update
        /// </summary>
        private Product selectedProduct;

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

        #region Common methods

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
        /// On app loaded
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            loadCustomers();
            loadProducts();
        }


        #endregion Common methods

        #region Product methods

        /// <summary>
        /// Load products
        /// </summary>
        private void loadProducts()
        {
            try
            {
                Products products = new Products();
                dgProducts.ItemsSource = products.GetProducts();
            }
            catch (Exception ex)
            {
                showError("Virhe: Tuotteiden haku ei onnistunut.", "Tuotteiden haku");
            }
        }

        /// <summary>
        /// Save product
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnProductSave_Click(object sender, RoutedEventArgs e)
        {
            Enums.Action action = Enums.Action.NotDefined;

            try
            {
                if (selectedProduct == null)
                {
                    action = Enums.Action.Insert;

                    // Add new
                    Product product = new Product();
                    fillProductObject(ref product);
                    product.AddProduct();
                    clearProductForm();
                }
                else
                {
                    action = Enums.Action.Update;

                    // Update existing
                    fillProductObject(ref selectedProduct);
                    selectedProduct.UpdateProduct();
                }

                loadProducts();
            }
            catch (Exception ex)
            {
                if (action == Enums.Action.Insert)
                    showError("Virhe: Tuotteen lisäys ei onnistunut.", "Tuotteen lisäys");
                else if (action == Enums.Action.Update)
                    showError("Virhe: Tuotteen muokkaus ei onnistunut.", "Tuotteen muokkaus");
            }
        }

        /// <summary>
        /// Clear product form
        /// </summary>
        private void clearProductForm()
        {
            // Clear labels
            lblProductId.Content = string.Empty;
            lblProductHeader.Content = "Lisää tuote";
            lblCreated.Content = string.Empty;
            lblModified.Content = string.Empty;

            // Clear textboxes
            tbProductNumber.Text = string.Empty;
            tbProductName.Text = string.Empty;
            tbVATPercent.Text = string.Empty;
            tbPrice.Text = string.Empty;
        }

        /// <summary>
        /// Fill product data
        /// </summary>
        /// <param name="product"></param>
        private void fillProductObject(ref Product product)
        {
            product.Number = tbProductNumber.Text;
            product.Name = tbProductName.Text;
            product.VATPercent = double.Parse(tbVATPercent.Text);
            product.Price = double.Parse(tbPrice.Text);
        }

        /// <summary>
        /// Select a product from a list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProductRow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                // Ensure row was clicked and not empty space
                DataGridRow row = ItemsControl.ContainerFromElement((DataGrid)sender, e.OriginalSource as DependencyObject) as DataGridRow;
                if (row == null) return;

                selectedProduct = (Product)row.DataContext;
                selectedProduct.GetProduct();

                lblProductId.Content = string.Format("{0}", selectedProduct.Id);
                lblCreated.Content = selectedProduct.Created;
                lblModified.Content = selectedProduct.Modified;
                tbProductNumber.Text = selectedProduct.Number;
                tbProductName.Text = selectedProduct.Name;
                tbVATPercent.Text = selectedProduct.VATPercent.ToString();
                tbPrice.Text = selectedProduct.Price.ToString();

                lblProductHeader.Content = "Muokkaa tuotetta";
            }
            catch (Exception ex)
            {
                showError("Virhe: Tuotteen valinta ei onnistunut.", "Muokkaa tuotetta");
            }
        }

        private void btnCancelProduct_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnDeleteProduct_Click(object sender, RoutedEventArgs e)
        {

        }

        #endregion Product methods

        #region Customer methods

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
        /// Fill customer data
        /// </summary>
        /// <param name="customer"></param>
        private void fillCustomerObject(ref Customer customer)
        {
            customer.Firstname = tbFirstname.Text;
            customer.Lastname = tbLastname.Text;
            customer.Company = tbCompany.Text;
            customer.InvoicingAddress.StreetAddress = tbStreetAddress.Text;
            customer.InvoicingAddress.PostalCode = tbPostalCode.Text;
            customer.InvoicingAddress.City = tbCity.Text;
            customer.Email = tbEmail.Text;
            customer.Phonenumber = tbPhonenumber.Text;
        }

        /// <summary>
        /// Save customer data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            Enums.Action action = Enums.Action.NotDefined;

            try
            {
                if (selectedCustomer == null)
                {
                    action = Enums.Action.Insert;

                    // Add new
                    Customer customer = new Customer();
                    fillCustomerObject(ref customer);
                    customer.AddCustomer();
                    clearCustomerForm();
                }
                else
                {
                    action = Enums.Action.Update;

                    // Update existing
                    fillCustomerObject(ref selectedCustomer);
                    selectedCustomer.UpdateCustomer();
                }

                loadCustomers();
            }
            catch (Exception ex)
            {
                if(action == Enums.Action.Insert)
                    showError("Virhe: Asiakkaan lisäys ei onnistunut.", "Asiakkaan lisäys");    
                else if(action == Enums.Action.Update)
                    showError("Virhe: Asiakkaan muokkaus ei onnistunut.", "Asiakkaan muokkaus");
            }
        }

        /// <summary>
        /// Select customer from a list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Row_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                // Ensure row was clicked and not empty space
                DataGridRow row = ItemsControl.ContainerFromElement((DataGrid)sender, e.OriginalSource as DependencyObject) as DataGridRow;
                if (row == null) return;

                selectedCustomer = (Customer)row.DataContext;
                selectedCustomer.GetCustomer();

                lblId.Content = string.Format("{0}", selectedCustomer.Id);
                tbFirstname.Text = selectedCustomer.Firstname;
                tbLastname.Text = selectedCustomer.Lastname;
                tbCompany.Text = selectedCustomer.Company;
                tbStreetAddress.Text = selectedCustomer.InvoicingAddress.StreetAddress;
                tbPostalCode.Text = selectedCustomer.InvoicingAddress.PostalCode;
                tbCity.Text = selectedCustomer.InvoicingAddress.City;
                tbEmail.Text = selectedCustomer.Email;
                tbPhonenumber.Text = selectedCustomer.Phonenumber;

                lblCustomerHeader.Content = "Muokkaa asiakasta";
            }
            catch (Exception ex)
            {
                showError("Virhe: Asiakkaan valinta ei onnistunut.", "Muokkaa asiakasta");
            }
        }

        /// <summary>
        /// Clear customer form
        /// </summary>
        private void clearCustomerForm()
        {
            lblCustomerHeader.Content = "Lisää asiakas";

            // Clear labels
            lblId.Content = string.Empty;

            // Clear form fields
            tbFirstname.Text = string.Empty;
            tbLastname.Text = string.Empty;
            tbCompany.Text = string.Empty;
            tbStreetAddress.Text = string.Empty;
            tbPostalCode.Text = string.Empty;
            tbCity.Text = string.Empty;
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
