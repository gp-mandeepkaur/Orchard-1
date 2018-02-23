using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PI.Party.Helpers
{
    public class ConnectionStringHelper
    {
        /// <summary>
        /// Test connection string by passing connection string as paramter
        /// </summary>
        /// <param name="connectionString">complete connection string for the database</param>
        /// <returns>true if connection string is valid and false if invalid</returns>
        static bool TestConnectionString(string connectionString)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    return (conn.State == ConnectionState.Open);
                }
                catch
                {
                    return false;
                }
                finally
                {
                    conn.Dispose();
                }
            }
        }

        /// <summary>
        /// Test connection string using connection string information
        /// </summary>
        /// <param name="dataSource"></param>
        /// <param name="catalog"></param>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns>true if connection string settings entered are valid and false if invalid</returns>
        public static bool TestConnectionString(string dataSource, string catalog, string userName, string password, bool integratedSecurity = false)
        {
            object[] args = new object[] { dataSource, catalog, userName, password, (!integratedSecurity ? "false" : "true") };
            // Create connection string using connection settings
            string connString = string.Format("Data Source={0};Initial Catalog={1};User ID={2};Password={3};Integrated Security={4};", args);
            return TestConnectionString(connString);
        }

    }
}