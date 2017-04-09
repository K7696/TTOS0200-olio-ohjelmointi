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
        public int CustomerId { get; set; }

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
        /// Parse customer object
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public Customer ParseCustomerObject(DataRow dr)
        {
            Customer customer = new Customer {
                CustomerId = int.Parse(dr["CustomerId"].ToString()),
                Firstname = dr["Firstname"].ToString(),
                Lastname = dr["Lastname"].ToString(),
                InvoicingAddress = InvoicingAddress.ParseAddressObject(dr)
            };

            return customer;
        }

        /// <summary>
        /// Save new customer
        /// </summary>
        public void AddCustomer()
        {
            try
            {
                string sql = @"
INSERT INTO Customers(
    Firstname, 
    Lastname,
    Company,
    Email,
    Phonenumber
) 
VALUES(
    ?, 
    ?,
    ?,
    ?,
    ?
)";
                // Add query parameters (Dont change the order of parameters)
                database.QueryParameters.Add("@Firstname", this.Firstname);
                database.QueryParameters.Add("@Lastname", this.Lastname);
                database.QueryParameters.Add("@Company", this.Company);
                database.QueryParameters.Add("@Email", this.Email);
                database.QueryParameters.Add("@Phonenumber", this.Phonenumber);

                // First add customer
                int customerId = database.AddNewRecord(sql);

                // Then add address
                this.InvoicingAddress.TargetId = customerId;
                this.InvoicingAddress.AddressType = (int)Enums.AddressType.CustomerAddress;
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
        /// <param name="customerId"></param>
        public Customer GetCustomer(int customerId)
        {
            try
            {
                Customer customer = new Customer();

                // First get customer
                string sql = "SELECT * FROM Customers WHERE CustomerId = ?";

                // Add query parameters (Dont change the order of parameters)
                database.QueryParameters.Add("@CustomerId", customerId);

                DataTable dt = database.GetDataTable(sql);

                foreach (DataRow dr in dt.Rows)
                {
                    customer.CustomerId = int.Parse(dr["CustomerId"].ToString());
                    customer.Firstname = dr["Firstname"].ToString();
                    customer.Lastname = dr["Lastname"].ToString();
                }

                // Then get address
                if(customer != null)
                    customer.InvoicingAddress.GetAddress(customer.CustomerId, (int)Enums.AddressType.CustomerAddress);

                return customer;
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
                string sql = "SELECT * FROM Customers WHERE CustomerId = ?";

                // Add query parameters (Dont change the order of parameters)
                database.QueryParameters.Add("@CustomerId", this.CustomerId);

                DataTable dt = database.GetDataTable(sql);

                foreach (DataRow dr in dt.Rows)
                {
                    this.CustomerId = int.Parse(dr["CustomerId"].ToString());
                    this.Firstname = dr["Firstname"].ToString();
                    this.Lastname = dr["Lastname"].ToString();
                }

                // Then get address
                this.InvoicingAddress.GetAddress(this.CustomerId, (int)Enums.AddressType.CustomerAddress);
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
                string sql = @"
UPDATE 
    Customers 
SET 
    Firstname = ?,
    Lastname = ?,
    Company = ?,
    Email = ?,
    Phonenumber = ?
WHERE 
    CustomerId = ?"
    ;

                // Add query parameters (Dont change the order of parameters)
                database.QueryParameters.Add("@Firstname", this.Firstname);
                database.QueryParameters.Add("@Lastname", this.Lastname);
                database.QueryParameters.Add("@Company", this.Company);
                database.QueryParameters.Add("@Email", this.Email);
                database.QueryParameters.Add("@Phonenumber", this.Phonenumber);
                database.QueryParameters.Add("@Id", this.CustomerId);

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
                // Remove customer
                string sql = "DELETE FROM Customers WHERE CustomerId = ?";

                // Add query parameters (Dont change the order of parameters)
                database.QueryParameters.Add("@CustomerId", this.CustomerId);

                database.DeleteRecord(sql);

                // Remove address
                this.InvoicingAddress.DeleteAddress();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion Public methods
    }
}
