using System;
using System.Data;

namespace Harjoitustyo
{
    public class Customer : Person
    {
        #region Fields

        /// <summary>
        /// Instance of database
        /// </summary>
        private Database database;

        #endregion Fields

        #region Properties

        /// <summary>
        /// CustomerId
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Company customer name
        /// </summary>
        public string Company { get; set; }

        /// <summary>
        /// Invoicing address
        /// </summary>
        public Address InvoicingAddress { get; set; }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Default ctor
        /// </summary>
        public Customer()
        {
            database = new Database();

            this.InvoicingAddress = new Address();
        }

        #endregion Constructors

        #region Public methods

        /// <summary>
        /// Save new customer
        /// </summary>
        public void AddCustomer()
        {
            try
            {
                string sql = string.Format(@"
INSERT INTO Customers(
    Firstname, 
    Lastname,
    Company,
    Email,
    Phonenumber
) 
VALUES(
    '{0}', 
    '{1}',
    '{2}',
    '{3}',
    '{4}'
)",
this.Firstname,
this.Lastname,
this.Company,
this.Email,
this.Phonenumber);

                // First add customer
                int customerId = database.AddNewRecord(sql);

                // Then add address
                this.InvoicingAddress.TargetId = customerId;
                this.InvoicingAddress.AddressType = 2;
                this.InvoicingAddress.AddAddress();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        /// <summary>
        /// Get customer
        /// </summary>
        public void GetCustomer()
        {
            try
            {
                // First get customer
                string sql = string.Format("SELECT * FROM Customers WHERE CustomerId = {0}", this.Id);

                DataTable dt = database.GetDataTable(sql);

                foreach (DataRow dr in dt.Rows)
                {
                    this.Id = int.Parse(dr["CustomerId"].ToString());
                    this.Firstname = dr["Firstname"].ToString();
                    this.Lastname = dr["Lastname"].ToString();
                }

                // Then get address
                this.InvoicingAddress.GetAddress(this.Id, 2);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Update customer
        /// </summary>
        public void UpdateCustomer()
        {
            try
            {
                // First update customer
                string sql = string.Format(@"
UPDATE 
    Customers 
SET 
    Firstname = '{1}',
    Lastname = '{2}',
    Company = '{3}',
    Email = '{4}',
    Phonenumber = '{5}'
WHERE 
    CustomerId = {0}", 
    this.Id,
    this.Firstname,
    this.Lastname,
    this.Company,
    this.Email,
    this.Phonenumber
    );

                database.UpdateRecord(sql);

                // Then update address
                this.InvoicingAddress.UpdateAddress();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Delete customer
        /// </summary>
        public void DeleteCustomer()
        {
            try
            {
                string sql = string.Format("DELETE FROM Customers WHERE CustomerId = {0}", this.Id);
                database.DeleteRecord(sql);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion Public methods
    }
}
