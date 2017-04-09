using System.Collections.Generic;
using System.Data;

namespace Harjoitustyo
{
    public class Bills
    {
        #region Fields

        private Database database;

        #endregion Fields

        #region Properties

        /// <summary>
        /// Product list
        /// </summary>
        public List<Bill> BillList { get; set; }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Default ctor
        /// </summary>
        public Bills()
        {
            BillList = new List<Bill>();

            database = new Database();
        }

        #endregion Constructors

        #region Public methods

        /// <summary>
        /// Get products
        /// </summary>
        /// <returns></returns>
        public List<Bill> GetBills()
        {
            try
            {
                string sql = @"
SELECT 
    b.*,
    c.Company,
    c.Firstname,
    c.Lastname,
    a.*
FROM 
    Bills b,
    Customers c,
    Address a
WHERE
    c.CustomerId = b.CustomerId AND
    c.CustomerId = a.TargetId AND 
    a.AddressType = ?
ORDER BY 
    b.BillId ASC";

                // Add query parameters (Dont change the order of parameters)
                database.QueryParameters.Add("@AddressType", (int)Enums.AddressType.CustomerAddress);

                DataTable dt = database.GetDataTable(sql);

                Bill bill = new Bill();

                foreach (DataRow dr in dt.Rows)
                {
                    this.BillList.Add(bill.ParseBillObject(dr));
                }

                return this.BillList;
            }
            catch (System.Exception ex)
            {

                throw;
            }
        }
    }

    #endregion Public methods
}
