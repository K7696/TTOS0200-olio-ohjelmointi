using System;
using System.Collections.Generic;
using System.Data;

namespace Harjoitustyo
{
    public class Bill
    {
        #region Fields

        /// <summary>
        /// Instance of database
        /// </summary>
        private Database database;

        #endregion Fields

        #region Properties

        /// <summary>
        /// Id of a bill
        /// </summary>
        public int BillId { get; set; }

        /// <summary>
        /// Number of a bill
        /// </summary>
        public string BillNumber { get; set; }

        /// <summary>
        /// Duedate
        /// </summary>
        public DateTime DueDate { get; set; }

        /// <summary>
        /// Billing date
        /// </summary>
        public DateTime BillDate { get; set; }

        /// <summary>
        /// IBAN
        /// </summary>
        public string IBAN { get; set; }

        /// <summary>
        /// BIC
        /// </summary>
        public string BIC { get; set; }

        /// <summary>
        /// Reference number
        /// </summary>
        public string ReferenceNumber { get; set; }

        /// <summary>
        /// Reference
        /// </summary>
        public string Reference { get; set; }

        /// <summary>
        /// Id of a customer
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// Customer entity
        /// </summary>
        public Customer Customer { get; set; }

        /// <summary>
        /// Created
        /// </summary>
        public DateTime Created { get; set; }

        /// <summary>
        /// Modified
        /// </summary>
        public DateTime Modified { get; set; }

        /// <summary>
        /// Overdue rate
        /// </summary>
        public string OverdueRate { get; set; }

        /// <summary>
        /// Bill rows
        /// </summary>
        public List<BillRow> BillRows { get; set; }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Default ctor
        /// </summary>
        public Bill()
        {
            database = new Database();

            Customer = new Customer();

            BillRows = new List<BillRow>();
        }

        #endregion Constructors

        #region Public methods

        /// <summary>
        /// Parse bill object from DataRow
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public Bill ParseBillObject(DataRow dr)
        {
            Bill bill = new Bill
            {
                BillId = int.Parse(dr["BillId"].ToString()),
                BillNumber = dr["BillNumber"].ToString(),
                DueDate = DateTime.Parse(dr["DueDate"].ToString()),
                BillDate = DateTime.Parse(dr["BillDate"].ToString()),
                IBAN = dr["IBAN"].ToString(),
                BIC = dr["BIC"].ToString(),
                ReferenceNumber = dr["ReferenceNumber"].ToString(),
                Reference = dr["Reference"].ToString(),
                CustomerId = int.Parse(dr["CustomerId"].ToString()),
                Created = DateTime.Parse(dr["Created"].ToString()),
                Modified = DateTime.Parse(dr["Modified"].ToString()),
                OverdueRate = dr["OverdueRate"].ToString(),
                Customer = Customer.ParseCustomerObject(dr)
            };

            return bill;
        }

