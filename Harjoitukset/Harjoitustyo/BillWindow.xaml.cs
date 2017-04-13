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
using System.Windows.Shapes;

namespace Harjoitustyo
{
    /// <summary>
    /// Interaction logic for Bill.xaml
    /// </summary>
    public partial class BillWindow : Window
    {
        #region Fields 

        /// <summary>
        /// Selected bill
        /// </summary>
        private Bill selectedBill;

        /// <summary>
        /// Company data
        /// </summary>
        private Company company;

        /// <summary>
        /// Main window
        /// </summary>
        private MainWindow parentWindow;

        List<string> validationErrors;
    
        #endregion Fields

        #region Constructors

        /// <summary>
        /// Default ctor
        /// </summary>
        public BillWindow(MainWindow parent)
        {
            initBillWindow(parent, null);
        }

        /// <summary>
        /// Overrided ctor
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="bill"></param>
        public BillWindow(MainWindow parent, Bill bill)
        {
            initBillWindow(parent, bill);
        }

        #endregion Constructors

        #region Private methods

        /// <summary>
        /// Initializer
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="bill"></param>
        private void initBillWindow(MainWindow parent, Bill bill)
        {
            InitializeComponent();

            // Set parent window
            parentWindow = parent;
            Owner = parent;

            // Init bill object
            selectedBill = bill;

            // Init company object
            company = new Company();

            Loaded += BillWindow_Loaded;

            // Init validation errors list
            validationErrors = new List<string>();
        }

        #endregion Private methods

        #region Common methods

        /// <summary>
        /// Call parent window's methods
        /// </summary>
        private void callMainWindow()
        {
            // A parent window has a Call-method
            parentWindow.Call();
        }

