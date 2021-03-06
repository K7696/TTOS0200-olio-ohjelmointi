﻿using System;
using System.Data;

namespace Harjoitustyo
{
    public class Address
    {
        #region Fields

        /// <summary>
        /// Instance of database
        /// </summary>
        private Database database;

        #endregion Fields

        #region properties

        /// <summary>
        /// Id of an address
        /// </summary>
        public int AddressId { get; set; }

        /// <summary>
        /// Id of a target entity
        /// </summary>
        public int TargetId { get; set; }

        /// <summary>
        /// Address type: 1 = User's address, 2 = customer's address
        /// </summary>
        public int AddressType { get; set; }

        /// <summary>
        /// Street address
        /// </summary>
        public string StreetAddress { get; set; }

        /// <summary>
        /// Postal code
        /// </summary>
        public string PostalCode { get; set; }

        /// <summary>
        /// City
        /// </summary>
        public string City { get; set; }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Default ctor
        /// </summary>
        public Address()
        {
            database = new Database();
        }

        #endregion Constructors

        #region Public methods

        /// <summary>
        /// Parse address object
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public Address ParseAddressObject(DataRow dr)
        {
            Address address = new Address {
                AddressId = int.Parse(dr["AddressId"].ToString()),
                TargetId = int.Parse(dr["TargetId"].ToString()),
                AddressType = int.Parse(dr["AddressType"].ToString()),
                StreetAddress = dr["StreetAddress"].ToString(),
                PostalCode = dr["PostalCode"].ToString(),
                City = dr["City"].ToString()
            };

            return address;
        }

        /// <summary>
        /// Add new address
        /// </summary>
        /// <returns></returns>
        public int AddAddress()
        {
            int addressId = 0;

            try
            {
                string sql = @"
INSERT INTO Address(
    TargetId, 
    AddressType, 
    StreetAddress, 
    PostalCode, 
    City
) 
VALUES(
    ?, 
    ?, 
    ?, 
    ?, 
    ?
)";

                // Add query parameters (Dont change the order of parameters)
                database.QueryParameters.Add("@TargetId", this.TargetId);
                database.QueryParameters.Add("@AddressType", this.AddressType);
                database.QueryParameters.Add("@StreetAddress", this.StreetAddress);
                database.QueryParameters.Add("@PostalCode", this.PostalCode);
                database.QueryParameters.Add("@City", this.City);

                addressId = database.AddNewRecord(sql);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return addressId;
        }

        /// <summary>
        /// Get address
        /// </summary>
        /// <param name="targetId"></param>
        /// <param name="addressType"></param>
        public void GetAddress(int targetId, int addressType)
        {
            if (targetId <= 0 || addressType <= 0)
                throw new ArgumentOutOfRangeException("Virhe: Osoitteen haku ei onnistunut (virheelliset parametrit).");

            try
            {
                string sql = "SELECT * FROM Address WHERE targetId = ? AND AddressType = ?";

                // Add query parameters (Dont change the order of parameters)
                database.QueryParameters.Add("@TargetId", targetId);
                database.QueryParameters.Add("@AddressType", addressType);

                DataTable dt = database.GetDataTable(sql);

                foreach (DataRow dr in dt.Rows)
                {
                    this.AddressId = int.Parse(dr["AddressId"].ToString());
                    this.TargetId = int.Parse(dr["TargetId"].ToString());
                    this.AddressType = int.Parse(dr["AddressType"].ToString());
                    this.StreetAddress = dr["StreetAddress"].ToString();
                    this.PostalCode = dr["PostalCode"].ToString();
                    this.City = dr["City"].ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        /// <summary>
        /// Update address
        /// </summary>
        public void UpdateAddress()
        {
            try
            {
                string sql = @"
UPDATE 
    Address
SET 
    StreetAddress = ?,
    PostalCode = ?,
    City = ?
WHERE 
    TargetId = ? AND 
    AddressType = ?
";              
                // Add query parameters (Dont change the order of parameters)
                database.QueryParameters.Add("@StreetAddress", this.StreetAddress);
                database.QueryParameters.Add("@PostalCode", this.PostalCode);
                database.QueryParameters.Add("@City", this.City);
                database.QueryParameters.Add("@TargetId", this.TargetId);
                database.QueryParameters.Add("@AddressType", this.AddressType);

                database.UpdateRecord(sql);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Delete address
        /// </summary>
        public void DeleteAddress()
        {
            try
            {
                string sql = "DELETE FROM Address WHERE TargetId = ? AND AddressType = ?";

                // Add query parameters (Dont change the order of parameters)
                database.QueryParameters.Add("@TargetId", this.TargetId);
                database.QueryParameters.Add("@AddressType", this.AddressType);

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
