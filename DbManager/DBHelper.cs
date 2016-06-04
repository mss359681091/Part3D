
/******************************************************************
** 创建人: 李赛赛
** 创建日期:2016-05-31
** 描 述: 调用DBManager类,外部直接调用DBHelper类即可
** 版 本:1.0
**-----------------------------------------------------------------
********************************************************************/

using System;
using System.Configuration;
using System.Data;

namespace DbManager
{
    public class DBHelper
    {
        private static readonly IDBManager dbManager = new DBManager(GetDataProvider(), GetConnectionString());

        /// <summary>
        /// 从配置文件中选择数据库类型
        /// </summary>
        /// <returns>DataProvider枚举值</returns>
        private static Dataprovider GetDataProvider()
        {
            string providerType = ConfigurationManager.AppSettings["DataProvider"];
            Dataprovider dataProvider;
            switch (providerType)
            {
                case "SqlServer":
                    dataProvider = Dataprovider.SqlServer;
                    break;
                case "OleDb":
                    dataProvider = Dataprovider.OleDb;
                    break;
                case "Odbc":
                    dataProvider = Dataprovider.Odbc;
                    break;
                default:
                    return Dataprovider.SqlServer;
            }
            return dataProvider;
        }

        /// <summary>
        /// 从配置文件获取连接字符串
        /// </summary>
        /// <returns>连接字符串</returns>
        private static string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        }

        /// <summary>
        /// 关闭数据库连接的方法
        /// </summary>
        public static void Close()
        {
            dbManager.Dispose();
        }

        /// <summary>
        /// 创建参数
        /// </summary>
        /// <param name="paramsCount"></param>
        public static void CreateParameters(int paramsCount)
        {
            dbManager.CreateParameters(paramsCount);
        }

        /// <summary>
        /// 添加参数
        /// </summary>
        /// <param name="index">参数索引</param>
        /// <param name="paramName">参数名</param>
        /// <param name="objValue">参数值</param>
        public static void AddParameters(int index, string paramName, object objValue)
        {
            dbManager.AddParameters(index, paramName, objValue);
        }

        /// <summary>
        /// 执行增删改
        /// </summary>
        /// <param name="sqlString">安全的sql语句string.Format()</param>
        /// <returns>操作成功返回true</returns>
        public static bool ExecuteNonQuery(string sqlString)
        {
            try
            {
                dbManager.Open();
                return dbManager.ExecuteNonQuery(CommandType.Text, sqlString) > 0 ? true : false;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
            finally
            {
                dbManager.Dispose();
            }
        }

        /// <summary>
        /// 返回第一行第一列
        /// </summary>
        /// <param name="sqlString">安全的sql语句string.Format()</param>
        /// <returns>操作成功返回true</returns>
        public static string ExecuteScalar(string sqlString)
        {
            try
            {
                dbManager.Open();
                return dbManager.ExecuteScalar(CommandType.Text, sqlString).ToString();
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
            finally
            {
                dbManager.Dispose();
            }
        }

        /// <summary>
        /// 执行查询
        /// </summary>
        /// <param name="sqlString">安全的sql语句string.Format()</param>
        /// <returns>返回IDataReader</returns>
        public static IDataReader ExecuteReader(string sqlString)
        {
            try
            {
                dbManager.Open();
                return dbManager.ExecuteReader(CommandType.Text, sqlString);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// 执行查询，返回数据集
        /// </summary>
        /// <param name="sqlString"></param>
        /// <returns></returns>
        public static DataSet ExecuteDataSet(string sqlString)
        {
            DataSet myDataSet = new DataSet();
            try
            {
                dbManager.Open();
                myDataSet = dbManager.ExecuteDataSet(CommandType.Text, sqlString);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return myDataSet;
        }


    }
}
