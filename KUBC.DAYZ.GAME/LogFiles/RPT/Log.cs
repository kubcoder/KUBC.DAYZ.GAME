using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUBC.DAYZ.GAME.LogFiles.RPT
{
    /// <summary>
    /// Лог RPT игры
    /// </summary>
    public class Log : File
    {
        /// <inheritdoc/>
        protected override ILogEntity? ParseLine(string LogLine)
        {
            if (LogStarted == null)
            {
                FindStartTime(LogLine);
            }
            else
            {

            }
            return null;
        }

        /// <inheritdoc/>
        protected override void OnFileOpen()
        {
            LogStarted = null;
        }

        /// <summary>
        /// Дата и время начала лога
        /// </summary>
        public DateTime? LogStarted;

        /// <summary>
        /// Шаблон поиска строчки с указанием текущего времени
        /// </summary>
        private const string KEYFINDSTARTTIME = "Current time:";

        private void FindStartTime(string Line)
        {

            if (Line.Contains(KEYFINDSTARTTIME))
            {
                var textTime = Line.Substring(KEYFINDSTARTTIME.Length + 1).Trim();
                if (DateTime.TryParse(textTime, out var sTime))
                {
                    LogStarted = sTime;
                }
            }
        }
    }
}
