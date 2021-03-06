﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;

namespace Harjoitustyo
{
    public class Customers //: ObservableCollection<Customer>
    {
        #region Fields

        private Database database;

        #endregion Fields

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

            database = new Database();
        }

        #endregion Constructors

        #region Public methods

        /// <summary>
        /// Get customers
        /// </summary>
        /// <returns></returns>
        public List<Customer> GetCustomers()
        {
           string sql = @"
SELECT 
    c.*,
    a.* 
FROM 
    Customers c,
    Address a
WHERE 
    a.TargetId = c.CustomerId AND a.AddressType = ?
ORDER BY 
    c.CustomerId ASC";

            // Add query parameters (Dont change the order of parameters)
            database.QueryParameters.Add("@AddressType", (int)Enums.AddressType.CustomerAddress);

            DataTable dt = database.GetDataTable(sql);

            foreach (DataRow dr in dt.Rows)
            {
                this.CustomerList.Add(new Customer {
                    CustomerId = int.Parse(dr["CustomerId"].ToString()),
                    Company = dr["Company"].ToString(),
                    Firstname = dr["Firstname"].ToString(),
                    Lastname = dr["Lastname"].ToString(),
                    Email = dr["Email"].ToString(),
                    Phonenumber = dr["Phonenumber"].ToString(),
                    InvoicingAddress = new Address() {
                        AddressId = int.Parse(dr["AddressId"].ToString()),
                        TargetId = int.Parse(dr["TargetId"].ToString()),
                        AddressType = int.Parse(dr["AddressType"].ToString()),
                        PostalCode = dr["PostalCode"].ToString(),
                        StreetAddress = dr["StreetAddress"].ToString(),
                        City = dr["City"].ToString()
                    }
                });
            }

            return this.CustomerList;
        }

        #endregion Public methods
    }
}
