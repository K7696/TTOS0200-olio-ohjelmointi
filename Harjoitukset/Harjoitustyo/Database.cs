using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;

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

        #region Properties

        /// <summary>
        /// Query parameters
        /// https://www.mikesdotnetting.com/article/26/parameter-queries-in-asp-net-with-ms-access
        /// </summary>
        public Dictionary<string, object> QueryParameters { get; set; }

        #endregion Properties

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

            QueryParameters = new Dictionary<string, object>();
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
        public int AddNewRecord(string sql)
        {
            int newId = 0;
            try
            {
                OleDbCommand cmd = new OleDbCommand();

                if (connection.State != ConnectionState.Open)
                    connection.Open();

                cmd.Connection = connection;
                cmd.CommandText = sql;
                cmd.ExecuteScalar();

                using (OleDbCommand command = new OleDbCommand("SELECT @@IDENTITY;", connection))
                {
                    newId = (int)command.ExecuteScalar();
                }

                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Virhe: Tietokantaan lisäys epäonnistui.");
            }

            return newId;
        }

        /// <summary>
        /// Update database record
        /// </summary>
        /// <param name="sql">SQL-Query</param>
        public void UpdateRecord(string sql)
        {
            try
            {
                using(OleDbCommand cmd = new OleDbCommand(sql, connection))
                {
                    cmd.CommandType = CommandType.Text;

                    // Loop through possible query parameters
                    foreach (var item in QueryParameters)
                    {
                        cmd.Parameters.AddWithValue(item.Key, item.Value);
                    }

                    connection.Open();
                    cmd.ExecuteNonQuery();
                    cmd.Connection.Close();
                }

                // Clear dictionary
                this.QueryParameters.Clear();
            }
            catch (Exception ex)
            {
                throw new Exception("Virhe: Tietokantaan päivitys epäonnistui.");
            }
        }

        /// <summary>
        /// Delete record from a database
        /// </summary>
        /// <param name="sql">SQL-Query</param>
        public void DeleteRecord(string sql)
        {
            try
            {
                using (OleDbCommand cmd = new OleDbCommand(sql, connection))
                {
                    cmd.CommandType = CommandType.Text;

                    // Loop through possible query parameters
                    foreach (var item in QueryParameters)
                    {
                        cmd.Parameters.AddWithValue(item.Key, item.Value);
                    }

                    connection.Open();
                    cmd.ExecuteNonQuery();
                    cmd.Connection.Close();
                }

                // Clear dictionary
                this.QueryParameters.Clear();
            }
            catch (Exception ex)
            {
                throw new Exception("Virhe: Tietokannasta poistaminen epäonnistui.");
            }
        }

        #endregion Public methods
    }
}
