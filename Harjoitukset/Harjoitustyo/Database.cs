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
        #region Fields

        /// <summary>
        /// Connectionstring for db
        /// </summary>
        private string connectionString;

        /// <summary>
        /// Database connection
        /// </summary>
        private OleDbConnection connection;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Default ctor
        /// </summary>
        public Database()
        {
            // TODO: Lue app.confista
            connectionString = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + AppDomain.CurrentDomain.BaseDirectory + "\\Talous.accdb";

            connection = new OleDbConnection();
            connection.ConnectionString = connectionString;
        }

        #endregion Constructors

        #region Public methods

        /// <summary>
        /// Get records from a database
        /// </summary>
        /// <param name="sql">SQL-Query</param>
        /// <returns></returns>
        public DataTable GetDataTable(string sql)
        {
            DataTable dt = new DataTable();

            try
            {
                OleDbCommand cmd = new OleDbCommand();

                if (connection.State != ConnectionState.Open)
                    connection.Open();

                cmd.Connection = connection;
                cmd.CommandText = sql;
                OleDbDataAdapter da = new OleDbDataAdapter(cmd);

                da.Fill(dt);

                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Virhe: Tietokannasta luku epäonnistui.");
            }

            return dt;
        }

        /// <summary>
        /// Add new record to a database
        /// </summary>
        /// <param name="sql">SQL-Query</param>
        public void AddNewRecord(string sql)
        {
            try
            {
                OleDbCommand cmd = new OleDbCommand();

                if (connection.State != ConnectionState.Open)
                    connection.Open();

                cmd.Connection = connection;
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Virhe: Tietokantaan lisäys epäonnistui.");
            }
        }

        #endregion Public methods
    }
}