        /// <summary>
        /// Add new bill
        /// </summary>
        public void AddBill()
        {
            try
            {
                DateTime inserted = DateTime.Now;

                string sql = @"
INSERT INTO Bills(
    BillNumber,
    DueDate,
    BillDate,
    IBAN,
    BIC,
    ReferenceNumber,
    Reference,
    CustomerId,
    Created,
    Modified,
    OverdueRate
)
VALUES(
    ?,
    ?,
    ?,
    ?,
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
                database.QueryParameters.Add("@BillNumber", this.BillNumber);
                database.QueryParameters.Add("@DueDate", this.DueDate);
                database.QueryParameters.Add("@BillDate", this.BillDate);
                database.QueryParameters.Add("@IBAN", this.IBAN);
                database.QueryParameters.Add("@BIC", this.BIC);
                database.QueryParameters.Add("@ReferenceNumber", this.ReferenceNumber);
                database.QueryParameters.Add("@Reference", this.Reference);
                database.QueryParameters.Add("@CustomerId", this.CustomerId);
                database.QueryParameters.Add("@Created", inserted);
                database.QueryParameters.Add("@Modified", inserted);
                database.QueryParameters.Add("@OverdueRate", this.OverdueRate);

                this.BillId = database.AddNewRecord(sql);

                // Add bill rows
                if(this.BillRows != null && this.BillRows.Count > 0)
                {
                    foreach (BillRow item in BillRows)
                    {
                        item.BillId = this.BillId;
                        item.AddBillRow();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get a bill
        /// </summary>
        public void GetBill()
        {
            try
            {
                string sql = @"
SELECT 
    b.*
FROM 
    Bills b
WHERE 
    b.BillId = ?
ORDER BY 
    b.BillId ASC";

                // Add query parameters (Dont change the order of parameters)
                database.QueryParameters.Add("@BillId", this.BillId);

                // First get bill
                DataTable dt = database.GetDataTable(sql);

                foreach (DataRow dr in dt.Rows)
                {
                    this.BillId = int.Parse(dr["BillId"].ToString());
                    this.BillNumber = dr["BillNumber"].ToString();
                    this.DueDate = DateTime.Parse(dr["DueDate"].ToString());
                    this.BillDate = DateTime.Parse(dr["BillDate"].ToString());
                    this.IBAN = dr["IBAN"].ToString();
                    this.BIC = dr["BIC"].ToString();
                    this.ReferenceNumber = dr["ReferenceNumber"].ToString();
                    this.Reference = dr["Reference"].ToString();
                    this.CustomerId = int.Parse(dr["CustomerId"].ToString());
                    this.Created = DateTime.Parse(dr["Created"].ToString());
                    this.Modified = DateTime.Parse(dr["Modified"].ToString());
                    this.OverdueRate = dr["OverdueRate"].ToString();
                }

                // Then get customer
                if (this != null)
                    this.Customer = Customer.GetCustomer(this.CustomerId);

                // Then get billrows
                if(this != null)
                {
                    BillRow billRow = new BillRow();

                    this.BillRows = billRow.GetBillRows(this.BillId);
                }
                    
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Update a bill
        /// </summary>
        public void UpdateBill()
        {
            try
            {
                string sql = @"
UPDATE 
    Bills
SET 
    BillNumber = ?,
    DueDate = ?,
    BillDate = ?,
    IBAN = ?,
    BIC = ?,
    ReferenceNumber = ?,
    Reference = ?,
    CustomerId = ?,
    Modified = ?,
    OverdueRate = ?
WHERE 
    BillId = ?"
    ;
                // Add query parameters (Dont change the order of parameters)
                database.QueryParameters.Add("@BillNumber", this.BillNumber);
                database.QueryParameters.Add("@DueDate", this.DueDate);
                database.QueryParameters.Add("@BillDate", this.BillDate);
                database.QueryParameters.Add("@IBAN", this.IBAN);
                database.QueryParameters.Add("@BIC", this.BIC);
                database.QueryParameters.Add("@ReferenceNumber", this.ReferenceNumber);
                database.QueryParameters.Add("@Reference", this.Reference);
                database.QueryParameters.Add("@CustomerId", this.CustomerId);
                database.QueryParameters.Add("@Modified", DateTime.Now);
                database.QueryParameters.Add("@OverdueRate", this.OverdueRate);
                database.QueryParameters.Add("@BillId", this.BillId);

                database.UpdateRecord(sql);

                // Add/update bill rows
                if (this.BillRows != null && this.BillRows.Count > 0)
                {
                    foreach (BillRow item in BillRows)
                    {
                        if (item.BillRowId > 0 && item.DeleteRow == false)
                            item.UpdateBillRow();
                        else if (item.BillRowId > 0 && item.DeleteRow == true)
                            item.DeleteBillRow();
                        else if (item.BillRowId < 1 && item.DeleteRow == false)
                        {
                            item.BillId = this.BillId;
                            item.AddBillRow();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Delete a bill
        /// </summary>
        public void DeleteBill()
        {
            try
            {
                string sql = @"
DELETE FROM 
    Bills 
WHERE 
    BillId = ?";

                // Add query parameters (Dont change the order of parameters)
                database.QueryParameters.Add("@BillId", this.BillId);

                database.DeleteRecord(sql);

                if(this.BillRows.Count > 0)
                {
                    BillRow billRow = new BillRow();
                    billRow.DeleteBillRowsByBillId(this.BillId);
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
