namespace Tilapia.Tools.Data.Entities
{
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    /// <summary>
    /// Allows abstract traditional items for SqlCommand.
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("{QueryText}")]
    public sealed class Query
    {
        /// <summary>
        /// Initialize a SQL parameters list
        /// </summary>
        private List<SqlParameter> listParameters = new List<SqlParameter>();

        /// <summary>
        /// Execution context
        /// </summary>
        private DbContext queryDbContext;

        /// <summary>
        /// Gets or sets the parametrized SQL text.
        /// </summary>
        public string QueryText { get; set; }

        /// <summary>
        /// Gets or sets the SQL parameters list to execute the query.
        /// </summary>
        public List<SqlParameter> Parameters
        {
            get
            {
                return this.listParameters;
            }

            set
            {
                this.listParameters = value;
            }
        }

        /// <summary>
        /// Gets or sets a value to indicate if the command executes inside transaction.
        /// </summary>
        public bool WithTransaction { get; set; }

        /// <summary>
        /// Gets or sets the isolation level for this query.
        /// </summary>
        public IsolationLevel IsolationLevel { get; set; }

        /// <summary>
        /// Gets or sets the command type <see cref="CommandType"/>.
        /// </summary>
        public CommandType TextCommandType { get; set; }

        /// <summary>
        /// Prevents an default instance of <see cref="Query"/> beign created
        /// </summary>
        private Query()
            : base()
        {
        }

        /// <summary>
        /// Initialize an instance for <see cref="Query"/> class.
        /// </summary>
        /// <returns><see cref="Query"/> instance.</returns>
        public static Query CreateQuery()
        {
            return new Query();
        }

        /// <summary>
        /// Initialize an instance for <see cref="Query"/> class.
        /// </summary>
        /// <param name="queryText">SQL text to execute.</param>
        /// <returns><see cref="Query"/> instance.</returns>
        public static Query CreateQuery(string queryText)
        {
            Query innerQuery = new Query
            {
                TextCommandType = CommandType.Text,
                QueryText = queryText,
                Parameters = null,
                WithTransaction = false,
                IsolationLevel = IsolationLevel.ReadCommitted
            };

            return innerQuery;
        }

        /// <summary>
        /// Initialize an instance for <see cref="Query"/> class.
        /// </summary>
        /// <param name="queryText">SQL text to execute.</param>
        /// <param name="parameters">Sql parameters list.</param>
        /// <returns><see cref="Query"/> instance.</returns>
        public static Query CreateQuery(string queryText, List<SqlParameter> parameters)
        {
            Query innerQuery = new Query
            {
                TextCommandType = CommandType.Text,
                QueryText = queryText,
                Parameters = parameters,
                WithTransaction = false,
                IsolationLevel = IsolationLevel.ReadCommitted
            };

            return innerQuery;
        }

        /// <summary>
        /// Initialize an instance for <see cref="Query"/> class.
        /// </summary>
        /// <param name="queryText">SQL text to execute.</param>
        /// <param name="parameters">Sql parameters list.</param>
        /// <param name="withTransaction">Execute command under transaction.</param>
        /// <param name="isolationLevel">Isolation level.</param>
        /// <returns><see cref="Query"/> instance.</returns>
        public static Query CreateQuery(string queryText, List<SqlParameter> parameters, bool withTransaction, IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
        {
            Query innerQuery = new Query
            {
                TextCommandType = CommandType.Text,
                QueryText = queryText,
                Parameters = parameters,
                WithTransaction = withTransaction,
                IsolationLevel = isolationLevel
            };

            return innerQuery;
        }
    }
}
