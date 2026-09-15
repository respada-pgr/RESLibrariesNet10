using System;
using System.Globalization;

namespace _1_LibraryClassesNet10.Time
{
    public class C_DateTime
    {
        private DateTime _dateTime;

        public DateTime DateTime
        {
            get { return this._dateTime; }
            set { this._dateTime = value; }
        }

        public long Ticks
        {
            get { return this._dateTime != null ? this._dateTime.Ticks : 0; }
            set { this._dateTime = new DateTime(value); }
        }

        public bool HasValue
        {
            get { return this._dateTime != null; }
        }

        public C_DateTime()
        {
        }

        public C_DateTime(Int64 ticks)
        {
            this._dateTime = new DateTime(ticks);
        }

        public C_DateTime(DateTime dateTime)
        {
            this._dateTime = dateTime;
        }

        public C_DateTime(C_DateTime dateTime)
        {
            this._dateTime = dateTime.DateTime;
        }

        public C_DateTime(string dateTime, CultureInfo cultureInfo = null, DateTimeStyles dateTimeStyles = DateTimeStyles.None)
        {
            this._dateTime = DateTime.ParseExact(dateTime, cultureInfo.DateTimeFormat.FullDateTimePattern, cultureInfo, dateTimeStyles);
        }

        public bool TryLoad(string dateTime, CultureInfo cultureInfo = null, DateTimeStyles dateTimeStyles = DateTimeStyles.None)
        {
            DateTime date;
            bool result = DateTime.TryParseExact(dateTime, cultureInfo.DateTimeFormat.FullDateTimePattern, cultureInfo, dateTimeStyles, out date);
            if (result)
                this._dateTime = date;
            return result;
        }

        public bool TryLoad(string dateTime, string format, CultureInfo cultureInfo)
        {
            try
            {
                DateTime date = DateTime.ParseExact(dateTime, format, cultureInfo);
                this._dateTime = date;
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
                return false;
            }
        }

        public Int64 ToInt64()
        {
            if (this._dateTime != null)
                return this.Ticks;
            else
                return 0;
        }

        public Int64? ToNullInt64()
        {
            if (this._dateTime != null)
                return this.Ticks;
            else
                return null;
        }

        public override string ToString()
        {
            if (this._dateTime != null)
            {
                return (this._dateTime).ToString("yyyy-MM-dd HH:mm:ss.ffff");
            }
            else
            {
                return "";
            }
        }

        public string ToString(string dateFormat)
        {
            if (this._dateTime != null)
            {
                return ((DateTime)this._dateTime).ToString(dateFormat);
            }
            else
            {
                return "";
            }
        }

        /// <summary>

        /// </summary>
        /// <param name="dateTimePattern">
        ///             Examples:
        ///                     "yyyyMMddHHmmss"        //20201231
        ///                     "yyyy-MM-dd"            //2020-12-31
        ///                     "MM-dd-yyyy HH:mm:ss"   //12-31-2020 23:59:59 
        /// </param>
        /// <param name="cultureInfo"></param>
        /// <returns></returns>
        public string ToString(string dateTimePattern, CultureInfo cultureInfo)
        {
            if (this._dateTime != null)
            {
                if (cultureInfo != null)
                    return (this._dateTime).ToString(dateTimePattern, cultureInfo);
                else
                    return (this._dateTime).ToString(dateTimePattern);
            }
            else
            {
                return "";
            }
        }
    }
}
