using EtrianOdysseyPC2.DataContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtrianOdysseyPC2.Logging
{
    class Logger
    {
        private static Lazy<Logger> _lazy = new Lazy<Logger>(() => new Logger());
        public static Logger Global => _lazy.Value;

        private void Log(ModelContext context, LogLevel level, string message)
        {
#if DEBUG
            context.TextBox.AppendText($"[{level}] {message}{Environment.NewLine}");
            context.TextBox.ScrollToEnd();
#endif
        }

        public void Information(ModelContext context, string message)
        {
            Log(context, LogLevel.Information, message);
        }

        public void Debug(ModelContext context, string message)
        {
            Log(context, LogLevel.Debug, message);
        }

        public void Warning(ModelContext context, string message)
        {
            Log(context, LogLevel.Warning, message);
        }

        public void Error(ModelContext context, string message)
        {
            Log(context, LogLevel.Error, message);
        }

        public void Fatal(ModelContext context, string message)
        {
            Log(context, LogLevel.Fatal, message);
        }
    }

    enum LogLevel
    {
        Information,
        Debug,
        Warning,
        Error,
        Fatal
    }
}
