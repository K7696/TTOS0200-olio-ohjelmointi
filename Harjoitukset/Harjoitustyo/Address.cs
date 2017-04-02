using System;
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
        /// Add new address
        /// </summary>
        /// <returns></returns>
        public int AddAddress()
        {
            int addressId = 0;

            try
            {
                string sql = string.Format("INSERT INTO Address(TargetId, AddressType, StreetAddress, PostalCode, City) VALUES({0}, {1}, '{2}', '{3}', '{4}')", 
                    this.TargetId, 
                    this.AddressType,
                    this.StreetAddress,
                    this.PostalCode,
                    this.City);

                addressId = database.AddNewRecord(sql);
            }
            catch (Exception ex)
            {
                throw new Exception("Virhe: Osoitteen lisäys ei onnistunut.");
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
                string sql = string.Format("SELECT * FROM Address WHERE targetId = {0} AND AddressType = {1}", targetId, addressType);

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
                throw new Exception("Virhe: Osoitteen haku ei onnistunut.");
            }
            
        }

        /// <summary>
        /// Update address
        /// </summary>
        public void UpdateAddress()
        {
            try
            {

            }
            catch (Exception ex)
            {
                throw new Exception("Virhe: Osoitteen päivitys ei onnistunut.");
            }
        }

        #endregion Public methods
    }
}
