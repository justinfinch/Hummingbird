using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Core.Logging
{
    public class LogUtil
    {
        public static string BuildExceptionMessage(Exception ex)
        {

            Exception logException = ex;
            if (ex.InnerException != null)
                logException = ex.InnerException;

            StringBuilder errorMsg = new StringBuilder();

            // Get the error message
            errorMsg.Append(Environment.NewLine);
            errorMsg.Append("Message :");
            errorMsg.Append(logException.Message);

            // Source of the message
            errorMsg.Append(Environment.NewLine);
            errorMsg.Append("Source :");
            errorMsg.Append(logException.Source);

            // Stack Trace of the error
            errorMsg.Append(Environment.NewLine);
            errorMsg.Append("Stack Trace :");
            errorMsg.Append(logException.StackTrace);

            // Method where the error occurred
            errorMsg.Append(Environment.NewLine);
            errorMsg.Append("TargetSite :");
            errorMsg.Append(logException.TargetSite);

            return errorMsg.ToString();
        }

    }
}
