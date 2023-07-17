using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    public static class Utils
    {
        [DllImport("wininet.dll")]
        private extern static bool InternetGetConnectedState(out int Description, int ReservedValue);

        



    }

    public class LinuxTime 
    {
        static System.DateTime LinuxDateTimeEpoch = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
        public static DateTime JavaTimeStampToDateTime(double javaTimeStamp)
        {
            // Java timestamp is millisecods past epoch

            return LinuxDateTimeEpoch.AddSeconds(Math.Round((javaTimeStamp - (javaTimeStamp % 1000)) / 1000)).ToLocalTime();
        }

        public static long Now
        {
            get
            {
                return DateTimeToJavaTimeStamp(DateTime.Now);
            }
        }
        public static DateTime JavaTimeStampToDateTimeUTC(double javaTimeStamp)
        {
            // Java timestamp is millisecods past epoch

            return LinuxDateTimeEpoch.AddSeconds(Math.Round((javaTimeStamp - (javaTimeStamp % 1000)) / 1000));
        }

        public static long DateTimeToJavaTimeStamp(DateTime date)
        {
            // Java timestamp is millisecods past epoch

            return (long)(date.ToUniversalTime() - LinuxDateTimeEpoch).TotalMilliseconds;
        }

        public static long DateTimeToJavaTimeStampUTC(DateTime date)
        {
            // Java timestamp is millisecods past epoch

            return (long)(date - LinuxDateTimeEpoch).TotalMilliseconds;
        }

        public static DateTime DateDiffStampToDateTime(DateTime refDate, double javaTimeStamp)
        {
            // Java timestamp is millisecods past epoch

            return refDate.AddSeconds(Math.Round(javaTimeStamp / 1000)).ToLocalTime();
        }

        public static long DateTimeDiffToTimeStamp(DateTime refDate, DateTime date)
        {
            // Java timestamp is millisecods past epoch

            return (long)(date.ToUniversalTime() - refDate).TotalMilliseconds;
        }
    }

}
