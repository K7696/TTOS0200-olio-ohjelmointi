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
            DataTable dt = database.GetDataTable("SELECT p.* FROM Products p");

            foreach (DataRow dr in dt.Rows)
            {
                this.ProductList.Add(new Product
                {
                    Id = int.Parse(dr["ProductId"].ToString()),
                    Number = dr["ProductNumber"].ToString(),
                    Name = dr["ProductName"].ToString(),
                    VATPercent = double.Parse(dr["VATPercent"].ToString()),
                    Price = double.Parse(dr["Price"].ToString()),
                    Created = DateTime.Parse(dr["Created"].ToString()),
                    Modified = DateTime.Parse(dr["Created"].ToString())
            });
            }

            return this.ProductList;
        }

        #endregion Public methods
    }
}
