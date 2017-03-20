using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Harjoitustyo
{
    public class Customers : ObservableCollection<Customer>
    {
        #region Properties

        /// <summary>
        /// Customer list
        /// </summary>
        public List<Customer> CustomerList { get; set; }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public Customers()
        {
            CustomerList = new List<Customer>();
        }

        #endregion Constructors

        #region Public methods

        /// <summary>
        /// Get customers
        /// </summary>
        /// <returns></returns>
        public List<Customer> GetCustomers()
        {
            Database db = new Database();
            DataTable dt = db.GetDataTable("Select * FROM Customers");

            foreach (DataRow dr in dt.Rows)
            {
                this.CustomerList.Add(new Customer {
                    Id = int.Parse(dr["CustomerId"].ToString()),
                    Firstname = dr["Firstname"].ToString(),
                    Lastname = dr["Lastname"].ToString(),
                    InvoicingAddress = new Address() {
                        AddressId = 10000
                    }
                });
            }

            return this.CustomerList;
        }

        #endregion Public methods
    }
}
