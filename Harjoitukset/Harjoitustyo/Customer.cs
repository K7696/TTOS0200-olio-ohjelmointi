﻿using System;
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
                // First add customer
                string sql = string.Format("INSERT INTO Customers(Firstname, Lastname) VALUES('{0}', '{1}')", this.Firstname, this.Lastname);
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
                string sql = string.Format(@"
UPDATE 
    Customers 
SET 
    Firstname = '{1}',
    Lastname = '{2}'
WHERE 
    CustomerId = {0}", 
    this.Id,
    this.Firstname,
    this.Lastname
    );

                database.UpdateRecord(sql);
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
