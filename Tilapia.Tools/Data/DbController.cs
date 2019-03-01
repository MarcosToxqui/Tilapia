namespace Tilapia.Tools.Data
{
    using System;
    using System.Configuration;
    using System.Data;
    using System.Data.SqlClient;
    using Extensions;

    /// <summary>
    /// Represents the current database context.
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("ConnectionString={ConnectionString}")]
    public class DbController
    {
        /// <summary>
        /// Initializes a new instance for class <see cref="DbController"/>
        /// </summary>
        /// <param name="connectionKeyName">Connection string key name.</param>
        public DbController(string connectionKeyName)
        {
            this.ConnectionKeyName = connectionKeyName;
            this.ConnectionString = this.GetConnectionString();
        }

        /// <summary>
        /// Initializes a new instance for class <see cref="DbController"/>
        /// </summary>
        /// <param name="sqlConnectionKeyName">Connection string.</param>
        public DbController(SqlConnectionStringBuilder sqlConnectionKeyName)
        {
            this.ConnectionKeyName = "SqlConnectionStringBuilder.Conn";
            this.ConnectionString = sqlConnectionKeyName.ToString();
        }

        /// <summary>
        /// Operation type for transaction.
        /// </summary>
        protected enum TransactionOperation : ushort
        {
            /// <summary>
            /// Commit
            /// </summary>
            Commit = 1,

            /// <summary>
            /// Rollback
            /// </summary>
            Rollback = 2
        }

        /// <summary>
        /// Gets a value that indicates if database is online.
        /// </summary>
        public bool IsOnline
        {
            get
            {
                if (!this.ConnectionString.IsNullOrEmpty())
                {
                    try
                    {
                        using (SqlConnection testConnection = new SqlConnection(this.ConnectionString))
                        {
                            testConnection.Open();
                            testConnection.Close();
                        }
                    }
                    catch (Exception)
                    {
                        return false;
                    }

                    return true;
                }

                return false;
            }
        }

        /// <summary>
        /// Gets the connection string name.
        /// </summary>
        public string ConnectionKeyName { get; internal set; }

        /// <summary>
        /// Gets the connection string.
        /// </summary>
        public string ConnectionString { get; internal set; }

        /// <summary>
        /// Gets or sets the SQL connection that is used in this context.
        /// </summary>
        internal SqlConnection SqlConnection { get; set; }

        /// <summary>
        /// Gets or sets the SQL command that is used in this context.
        /// </summary>
        internal SqlCommand SqlCommand { get; set; }

        /// <summary>
        /// Gets or sets the SQL transaction that is used in this context.
        /// </summary>
        protected SqlTransaction SqlTransaction { get; set; }

        /// <summary>
        /// Open the SQL connection for this context.
        /// </summary>
        protected void OpenConnection()
        {
            if (this.IsOnline)
            {
                this.SqlConnection = new SqlConnection(this.ConnectionString);
                this.SqlConnection.Open();
            }
            else
            {
                throw new Exception("Database is not online.");
            }
        }

        /// <summary>
        /// Closes the SQL connection for current context.
        /// </summary>
        protected void CloseConnection()
        {
            if (this.SqlConnection.IsNotNull() && this.SqlConnection.State == ConnectionState.Open)
            {
                this.SqlConnection.Close();
                this.SqlConnection.Dispose();
            }
        }

        /// <summary>
        /// Execute the operation requested.
        /// </summary>
        /// <param name="transaction">Transaction operation (commit or rollback).</param>
        protected void CurrentTransaction(TransactionOperation transaction)
        {
            if (this.SqlTransaction != null)
            {
                switch (transaction)
                {
                    case TransactionOperation.Commit:
                        this.SqlTransaction.Commit();
                        break;
                    case TransactionOperation.Rollback:
                        this.SqlTransaction.Rollback();
                        break;
                }
            }
        }

        /// <summary>
        /// Gets the connection string from .CONFIG file.
        /// </summary>
        /// <returns>A string that contains the connection data.</returns>
        private string GetConnectionString()
        {
            if (Convert.ToString(ConfigurationManager.ConnectionStrings[this.ConnectionKeyName]).IsNullOrEmpty())
            {
                throw new Exception("String connection not found.");
            }

            return Convert.ToString(ConfigurationManager.ConnectionStrings[this.ConnectionKeyName]);
        }
    }
}
