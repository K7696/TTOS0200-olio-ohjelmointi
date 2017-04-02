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
        public int Id { get; set; }

        /// <summary>
        /// Number of a product
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// Name of a product
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// VAT percent
        /// </summary>
        public double VATPercent { get; set; }

        /// <summary>
        /// Price
        /// </summary>
        public double Price { get; set; }

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

                string sql = string.Format(@"
INSERT INTO Products(
    ProductNumber,
    ProductName,
    VATPercent,
    Price,
    Created,
    Modified
)
VALUES(
    '{0}',
    '{1}',
    {2},
    {3},
    '{4}',
   '{5}'
)",
this.Number,
this.Name,
this.VATPercent,
this.Price,
inserted.ToString(),
inserted.ToString()
);
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
                string sql = string.Format("SELECT * FROM Products WHERE ProductId = {0}", this.Id);

                DataTable dt = database.GetDataTable(sql);

                foreach (DataRow dr in dt.Rows)
                {
                    this.Id = int.Parse(dr["ProductId"].ToString());
                    this.Number = dr["ProductNumber"].ToString();
                    this.Name = dr["ProductName"].ToString();
                    this.VATPercent = double.Parse(dr["VATPercent"].ToString());
                    this.Price = double.Parse(dr["Price"].ToString());
                    this.Created = DateTime.Parse(dr["Created"].ToString());
                    this.Modified = DateTime.Parse(dr["Created"].ToString());
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
                database.QueryParameters.Add("@ProductNumber", this.Number);
                database.QueryParameters.Add("@ProductName", this.Name);
                database.QueryParameters.Add("@VATPercent", this.VATPercent);
                database.QueryParameters.Add("@Price", this.Price);
                database.QueryParameters.Add("@Modified", DateTime.Now.ToString());
                database.QueryParameters.Add("@Id", this.Id);

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
                string sql = string.Format("DELETE FROM Products WHERE ProductId = {0}", this.Id);
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
