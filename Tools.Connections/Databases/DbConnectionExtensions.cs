using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Reflection;

namespace Tools.Connections.Databases
{
    public static class DbConnectionExtensions
    {
        public static int ExecuteNonQuery(this DbConnection dbConnection, string query, bool isStoredProcedure = false, object? parameters = null)
        {
            using (DbCommand dbCommand = CreateCommand(dbConnection, query, isStoredProcedure, parameters))
            {
                if (dbConnection.State == ConnectionState.Closed)
                    dbConnection.Open();

                return dbCommand.ExecuteNonQuery();
            }
        }

        public static object? ExecuteScalar(this DbConnection dbConnection, string query, bool isStoredProcedure = false, object? parameters = null)
        {
            using (DbCommand dbCommand = CreateCommand(dbConnection, query, isStoredProcedure, parameters))
            {
                if (dbConnection.State == ConnectionState.Closed)
                    dbConnection.Open();

                object? result = dbCommand.ExecuteScalar();
                return (result is DBNull) ? null : result;
            }
        }

        public static IEnumerable<TResult> ExecuteReaderImmediately<TResult>(this DbConnection dbConnection, string query, Func<IDataRecord, TResult> selector, bool isStoredProcedure = false, object? parameters = null)
        {
            return ExecuteReader(dbConnection, query, selector, isStoredProcedure, parameters).ToList();
        }

        public static IEnumerable<TResult> ExecuteReader<TResult>(this DbConnection dbConnection, string query, Func<IDataRecord, TResult> selector, bool isStoredProcedure = false, object? parameters = null)
        {
            using (DbCommand dbCommand = CreateCommand(dbConnection, query, isStoredProcedure, parameters))
            { 
                if (dbConnection.State == ConnectionState.Closed)
                    dbConnection.Open();

                using (DbDataReader reader = dbCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        yield return selector(reader);
                    }
                }
            }
        }

        private static DbCommand CreateCommand(DbConnection dbConnection, string query, bool isStoredProcedure, object? parameters)
        {
            DbCommand dbCommand = dbConnection.CreateCommand();
            dbCommand.CommandText = query;

            if (isStoredProcedure)
                dbCommand.CommandType = CommandType.StoredProcedure;

            if (parameters is not null)
            {
                Type type = parameters.GetType();

                foreach (PropertyInfo propertyInfo in type.GetProperties())
                {
                    DbParameter dbParameter = dbCommand.CreateParameter();
                    dbParameter.ParameterName = propertyInfo.Name;
                    MethodInfo? methodInfo = propertyInfo.GetMethod;

                    if (methodInfo is null)
                        throw new InvalidOperationException("L'accesseur GET doit être 'public'");

                    dbParameter.Value = methodInfo.Invoke(parameters, null);
                    dbCommand.Parameters.Add(dbParameter);
                }
            }

            return dbCommand;
        }
    }
}