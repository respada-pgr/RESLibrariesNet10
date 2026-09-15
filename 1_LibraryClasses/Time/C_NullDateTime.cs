using System;
using System.Globalization;

namespace _1_LibraryClassesNet10.Time
{
    public class C_NullDateTime
    {
        private DateTime? _dateTime = null;

        public DateTime? DateTime
        {
            get { return this._dateTime; }
            set { this._dateTime = value; }
        }

        public int? Year
        {
            get
            {
                int? result = null;
                if (_dateTime != null)
                    result = ((DateTime)(this._dateTime)).Year;
                return result;
            }
        }

        public int? Month
        {
            get
            {
                int? result = null;
                if (_dateTime != null)
                    result = ((DateTime)(this._dateTime)).Month;
                return result;
            }
        }

        public int? Day
        {
            get
            {
                int? result = null;
                if (_dateTime != null)
                    result = ((DateTime)(this._dateTime)).Day;
                return result;
            }
        }

        public int? Hour
        {
            get
            {
                int? result = null;
                if (_dateTime != null)
                    result = ((DateTime)(this._dateTime)).Hour;
                return result;
            }
        }

        public int? Minute
        {
            get
            {
                int? result = null;
                if (_dateTime != null)
                    result = ((DateTime)(this._dateTime)).Minute;
                return result;
            }
        }

        public int? Second
        {
            get
            {
                int? result = null;
                if (_dateTime != null)
                    result = ((DateTime)(this._dateTime)).Second;
                return result;
            }
        }

        public int? Millisecond
        {
            get
            {
                int? result = null;
                if (_dateTime != null)
                    result = ((DateTime)(this._dateTime)).Millisecond;
                return result;
            }
        }

        public long Ticks
        {
            get
            {
                long result = 0;
                if (_dateTime != null)
                    result = ((DateTime)(this._dateTime)).Ticks;
                return result;
            }
            set { this._dateTime = new DateTime(value); }
        }

        public bool HasValue
        {
            get { return this._dateTime != null; }
        }


        public C_NullDateTime()
        {
        }

        public C_NullDateTime(long ticks)
        {
            this._dateTime = new DateTime(ticks);
        }

        public C_NullDateTime(Int64? ticks)
        {
            if (ticks != null)
                this._dateTime = new DateTime((long)ticks);
            else
                this._dateTime = null;
        }

        public C_NullDateTime(string dateTime, CultureInfo cultureInfo = null, DateTimeStyles dateTimeStyles = DateTimeStyles.None)
        {
            this._dateTime = System.DateTime.ParseExact(dateTime, cultureInfo.DateTimeFormat.FullDateTimePattern, cultureInfo, dateTimeStyles);
        }

        public C_NullDateTime(DateTime dateTime)
        {
            this._dateTime = dateTime;
        }

        public C_NullDateTime(DateTime? dateTime)
        {
            this._dateTime = dateTime;
        }

        public C_NullDateTime(C_NullDateTime dateTime)
        {
            this._dateTime = dateTime.DateTime;
        }

        public C_NullDateTime(C_DateTime dateTime)
        {
            this._dateTime = dateTime.DateTime;
        }

        public void SetNow()
        {
            this._dateTime = System.DateTime.Now;
        }

        public bool TryLoad(string dateTime, CultureInfo cultureInfo = null, DateTimeStyles dateTimeStyles = DateTimeStyles.None)
        {
            DateTime date;
            bool result = System.DateTime.TryParseExact(dateTime, cultureInfo.DateTimeFormat.FullDateTimePattern, cultureInfo, dateTimeStyles, out date);
            if (result)
                this._dateTime = date;
            return result;
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
                return ((DateTime)this._dateTime).ToString("yyyy-MM-dd HH:mm:ss.ffff");
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

        /// </summary>
        /// <param name="dateTimePattern">
        ///             Examples:
        ///                     "yyyyMMddHHmmss"        //20201231
        ///                     "yyyy-MM-dd"            //2020-12-31
        ///                     "MM-dd-yyyy HH:mm:ss"   //12-31-2020 23:59:59 
        /// </param>
        /// <param name="cultureInfo"></param>
        /// <returns></returns>
        public string ToString(string dateTimePattern, CultureInfo cultureInfo = null)
        {
            if (this._dateTime != null)
            {
                if (cultureInfo != null)
                    return ((DateTime)this._dateTime).ToString(dateTimePattern, cultureInfo);
                else
                    return ((DateTime)this._dateTime).ToString(dateTimePattern);
            }
            else
            {
                return "";
            }
        }
    }
}
