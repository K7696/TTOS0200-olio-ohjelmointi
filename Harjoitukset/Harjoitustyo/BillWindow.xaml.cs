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

        private Bill selectedBill;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Default ctor
        /// </summary>
        public BillWindow()
        {
            InitializeComponent();

            Loaded += BillWindow_Loaded;
        }

        #endregion Constructors

        #region Common methods

        /// <summary>
        /// On window loadead
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BillWindow_Loaded(object sender, RoutedEventArgs e)
        {
            fillCustomerCombo();
        }

        #endregion Common methods

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
            bill.DueDate = DateTime.Parse(dueDate.SelectedDate.Value.ToShortDateString());
            bill.BillDate = DateTime.Parse(billDate.SelectedDate.Value.ToShortDateString());
            bill.ReferenceNumber = tbReferenceNumber.Text;
            bill.Reference = tbReference.Text;
            bill.OverdueRate = tbOverdueRate.Text;

            //bill.IBAN
            //bill.BIC

            UI.ComboBoxItem item = (UI.ComboBoxItem)cbCustomers.SelectedItem;
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
