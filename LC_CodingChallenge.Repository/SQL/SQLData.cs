using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace LC_CodingChallenge.Repository.SQL
{
    public class SQLData
    {
        private string connectionString;
        private int commandTimeout;
        private bool isStoredprocedure;
        private bool _isStoredProcedure;

        public bool IsStoredProcedure { get => _isStoredProcedure; set => _isStoredProcedure = value; }
        public SQLData(string connectionString)
        {
            this.connectionString = connectionString;
            this.commandTimeout = -1;
            this.isStoredprocedure = false;

        }
        public int ExecuteNonQueryTableValued(string spName, Dictionary<string, object> parameters)
        {
            try
            {
                int result = 0;
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(spName, sqlConnection);
                    command.CommandType = CommandType.StoredProcedure;
                    if (commandTimeout > 0)
                    {
                        command.CommandTimeout = commandTimeout;
                    }
                    if (parameters != null)
                    {
                        foreach (string key in parameters.Keys)
                        {
                            if (parameters[key].GetType() == typeof(DataTable))
                            {
                                command.Parameters.AddWithValue(key, parameters[key]).SqlDbType = SqlDbType.Structured;
                            }
                            else
                            {
                                command.Parameters.Add(new SqlParameter(key, parameters[key]));
                            }
                        }
                    }
                    try
                    {
                        if (sqlConnection.State != ConnectionState.Open)
                        {
                            sqlConnection.Open();
                        }
                        result = command.ExecuteNonQuery();
                    }
                    finally
                    {
                        command.Dispose();
                        sqlConnection.Close();
                    }
                    return result;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public IEnumerable<T> GetData<T>(string spName, Dictionary<string, object> paramenters, Func<IDataRecord, T> BuildObject)
        {

            using (var conn = new SqlConnection(connectionString))
            {

                var sqlCommand = conn.CreateCommand();
                sqlCommand.CommandText = spName;

                if (commandTimeout > 0)
                {
                    sqlCommand.CommandTimeout = commandTimeout;
                }

                if (isStoredprocedure)
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                }

                if (paramenters != null)
                {
                    foreach (string key in paramenters.Keys)
                    {
                        sqlCommand.Parameters.Add(new SqlParameter(key, paramenters[key]));
                    }
                }

                conn.Open();

                using (var reader = sqlCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        yield return BuildObject(reader);
                    }
                }

            }

        }

    }
}
