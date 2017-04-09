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
        private MainWindow mainWindow;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Default ctor
        /// </summary>
        public BillWindow(MainWindow main)
        {
            InitializeComponent();

            mainWindow = main;
            Owner = main;

            company = new Company();

            Loaded += BillWindow_Loaded;
        }

        #endregion Constructors

        #region Common methods

        /// <summary>
        /// 
        /// </summary>
        private void callMainWindow()
        {
            mainWindow.Call();
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
        /// Fill bill object
        /// </summary>
        /// <param name="bill"></param>
        private void fillBillObject(ref Bill bill)
        {
            bill.BillNumber = tbBillNumber.Text;

            if(dueDate.SelectedDate != null)
                bill.DueDate = DateTime.Parse(dueDate.SelectedDate.Value.ToShortDateString());

            if(billDate.SelectedDate != null)
                bill.BillDate = DateTime.Parse(billDate.SelectedDate.Value.ToShortDateString());

            bill.ReferenceNumber = tbReferenceNumber.Text;
            bill.Reference = tbReference.Text;
            bill.OverdueRate = tbOverdueRate.Text;

            bill.BIC = company.BIC;
            bill.IBAN = company.IBAN;

            UI.ComboBoxItem item = (UI.ComboBoxItem)cbCustomers.SelectedItem;

            if (item != null)
                bill.CustomerId = item.Id;
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
                    fillBillObject(ref bill);
                    bill.AddBill();

                    callMainWindow();
                }
                else
                {
                    // Update bill

                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        #endregion Bill methods

    }
}
