using System;
using System.Data;

namespace Harjoitustyo
{
    class Product
    {
        #region Fields

        /// <summary>
        /// Instance of database
        /// </summary>
        private Database database;

        #endregion Fields

        #region Properties

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
        /// Created
        /// </summary>
        public DateTime Created { get; set; }

        /// <summary>
        /// Modified
        /// </summary>
        public DateTime Modified { get; set; }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Default ctor
        /// </summary>
        public Product()
        {
            database = new Database();
        }

        #endregion Constructors

        #region Public methods

        /// <summary>
        /// Add new product
        /// </summary>
        public void AddProduct()
        {
            try
            {
                DateTime inserted = DateTime.Now;

                string sql = @"
INSERT INTO Products(
    ProductNumber,
    ProductName,
    VATPercent,
    Price,
    Created,
    Modified
)
VALUES(
    ?,
    ?,
    ?,
    ?,
    ?,
    ?
)"
;
                // Add query parameters (Dont change the order of parameters)
                database.QueryParameters.Add("@ProductNumber", this.ProductNumber);
                database.QueryParameters.Add("@ProductName", this.ProductName);
                database.QueryParameters.Add("@VATPercent", this.VATPercent);
                database.QueryParameters.Add("@Price", this.Price);
                database.QueryParameters.Add("@Created", inserted);
                database.QueryParameters.Add("@Modified", inserted);

                int productId = database.AddNewRecord(sql);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get product
        /// </summary>
        public void GetProduct()
        {
            try
            {
                string sql = "SELECT * FROM Products WHERE ProductId = ?";

                // Add query parameters (Dont change the order of parameters)
                database.QueryParameters.Add("@ProductId", this.ProductId);

                DataTable dt = database.GetDataTable(sql);

                foreach (DataRow dr in dt.Rows)
                {
                    this.ProductId = int.Parse(dr["ProductId"].ToString());
                    this.ProductNumber = dr["ProductNumber"].ToString();
                    this.ProductName = dr["ProductName"].ToString();
                    this.VATPercent = dr["VATPercent"].ToString();
                    this.Price = dr["Price"].ToString();
                    this.Created = DateTime.Parse(dr["Created"].ToString());
                    this.Modified = DateTime.Parse(dr["Modified"].ToString());
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Update product
        /// </summary>
        public void UpdateProduct()
        {
            try
            {
                string sql = @"
UPDATE 
    Products
SET 
    ProductNumber = ?,
    ProductName = ?,
    VATPercent = ?,
    Price = ?,
    Modified = ?
WHERE 
    ProductId = ?"
    ;
                // Add query parameters (Dont change the order of parameters)
                database.QueryParameters.Add("@ProductNumber", this.ProductNumber);
                database.QueryParameters.Add("@ProductName", this.ProductName);
                database.QueryParameters.Add("@VATPercent", this.VATPercent);
                database.QueryParameters.Add("@Price", this.Price);
                database.QueryParameters.Add("@Modified", DateTime.Now.ToString());
                database.QueryParameters.Add("@ProductId", this.ProductId);

                database.UpdateRecord(sql);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Delete product
        /// </summary>
        public void DeleteProduct()
        {
            try
            {
                string sql = "DELETE FROM Products WHERE ProductId = ?";

                // Add query parameters (Dont change the order of parameters)
                database.QueryParameters.Add("@ProductId", this.ProductId);

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