        /// <summary>
        /// On window loadead
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BillWindow_Loaded(object sender, RoutedEventArgs e)
        {
            fillCustomerCombo();
            loadOwnData();

            // Hack: for adding new rows when creating a new bill
            List<BillRow> list = new List<BillRow>();
            dgBillRows.ItemsSource = list;

            // If existing bill was double clicked
            if (selectedBill != null)
                getBill();
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
        /// Get validation errors
        /// </summary>
        /// <returns></returns>
        private string getErrors()
        {
            string error = string.Format("Huomio:{0}", Environment.NewLine);

            foreach (string err in validationErrors)
            {
                error += string.Format("{0}{1}", err, Environment.NewLine);
            }

            // Clear validation errors
            validationErrors.Clear();

            return error;
        }

        #endregion Common methods

        #region Own data

        /// <summary>
        /// Load own data
        /// </summary>
        private void loadOwnData()
        {
            try
            {
                company.GetCompany();
            }
            catch (Exception ex)
            {
                showError("Virhe: Omien tietojen haku ei onnistunut.", "Omat tiedot");
            }
        }

        #endregion Own data

        #region Customer methods

        /// <summary>
        /// Fill customers combo
        /// </summary>
        private void fillCustomerCombo()
        {
            try
            {
                Customers customers = new Customers();

                List<Customer> list = customers.GetCustomers();

                List<UI.ComboBoxItem> comboList = new List<UI.ComboBoxItem>();
                
                foreach (Customer c in list)
                {
                    string name = (string.IsNullOrEmpty(c.Company)) ? 
                        string.Format("{0} {1}" , c.Firstname, c.Lastname) : 
                            string.Format("{0} - {1} {2}", c.Company, c.Firstname, c.Lastname);

                    comboList.Add(new UI.ComboBoxItem {
                        Id = c.CustomerId,
                        Name = name
                    });
                }

                // Sort list
                comboList.Sort((x, y) => string.Compare(x.Name, y.Name));

                cbCustomers.ItemsSource = comboList;
                cbCustomers.DisplayMemberPath = "Name";
            }
            catch (Exception ex)
            {

                throw;
            }
        }


        #endregion Customer methods

        #region Bill methods

        /// <summary>
        /// Get a bill data
        /// </summary>
        private void getBill()
        {
            try
            {
                selectedBill.GetBill();

                fillBillForm();
            }
            catch (Exception ex)
            {
                showError("Virhe: Laskun haku ei onnistunut.", "Muokkaa laskua");
            }
        }

        /// <summary>
        /// Fill bill form
        /// </summary>
        private void fillBillForm()
        {
            tbBillNumber.Text = selectedBill.BillNumber;
            tbReferenceNumber.Text = selectedBill.ReferenceNumber;
            tbReference.Text = selectedBill.Reference;
            tbOverdueRate.Text = selectedBill.OverdueRate;

            dueDate.SelectedDate = selectedBill.DueDate;
            billDate.SelectedDate = selectedBill.BillDate;

            // Select customer (stupid way, but I don't have time to make it better)
            List<UI.ComboBoxItem> list = new List<UI.ComboBoxItem>();

            foreach (var item in cbCustomers.ItemsSource)
            {
                UI.ComboBoxItem it = (UI.ComboBoxItem)item;

                list.Add(it);
            }

            cbCustomers.SelectedItem = list.Where(x => x.Id == selectedBill.CustomerId).FirstOrDefault();

            // Fill bill rows
            dgBillRows.ItemsSource = selectedBill.BillRows;
        }

        /// <summary>
        /// Fill bill object
        /// </summary>
        /// <param name="bill"></param>
        /// <param name="action"></param>
        private void fillBillObject(ref Bill bill, Enums.Action action)
        {
            bill.BillNumber = tbBillNumber.Text;

            if(dueDate.SelectedDate != null)
                bill.DueDate = DateTime.Parse(dueDate.SelectedDate.Value.ToShortDateString());
            else
            {
                validationErrors.Add("- Laskulle pitää syöttää eräpäivä.");
            }

            if(billDate.SelectedDate != null)
                bill.BillDate = DateTime.Parse(billDate.SelectedDate.Value.ToShortDateString());
            else
            {
                validationErrors.Add("- Laskulle pitää syöttää laskutuspäivä.");
            }

            bill.ReferenceNumber = tbReferenceNumber.Text;
            bill.Reference = tbReference.Text;
            bill.OverdueRate = tbOverdueRate.Text;

            bill.BIC = company.BIC;
            bill.IBAN = company.IBAN;

            // Get selected customer
            UI.ComboBoxItem item = (UI.ComboBoxItem)cbCustomers.SelectedItem;

            if (item != null)
                bill.CustomerId = item.Id;
            else
            {
                validationErrors.Add("- Laskulle pitää valita asiakas.");
            }

            // Bill rows
            bill.BillRows = dgBillRows.ItemsSource as List<BillRow>;                
        }

        /// <summary>
        /// Save a bill
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(selectedBill == null)
                {
                    // New bill
                    Bill bill = new Bill();
                    fillBillObject(ref bill, Enums.Action.Insert);

                    if(validationErrors.Count() == 0)
                    {
                        bill.AddBill();

                        // Get added bill
                        bill.GetBill();
                        selectedBill = bill;

                        // Reset form
                        fillBillForm();

                        // Call parent window for updating list
                        callMainWindow();
                    }
                    else
                    {
                        showError(getErrors(), "Laskun lisäys");
                    }
                    
                }
                else
                {
                    // Update bill
                    fillBillObject(ref selectedBill, Enums.Action.Update);

                    if(validationErrors.Count() == 0)
                    {
                        selectedBill.UpdateBill();

                        // Get updated bill
                        selectedBill.GetBill();

                        // Reset form
                        fillBillForm();
                    }
                    else
                    {
                        showError(getErrors(), "Laskun muokkaus");
                    }

                    // Call parent window for updating list
                    callMainWindow();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        /// <summary>
        /// Delete bill
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (selectedBill == null)
            {
                showError("Laskua ei voida poistaa, koska sitä ei ole tallennettu.", "Laskun poisto");
            }
            else
            {
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Haluatko varmasti poistaa laskun?", "Laskun poisto", System.Windows.MessageBoxButton.YesNo);

                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    try
                    {
                        // Delete bill
                        selectedBill.DeleteBill();
                        selectedBill = null;
                        // Call parent window
                        callMainWindow();
                        // Close this window
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        showError("Virhe: Laskun poisto ei onnistunut.", "Laskun poisto");
                    }
                }
            }
        }

        #endregion Bill methods
    }
}
