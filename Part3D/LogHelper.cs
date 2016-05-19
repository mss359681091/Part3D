using System;
using System.Collections.Generic;
using System.Web;
using log4net;

namespace Part3D
{
    public class LogHelper
    {
        private static ILog log;
        private static LogHelper logHelper = null;
        /// <summary>
        /// 初始化
        /// </summary>
        /// <returns></returns>
        public static ILog GetInstance()
        {
            logHelper = new LogHelper(null);

            return log;
        }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="configPath"></param>
        /// <returns></returns>
        public static ILog GetInstance(string configPath)
        {
            logHelper = new LogHelper(configPath);

            return log;
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="configPath"></param>
        private LogHelper(string configPath)
        {
            if (!string.IsNullOrEmpty(configPath))
            {
                log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                log4net.Config.XmlConfigurator.Configure(new System.IO.FileInfo(configPath));
            }
            else
            {
                log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            }
        }
        #region 引用方法
        //private static readonly ILog m_log = LogHelper.GetInstance(); //LogManager.GetLogger(typeof(TEST));
        //m_log.Debug("这是一个Debug日志");  
        //m_log.Info("这是一个Info日志");  
        //m_log.Warn("这是一个Warn日志");  
        //m_log.Error("这是一个Error日志");  
        //m_log.Fatal("这是一个Fatal日志");  
        #endregion

    }
}