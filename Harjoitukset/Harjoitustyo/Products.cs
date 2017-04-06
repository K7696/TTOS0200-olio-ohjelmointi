using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Harjoitustyo
{
    class Products : ObservableCollection<Product>
    {
        #region Fields

        private Database database;

        #endregion Fields

        #region Properties

        /// <summary>
        /// Product list
        /// </summary>
        public List<Product> ProductList { get; set; }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Default ctor
        /// </summary>
        public Products()
        {
            ProductList = new List<Product>();

            database = new Database();
        }

        #endregion Constructors

        #region Public methods

        /// <summary>
        /// Get products
        /// </summary>
        /// <returns></returns>
        public List<Product> GetProducts()
        {
            string sql = "SELECT p.* FROM Products p";

            DataTable dt = database.GetDataTable(sql);

            foreach (DataRow dr in dt.Rows)
            {
                this.ProductList.Add(new Product
                {
                    ProductId = int.Parse(dr["ProductId"].ToString()),
                    ProductNumber = dr["ProductNumber"].ToString(),
                    ProductName = dr["ProductName"].ToString(),
                    VATPercent = dr["VATPercent"].ToString(),
                    Price = dr["Price"].ToString(),
                    Created = DateTime.Parse(dr["Created"].ToString()),
                    Modified = DateTime.Parse(dr["Modified"].ToString())
            });
            }

            return this.ProductList;
        }

        #endregion Public methods
    }
}
