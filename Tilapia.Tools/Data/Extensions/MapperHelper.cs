namespace Tilapia.Data.Extensions
{
    using System;
    using System.Data.SqlClient;

    /// <summary>
    /// Helper class to retrieve data from SqlDataReader.
    /// </summary>
    public static class MapperHelper
    {
        /// <summary>
        /// Retrieves the value of an SqlDataReader object and trying cast to specified type.
        /// </summary>
        /// <typeparam name="T">Type used to cast.</typeparam>
        /// <param name="dataReader">DataReader object.</param>
        /// <param name="colName">Column name.</param>
        /// <returns>Value retrieved.</returns>
        public static T GetValue<T>(this SqlDataReader dataReader, string colName)
        {
            try
            {
                Type typeCast = typeof(T);
                dynamic valueResult = Convert.ChangeType(dataReader[colName], typeCast);

                if (valueResult == DBNull.Value)
                {
                    return default(T);
                }

                return valueResult;
            }
            catch (Exception)
            {
                return default(T);
            }
        }
    }
}
