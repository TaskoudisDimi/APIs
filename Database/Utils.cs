using System;
using System.Collections.Generic;
using System.Globalization;
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

        public static int GetInt(object val, int defval = -1)
        {
            if (string.IsNullOrWhiteSpace(GetString(val)))
                return defval;

            try
            {
                return Convert.ToInt32(val);
            }
            catch
            {
                return defval;
            }
        }


        public static int? GetNullInt(object val, int? defval = null)
        {
            if (string.IsNullOrWhiteSpace(GetString(val)))
                return null;

            try
            {
                return (int?)Convert.ToInt32(val);
            }
            catch
            {
                return defval;
            }
        }

        public static Int64 GetLong(object val, long defval = -1)
        {
            if (string.IsNullOrWhiteSpace(GetString(val)))
                return defval;

            try
            {
                return Convert.ToInt64(val);
            }
            catch
            {
                return -1;
            }
        }

        public static long? GetNullLong(object val, long? defval = null)
        {
            if (string.IsNullOrWhiteSpace(GetString(val)))
                return null;

            try
            {
                return (long?)Convert.ToInt64(val);
            }
            catch
            {
                return defval;
            }
        }

        public static DateTime? GetDate(object val, string format)
        {
            if (string.IsNullOrWhiteSpace(GetString(val)))
                return null;

            try
            {
                return DateTime.ParseExact(GetString(val), format, CultureInfo.InvariantCulture);
            }
            catch
            {
                return null;
            }
        }

        public static DateTime? GetDate(object val)
        {
            if (string.IsNullOrWhiteSpace(GetString(val)))
                return null;

            try
            {
                return DateTime.Parse(GetString(val));
            }
            catch
            {
                return null;
            }
        }

        public static DateTime GetDate(object val, DateTime defaultValue)
        {
            try
            {
                return DateTime.Parse(GetString(val));
            }
            catch
            {
                return defaultValue;
            }
        }

        public static float GetFloat(object val, int defval = -1)
        {
            if (string.IsNullOrWhiteSpace(GetString(val)))
                return defval;

            try
            {
                return float.Parse(val.ToString().Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture);
            }
            catch
            {
                return defval;
            }
        }
        public static string GetString(object val, bool forSql = false, string defaultValue = "")
        {
            string result = defaultValue;

            if (val != null)
            {
                result = forSql ? val.ToString().Replace("'", "''").Trim() : val.ToString().Trim();
            }

            return result;
        }
        public static float? GetNullFloat(object val, float? defval = null)
        {
            if (string.IsNullOrWhiteSpace(GetString(val)))
                return null;

            try
            {
                return (float?)float.Parse(GetString(val).Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture);
            }
            catch
            {
                return defval;
            }
        }

        public static double GetDouble(object val, int defval = -1)
        {
            if (string.IsNullOrWhiteSpace(GetString(val)))
                return defval;

            try
            {
                return double.Parse(val.ToString().Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture);
            }
            catch
            {
                return defval;
            }
        }

        public static double? GetNullDouble(object val, double? defval = null)
        {
            if (string.IsNullOrWhiteSpace(GetString(val)))
                return null;

            try
            {
                return (double?)double.Parse(GetString(val).Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture);
            }
            catch
            {
                return defval;
            }
        }

        public static decimal GetDecimal(object val, decimal defval = -1)
        {
            if (string.IsNullOrWhiteSpace(GetString(val)))
                return defval;

            try
            {
                decimal value = decimal.Parse(val.ToString().Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture);
                return value;
            }
            catch
            {
                return defval;
            }
        }

        public static decimal? GetNullDecimal(object val, decimal? defval = null)
        {
            if (string.IsNullOrWhiteSpace(GetString(val)))
                return null;

            try
            {
                return decimal.Parse(GetString(val).Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture);
            }
            catch
            {
                return defval;
            }
        }

        public static bool GetBool(object val, bool defaultValue = false)
        {
            bool result = defaultValue;
            try
            {
                if (GetString(val).Equals("1") || GetString(val).ToLower().Equals("true"))
                    result = true;
                else if (val == null || GetString(val).Equals("0") || GetString(val).ToLower().Equals("false"))
                    result = false;
                else
                    result = bool.Parse(val.ToString());
            }
            catch { }

            return result;
        }

        public static bool? GetNullBool(object val, bool? defaultValue = null)
        {
            if (string.IsNullOrWhiteSpace(GetString(val)))
                return null;

            bool? result = defaultValue;
            try
            {
                if (GetString(val).Equals("1") || GetString(val).ToLower().Equals("true"))
                    result = (bool?)true;
                else if (GetString(val).Equals("0") || GetString(val).ToLower().Equals("false"))
                    result = (bool?)false;
                else
                    result = (bool?)bool.Parse(val.ToString());
            }
            catch { }

            return result;
        }



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
