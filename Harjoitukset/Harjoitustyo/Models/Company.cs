using System;
using System.Data;

namespace Harjoitustyo
{
    public class Company
    {
        #region Fields

        /// <summary>
        /// Instance of database
        /// </summary>
        private Database database;

        #endregion Fields

        #region Properties

        /// <summary>
        /// Id of a company
        /// </summary>
        public int? CompanyId { get; set; }

        /// <summary>
        /// Name of a company
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// Name of a contact person
        /// </summary>
        public string ContactPersonName { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Phonenumber
        /// </summary>
        public string Phonenumber { get; set; }

        /// <summary>
        /// Address
        /// </summary>
        public Address Address { get; set; }

        /// <summary>
        /// IBAN
        /// </summary>
        public string IBAN { get; set; }

        /// <summary>
        /// BIC
        /// </summary>
        public string BIC { get; set; }


        #endregion Properties

        #region Constructors

        /// <summary>
        /// Default ctor
        /// </summary>
        public Company()
        {
            database = new Database();
            Address = new Address();
        }

        #endregion Constructors

        #region Private methods

        /// <summary>
        /// Add company data
        /// </summary>
        private void addCompany()
        {
            try
            {
                string sql = @"
INSERT INTO Company(
    CompanyName,
    ContactPersonName,
    Email,
    Phonenumber,
    IBAN,
    BIC
)
VALUES(
    ?,
    ?,
    ?,
    ?,
    ?,
    ?
)
";

                // Add query parameters (Dont change the order of parameters)
                database.QueryParameters.Add("@CompanyName", this.CompanyName);
                database.QueryParameters.Add("@ContactPersonName", this.ContactPersonName);
                database.QueryParameters.Add("@Email", this.Email);
                database.QueryParameters.Add("@Phonenumber", this.Phonenumber);
                database.QueryParameters.Add("@IBAN", this.IBAN);
                database.QueryParameters.Add("@BIC", this.BIC);

                // First add company
                int companyId = database.AddNewRecord(sql);

                // Then add address
                this.Address.TargetId = companyId;
                this.Address.AddressType = (int)Enums.AddressType.OwnAddress;
                this.Address.AddAddress();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        /// <summary>
        /// Update company data
        /// </summary>
        private void updateCompany()
        {
            try
            {
                string sql = @"
UPDATE  
    Company 
SET 
    CompanyName = ?,
    ContactPersonName = ?,
    Email = ?,
    Phonenumber = ?,
    IBAN = ?,
    BIC = ?
WHERE 
    CompanyId = ?
";
                // Add query parameters (Dont change the order of parameters)
                database.QueryParameters.Add("@CompanyName", this.CompanyName);
                database.QueryParameters.Add("@ContactPersonName", this.ContactPersonName);
                database.QueryParameters.Add("@Email", this.Email);
                database.QueryParameters.Add("@Phonenumber", this.Phonenumber);
                database.QueryParameters.Add("@IBAN", this.IBAN);
                database.QueryParameters.Add("@BIC", this.BIC);
                database.QueryParameters.Add("@CompanyId", this.CompanyId);

                // First update company
                database.UpdateRecord(sql);

                // Then update address
                this.Address.UpdateAddress();
            }
            catch (Exception ex)
            {

                throw;
            }
        }


        #endregion Private methods

        #region Public methods

        /// <summary>
        /// Get company data
        /// </summary>
        public void GetCompany()
        {
            try
            {
                string sql = @"
SELECT 
    c.*
FROM 
    Company c
";

                // First get company data
                DataTable dt = database.GetDataTable(sql);

                foreach (DataRow dr in dt.Rows)
                {
                    this.CompanyId = int.Parse(dr["CompanyId"].ToString());
                    this.CompanyName = dr["CompanyName"].ToString();
                    this.IBAN = dr["IBAN"].ToString();
                    this.BIC = dr["BIC"].ToString();
                    this.ContactPersonName = dr["ContactPersonName"].ToString();
                    this.Email = dr["Email"].ToString();
                    this.Phonenumber = dr["Phonenumber"].ToString();                
                }

                // Then get address
                if(CompanyId.HasValue)
                    this.Address.GetAddress(this.CompanyId.Value, (int)Enums.AddressType.OwnAddress);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        /// <summary>
        /// Set company data
        /// </summary>
        public void SetCompany()
        {
            try
            {
                if (this.CompanyId.HasValue)
                    updateCompany();
                else
                    addCompany();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        #endregion Public methods
    }
}
