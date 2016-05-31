using System.Data;
using System.Data.Odbc;
using System.Data.OleDb;
using System.Data.SqlClient;

namespace DbManager
{
    public sealed class DBManagerFactory
    {
        private DBManagerFactory()
        {

        }

        public static IDbConnection GetConnection(Dataprovider providerType)
        {
            IDbConnection iDbConnection;
            switch (providerType)
            {
                case Dataprovider.SqlServer:
                    iDbConnection = new SqlConnection();
                    break;

                case Dataprovider.OleDb:
                    iDbConnection = new OleDbConnection();
                    break;
                case Dataprovider.Odbc:
                    iDbConnection = new OdbcConnection();
                    break;
                default:
                    return null;
            }

            return iDbConnection;
        }

        public static IDbCommand GetCommand(Dataprovider providerType)
        {
            switch (providerType)
            {
                case Dataprovider.SqlServer:
                    return new SqlCommand();
                case Dataprovider.OleDb:
                    return new OleDbCommand();
                case Dataprovider.Odbc:
                    return new OdbcCommand();
                default:
                    return null;
            }
        }

        public static IDataAdapter GetDataAdapter(Dataprovider providerType)
        {
            switch (providerType)
            {
                case Dataprovider.SqlServer:
                    return new SqlDataAdapter();
                case Dataprovider.OleDb:
                    return new OleDbDataAdapter();
                case Dataprovider.Odbc:
                    return new OdbcDataAdapter();
                default:
                    return null;
            }
        }

        public static IDbTransaction GetTransaction(Dataprovider providerType)
        {
            IDbConnection iDbConnection = GetConnection(providerType);
            IDbTransaction iDbTransaction = iDbConnection.BeginTransaction();
            return iDbTransaction;
        }
        public static IDbDataParameter[] GetParameters(Dataprovider providerType, int paramsCount)
        {
            IDbDataParameter[] idbParams = new IDbDataParameter[paramsCount];
            switch (providerType)
            {
                case Dataprovider.SqlServer:
                    for (int i = 0; i < paramsCount; i++)
                    {
                        idbParams[i] = new SqlParameter();
                    }
                    break;
                case Dataprovider.OleDb:
                    for (int i = 0; i < paramsCount; i++)
                    {
                        idbParams[i] = new OleDbParameter();
                    }
                    break;
                case Dataprovider.Odbc:
                    for (int i = 0; i < paramsCount; i++)
                    {
                        idbParams[i] = new OdbcParameter();
                    }
                    break;

                default:
                    idbParams = null;
                    break;
            }
            return idbParams;
        }
    }
}
