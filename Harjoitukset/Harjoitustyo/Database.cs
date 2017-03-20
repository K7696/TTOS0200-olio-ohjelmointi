using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Harjoitustyo
{
    /// <summary>
    /// accdb-format neeeds this: https://social.msdn.microsoft.com/Forums/en-US/1d5c04c7-157f-4955-a14b-41d912d50a64/how-to-fix-error-the-microsoftaceoledb120-provider-is-not-registered-on-the-local-machine?forum=vstsdb
    /// Microsoft Access Database Engine 2010 Redistributable 
    /// 2007 Office System Driver: Data Connectivity Components 
    /// 
    /// Access CRUD: http://www.codescratcher.com/wpf/add-edit-delete-data-wpf-access-database/
    /// </summary>
    public class Database
    {
        #region Constructors

        /// <summary>
        /// Default ctor
        /// </summary>
        public Database()
        {

        }

        #endregion Constructors

        #region Public methods

        /// <summary>
        /// Get datatable by sql
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public DataTable GetDataTable(string sql)
        {
            OleDbCommand cmd = new OleDbCommand();
            OleDbConnection con = new OleDbConnection();
            con.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + AppDomain.CurrentDomain.BaseDirectory + "\\Talous.accdb";

            if (con.State != ConnectionState.Open)
                con.Open();
            cmd.Connection = con;
            cmd.CommandText = sql;
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            return dt;
        }


        #endregion Public methods
    }
}
