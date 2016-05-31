using System.Data;

namespace DbManager
{
    public interface IDBManager
    {
        Dataprovider ProviderType
        {
            get;
            set;
        }
        IDbConnection Connection
        {
            get;
            set;
        }
        IDataReader DataReader
        {
            get;
            set;
        }
        IDbCommand Command
        {
            get;
            set;
        }
        IDbTransaction Transaction
        {
            get;
            set;
        }
        IDbDataParameter[] Parameters
        {
            get;
            set;
        }
        string ConnectionString
        {
            get;
            set;
        }

        void Open();
        void Close();
        void Dispose();
        void CreateParameters(int paramCount);
        void AddParameters(int index, string paramNam, object objValue);
        void BeginTransaction();
        void CommitTransaction();
        void CloseReader();
        IDataReader ExecuteReader(CommandType commandType, string commandText);
        int ExecuteNonQuery(CommandType commantype, string commandText);
        object ExecuteScalar(CommandType commandType, string commandText);
        DataSet ExecuteDataSet(CommandType commanType, string commandText);
    }
}
