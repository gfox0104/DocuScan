using System;
using System.IO;
using System.Threading;

namespace DocuScan.Logging
{
    public class Log
    {

        private string programDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData).ToString();

        public bool Enabled { get; set; }

        public Log(bool enabled) => this.Enabled = enabled;

        private void WriteToLog(Log.EntryType category, string entry)
        {
            if (!this.Enabled)
                return;
            bool flag1 = false;
            string str;
            switch (category)
            {
                case Log.EntryType.Info:
                    str = "INFO ";
                    break;
                case Log.EntryType.Error:
                    str = "ERROR";
                    break;
                default:
                    str = "WARN ";
                    break;
            }
            if (flag1)
                Thread.Sleep(30);
            bool flag2 = true;
            StreamWriter streamWriter = new StreamWriter(this.programDataFolder + "\\Office Gemini\\Bulk Scan\\BulkScan.log", true);
            DateTime now = DateTime.Now;
            streamWriter.WriteLine(now.Month.ToString().PadLeft(2, '0') + "/" + now.Day.ToString().PadLeft(2, '0') + "/" + now.Year.ToString().PadLeft(2, '0') + " " + now.Hour.ToString().PadLeft(2, '0') + ":" + now.Minute.ToString().PadLeft(2, '0') + ":" + now.Second.ToString().PadLeft(2, '0') + "." + now.Millisecond.ToString().PadLeft(3, '0') + "\t" + str + "\t" + entry);
            streamWriter.Close();
            streamWriter.Dispose();
            flag2 = false;
        }

        public void Info(string entry)
        {
            this.WriteToLog(Log.EntryType.Info, entry);
        }

        public void Error(string entry) => this.WriteToLog(Log.EntryType.Error, entry);

        public void Warn(string entry) => this.WriteToLog(Log.EntryType.Warn, entry);

        public void Error(Exception ex)
        {
            this.Error(ex.ToString());
            for (; ex.InnerException != null; ex = ex.InnerException)
                this.Error("InnerException: " + ex.InnerException.ToString());
        }

        private enum EntryType
        {
            Info,
            Warn,
            Error,
        }
    }
}
