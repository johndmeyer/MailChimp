using System;
using log4net;

namespace HSC_MC_Common
{
    public static class LoggingHelper
    {
        private static readonly ILog Logger = LogManager.GetLogger(string.Empty);

        /// <summary>
        /// Create a new log entry
        /// </summary>
        /// <param name="message"></param>
        /// <param name="type"></param>
        /// <param name="exception"></param>
        /// <returns></returns>
        public static bool WriteLog(Constants.LogType type, string message, Exception exception)
        {
            try
            {
                if (exception != null)
                {
                    var exceptionDetail = string.Format("Source: {0}, Stack Trace: {1}, Inner Exception: {2}, Data: {3}",
                                          exception.Source,
                                          exception.StackTrace,
                                          exception.InnerException,
                                          exception.Data);

                    message = string.Format("{0} {1} {2} {1} {1}", exceptionDetail, Environment.NewLine, message);
                }

                switch (type)
                {
                    case Constants.LogType.Info:

                        Logger.Info(message);

                        break;

                    default:

                        Logger.Error(message);

                        break;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(message, ex);

                return false;
            }

            return true;

        } // end

    } // end class

} // end namespace
