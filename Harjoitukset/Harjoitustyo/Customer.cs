using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

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
                string sql = string.Format("INSERT INTO Customers(Firstname, Lastname) VALUES('{0}', '{1}')", this.Firstname, this.Lastname);
                database.AddNewRecord(sql);
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
                string sql = string.Format("SELECT * FROM Customers WHERE CustomerId = {0}", this.Id);

                DataTable dt = database.GetDataTable(sql);

                foreach (DataRow dr in dt.Rows)
                {
                    this.Id = int.Parse(dr["CustomerId"].ToString());
                    this.Firstname = dr["Firstname"].ToString();
                    this.Lastname = dr["Lastname"].ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion Public methods
    }
}
