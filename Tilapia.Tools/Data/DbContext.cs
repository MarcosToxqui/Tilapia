namespace Tilapia.Tools.Data
{
    using System;
    using System.Data.SqlClient;
    using Extensions;
    using Entities;
    using System.Configuration;

    /// <summary>
    /// Defines the running context of the database execution.
    /// </summary>
    public class DbContext : DbController
    {
        /// <summary>
        /// Intialize a new instance of <see cref="DbContext"/>
        /// </summary>
        /// <param name="connectionKeyName">Name for the connection string in a .CONFIG file.</param>
        private DbContext(string connectionKeyName) : base(connectionKeyName)
        {
            new DbController(connectionKeyName).ConnectionKeyName = connectionKeyName;
        }

        /// <summary>
        /// Intialize a new instance of <see cref="DbContext"/>
        /// </summary>
        /// <param name="connectionStringBuilder">Objeto <see cref="SqlConnectionStringBuilder"/> para construir la cadena de conexión.</param>
        private DbContext(SqlConnectionStringBuilder connectionStringBuilder) : base(connectionStringBuilder)
        {
        }

        /// <summary>
        /// Gets or sets the <see cref="Query"/> object.
        /// </summary>
        public Query Query { get; set; }

        /// <summary>
        /// Initialize the context instance.
        /// </summary>
        /// <param name="connectionKeyName">Connection string in .CONFIG file.</param>
        /// <returns>Instance of <see cref="DbContext"/>.</returns>
        public static DbContext PrepareContext(string connectionKeyName)
        {
            return new DbContext(connectionKeyName);
        }

        /// <summary>
        /// Instance of the context.
        /// </summary>
        /// <param name="dbName">Database name.</param>
        /// <param name="serverName">Sever name.</param>
        /// <param name="configKeyUserId">User id (from CONFIG file).</param>
        /// <param name="configKeyPassword">User password (from CONFIG file).</param>
        /// <param name="timeOut">Timeout value.</param>
        /// <param name="pooling">Uses pooling for connection.</param>
        /// <returns>Instance of <see cref="DbContext"/>.</returns>
        public static DbContext PrepareContext(string dbName, string serverName, string configKeyUserId, string configKeyPassword, string timeOut, bool pooling)
        {
            SqlConnectionStringBuilder sqlConnStringBuilder = new SqlConnectionStringBuilder();

            sqlConnStringBuilder.DataSource = serverName;
            sqlConnStringBuilder.InitialCatalog = dbName;
            sqlConnStringBuilder.UserID = Convert.ToString(ConfigurationManager.AppSettings[configKeyUserId]);
            sqlConnStringBuilder.Password = Convert.ToString(ConfigurationManager.AppSettings[configKeyPassword]);
            sqlConnStringBuilder.ConnectTimeout = Convert.ToInt32(timeOut);
            sqlConnStringBuilder.Pooling = pooling;

            return new DbContext(sqlConnStringBuilder);
        }

        /// <summary>
        /// Tries to execute the specified function.
        /// </summary>
        /// <typeparam name="TResult">Type of data for the result.</typeparam>
        /// <param name="function">Function.</param>
        /// <returns>Function result.</returns>
        public TResult TryExecute<TResult>(Func<TResult> function)
        {
            TResult result;

            try
            {
                result = function();
            }
            catch (SqlException sqlException)
            {
                result = default(TResult);
            }
            catch (Exception exception)
            {
                result = default(TResult);
            }

            return result;
        }

        /// <summary>
        /// Ejecuta el comando SQL.
        /// </summary>
        /// <returns>Un entero que especifica el numero de filas afectadas.</returns>
        /// <exception cref="NullReferenceException">Produce una excepción si el objeto <see cref="Query"/> es nulo o posee elementos inválidos.</exception>
        /// <exception cref="InvalidOperationException">Produce una excepción si el objeto <see cref="SqlConnection"/> no puede obtener la conexión a la base de datos especificada.</exception>
        public int ExecuteQuery()
        {
            int affectedRows = 0;

            if (!this.IsOnline)
            {
                new InvalidOperationException("La conexión no se puede iniciar en este momento es probable que el servidor de datos no este disponible.", new Exception("La verificación de la disponibilidad del servidor ha fallado."));
            }

            if (this.Query.IsNull() || this.Query.QueryText.IsNullOrEmpty())
            {
                throw new NullReferenceException("El objeto Query no se ha instanciado aun.", new Exception("El comando SQL no se ha especificado."));
            }

            affectedRows = this.ExecuteCommandText(this.Query);

            return affectedRows;
        }

        /// <summary>
        /// Executes the SQL command in <see cref="Query"/>.
        /// </summary>
        /// <param name="mapper">Function to map the values vs object.</param>
        /// <typeparam name="T">Type of data for the result.</typeparam>
        /// <returns>Function result.</returns>
        /// <exception cref="InvalidOperationException">Throws an exception when <see cref="Query"/> contanins null values.</exception>
        public T ExecuteQuery<T>(Func<SqlDataReader, T> mapper)
        {
            if (this.Query.IsNull() || this.Query.QueryText.IsNullOrEmpty())
            {
                throw new InvalidOperationException("El objeto Query no se ha instanciado aun.", new Exception("El comando SQL no se ha especificado."));
            }

            var result = this.ExecuteCommandText<T>(this.Query, mapper);
            return result;
        }

        /// <summary>
        /// Inner command execution.
        /// </summary>
        /// <param name="query"><see cref="Query"/> object to allows execute.</param>
        /// <returns>An integer.</returns>
        internal int ExecuteCommandText(Query query)
        {
            int affectedRows = 0;
            try
            {
                this.OpenConnection();

                if (query.WithTransaction)
                {
                    this.SqlTransaction = this.SqlConnection.BeginTransaction(query.IsolationLevel);
                    this.SqlCommand = new SqlCommand(query.QueryText, this.SqlConnection, this.SqlTransaction);
                }
                else
                {
                    this.SqlCommand = new SqlCommand(query.QueryText, this.SqlConnection);
                }

                if (!query.Parameters.IsNullOrEmpty())
                {
                    this.SqlCommand.Parameters.AddRange(query.Parameters.ToArray());
                }

                this.SqlCommand.CommandType = query.TextCommandType;

                affectedRows = this.SqlCommand.ExecuteNonQuery();

                this.CurrentTransaction(TransactionOperation.Commit);

                return affectedRows;
            }
            catch (SqlException sqlException)
            {
                affectedRows = -1;
                this.CurrentTransaction(TransactionOperation.Rollback);
                this.CloseConnection();
            }
            catch (Exception exception)
            {
                affectedRows = -1;
                this.CurrentTransaction(TransactionOperation.Rollback);
                this.CloseConnection();
            }
            finally
            {
                this.CloseConnection();
            }

            return affectedRows;
        }

        /// <summary>
        /// Executes the command.
        /// </summary>
        /// <typeparam name="T">Generic type.</typeparam>
        /// <param name="query">Object with the data to execute the query.</param>
        /// <param name="mapper">Mapper function to retrieve data with the entities.</param>
        /// <returns>Typed object.</returns>
        internal T ExecuteCommandText<T>(Query query, Func<SqlDataReader, T> mapper)
        {
            try
            {
                this.OpenConnection();

                if (query.WithTransaction)
                {
                    this.SqlTransaction = this.SqlConnection.BeginTransaction(query.IsolationLevel);
                }

                this.SqlCommand = new SqlCommand(query.QueryText, this.SqlConnection);

                if (query.Parameters.IsNotNull())
                {
                    this.SqlCommand.Parameters.AddRange(query.Parameters.ToArray());
                }

                this.SqlCommand.CommandType = query.TextCommandType;

                SqlDataReader sqlDataReader = this.SqlCommand.ExecuteReader();

                return mapper(sqlDataReader);
            }
            catch (SqlException sqlException)
            {
                this.CurrentTransaction(TransactionOperation.Rollback);
                this.CloseConnection();
            }
            catch (Exception exception)
            {
                this.CurrentTransaction(TransactionOperation.Rollback);
                this.CloseConnection();
            }
            finally
            {
                this.CloseConnection();
            }

            return default(T);
        }
    }
}
