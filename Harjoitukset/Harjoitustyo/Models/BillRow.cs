using System;
using System.Collections.Generic;
using System.Data;

namespace Harjoitustyo
{
    public class BillRow
    {
        #region Fields

        /// <summary>
        /// Instance of database
        /// </summary>
        private Database database;

        #endregion Fields

        #region Properties

        /// <summary>
        /// Id of a bill row
        /// </summary>
        public int BillRowId { get; set; }

        /// <summary>
        /// Id of a bill
        /// </summary>
        public int BillId { get; set; }

        /// <summary>
        /// Id of a product
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// Number of a product
        /// </summary>
        public string ProductNumber { get; set; }

        /// <summary>
        /// Name of a product
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// VAT percent
        /// </summary>
        public string VATPercent { get; set; }

        /// <summary>
        /// Price
        /// </summary>
        public string Price { get; set; }

        /// <summary>
        /// Quantity
        /// </summary>
        public string Quantity { get; set; }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Default ctor
        /// </summary>
        public BillRow()
        {
            database = new Database();
        }

        #endregion Constructors

        #region Public methods

        /// <summary>
        /// Add new bill row
        /// </summary>
        public void AddBillRow()
        {
            try
            {
                string sql = @"
INSERT INTO BillRow(
    BillId,
    ProductId,
    ProductName,
    VATPercent,
    Price,
    Quantity,
    ProductNumber
)
VALUES(
    ?,
    ?,
    ?,
    ?,
    ?,
    ?,
    ?
)"
;
                // Add query parameters (Dont change the order of parameters)
                database.QueryParameters.Add("@BillId", this.BillId);
                database.QueryParameters.Add("@ProductId", this.ProductId);
                database.QueryParameters.Add("@ProductName", this.ProductName);
                database.QueryParameters.Add("@VATPercent", this.VATPercent);
                database.QueryParameters.Add("@Price", this.Price);
                database.QueryParameters.Add("@Quantity", this.Quantity);
                database.QueryParameters.Add("@ProductNumber", this.ProductNumber);

                int billRowId = database.AddNewRecord(sql);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get bill rows
        /// </summary>
        /// <param name="billId"></param>
        /// <returns></returns>
        public List<BillRow> GetBillRows(int billId)
        {
            List<BillRow> list = new List<BillRow>();

            try
            {
                string sql = @"
SELECT 
    * 
FROM 
    BillRow
WHERE 
    BillId = ?";

                // Add query parameters (Dont change the order of parameters)
                database.QueryParameters.Add("@BillId", billId);

                DataTable dt = database.GetDataTable(sql);

                foreach (DataRow dr in dt.Rows)
                {
                    list.Add(new BillRow
                    {
                        BillRowId = int.Parse(dr["BillRowId"].ToString()),
                        BillId = int.Parse(dr["BillId"].ToString()),
                        ProductId = int.Parse(dr["ProductId"].ToString()),
                        Quantity = dr["Quantity"].ToString(),
                        ProductName = dr["ProductName"].ToString(),
                        VATPercent = dr["VATPercent"].ToString(),
                        Price = dr["Price"].ToString(),
                        ProductNumber = dr["ProductNumber"].ToString()
                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return list;
        }

        /// <summary>
        /// Update bill row
        /// </summary>
        public void UpdateBillRow()
        {
            try
            {
                string sql = @"
UPDATE 
    BillRow
SET 
    ProductId = ?,
    ProductName = ?,
    VATPercent = ?,
    Price = ?,
    Quantity = ?,
    ProductNumber = ?
WHERE 
    BillRowId = ?"
    ;
                // Add query parameters (Dont change the order of parameters)
                database.QueryParameters.Add("@ProductId", this.ProductId);
                database.QueryParameters.Add("@ProductName", this.ProductName);
                database.QueryParameters.Add("@VATPercent", this.VATPercent);
                database.QueryParameters.Add("@Price", this.Price);
                database.QueryParameters.Add("@Quantity", this.Quantity);
                database.QueryParameters.Add("@ProductNumber", this.ProductNumber);
                database.QueryParameters.Add("@BillRowId", this.BillRowId);

                database.UpdateRecord(sql);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Delete bill row
        /// </summary>
        public void DeleteBillRow()
        {
            try
            {
                string sql = @"
DELETE FROM 
    BillRow
WHERE 
    BillRowId = ?";

                // Add query parameters (Dont change the order of parameters)
                database.QueryParameters.Add("@BillRowId", this.BillRowId);

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
