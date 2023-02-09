using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapBuddy
{
    internal class Logger
    {
        public string log;

        public Logger()
        {
            log = "";
        }

        public void WriteLog()
        {
            string logDir = GetLogDir();

            bool exists = System.IO.Directory.Exists(logDir);

            if (!exists)
            {
                System.IO.Directory.CreateDirectory(logDir);
            }

            File.AppendAllText($"{logDir}log.txt", log);

            log = "";
        }

        public void AddToLog(string text)
        {
            log = log + text + "\n";
        }

        public string GetLogDir()
        {
            return System.AppDomain.CurrentDomain.BaseDirectory + "\\Log\\";
        }
    }
}
