using System;
using System.IO;
using System.Reflection;
using log4net;

namespace EPAM_TAO_CORE_COMMON_TAF.CommonHelpers
{
    public class Log4NetLogger
    {
        private static readonly object syncLock = new object();
        private static Log4NetLogger _log4NetLogger = null;
        private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private string strPathToLogFolder = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), DateTime.Now.ToString("dd-MM-yyyy")) + @"\" + "Log4NetLogs";
        private string strPathToLogFile = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), DateTime.Now.ToString("dd-MM-yyyy")) + @"\" + "Log4NetLogs" + @"\" + "Logs_" + DateTime.Now.ToString("dd-MM-yyyy") + ".log";

        Log4NetLogger()
        {
            
        }

        public static Log4NetLogger log4NetLogger
        {
            get
            {
                lock (syncLock)
                {
                    if (_log4NetLogger == null)
                    {
                        _log4NetLogger = new Log4NetLogger();
                    }
                    return _log4NetLogger;
                }
            }
        }

        public static ILog log
        {
            get
            {
                return _log;
            }
        }

        public void ConfigureLogFile()
        {
            if (!Directory.Exists(strPathToLogFolder))
            {
                Directory.CreateDirectory(strPathToLogFolder);
            }
            var appender = (log4net.Appender.FileAppender)LogManager.GetRepository().GetAppenders()[1];
            appender.File = strPathToLogFile;
            appender.ActivateOptions();
        }
    }
}
