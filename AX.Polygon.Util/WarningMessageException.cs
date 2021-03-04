using System;

namespace AX.Polygon.Util
{
    public class WarningMessageException : Exception
    {
        public string WarningMessage { get; set; }

        public WarningMessageException(string warningmessage)
        {
            WarningMessage = warningmessage;
        }
    }
}