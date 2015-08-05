using System;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace HSC_MC_DAL
{
    /// <summary>
    /// The base data access layer class: contains methods to build parameters and execute sprocs
    /// </summary>
    public class MC_DAL_Base
    {
        // Execute non query
        public void nonq_exec(string sprcName, DbParameter[] sprcPrms)
        {
            // Get connection string and create connection
            var tsqlConn = new SqlConnection(ConfigurationManager.AppSettings.Get("conn"));

            try
            {
                // Build command
                var tsqlCmnd = new SqlCommand
                {
                    Connection = tsqlConn,
                    CommandText = sprcName,
                    CommandType = CommandType.StoredProcedure,
                    CommandTimeout = 600
                };

                // Add parameters
                foreach (var pram in sprcPrms)
                {
                    tsqlCmnd.Parameters.Add(pram);
                }

                // Open connection
                tsqlConn.Open();

                // Execute command
                tsqlCmnd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                tsqlConn.Close();
            }

        } // end

        // Execute stored procedure 
        public DataTable sprc_exec(string sprcName, DbParameter[] sprcPrms)
        {
            var dtbl = new DataTable();

            try
            {
                var tsqlConn = new SqlConnection(ConfigurationManager.AppSettings.Get("conn"));

                var tsqlCmnd = new SqlCommand
                {
                    Connection = tsqlConn,
                    CommandText = sprcName,
                    CommandType = CommandType.StoredProcedure
                };

                foreach (var pram in sprcPrms)
                {
                    tsqlCmnd.Parameters.Add(pram);
                }

                var daData = new SqlDataAdapter { SelectCommand = tsqlCmnd };

                daData.Fill(dtbl);

                tsqlCmnd.Parameters.Clear();

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dtbl;

        } // end

        // Make an integer parameter
        public DbParameter pram_make_inte(int value, string column)
        {
            return new SqlParameter(column, value) { DbType = DbType.Int32 };

        } // end

        // Make a string parameter
        public DbParameter pram_make_strn(string value, string column)
        {
            return new SqlParameter(column, value) { DbType = DbType.String };

        } // end

    } // end class

} // end namespace
