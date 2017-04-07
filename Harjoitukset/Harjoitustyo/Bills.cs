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
            string sql = @"
SELECT 
    b.* 
FROM 
    Bills b 
ORDER BY 
    b.BillId ASC";

            DataTable dt = database.GetDataTable(sql);

            Bill bill = new Bill();

            foreach (DataRow dr in dt.Rows)
            {
                this.BillList.Add(bill.ParseBillObject(dr));
            }

            return this.BillList;
        }
    }

    #endregion Public methods
}
