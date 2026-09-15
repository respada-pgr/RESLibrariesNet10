namespace _2_LibraryUtils.Logger
{
    public class C_LoggedLine : C_LogLine
    {
        private long? _logId = -1;

        public long LogId { get => this._logId ?? -1; set => this._logId = value; }

        public C_LoggedLine()
        {
        }

        public C_LoggedLine(long logId, C_LogLine line) : base(line)
        {
            this._logId = logId;
        }
    }
}
