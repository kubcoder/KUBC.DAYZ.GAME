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
    public class Log : FileWithTime
    {
        
        /// <summary>
        /// Парсер лога RPT
        /// </summary>
        private RPTParser parser = new RPTParser();
        /// <inheritdoc/>
        protected override ILogEntityFabric LogParser => parser;

        /// <summary>
        /// Шаблон поиска строчки с указанием текущего времени
        /// </summary>
        private const string KEYFINDSTARTTIME = "Current time:";
        /// <inheritdoc/>
        protected override bool FindStartTime(string Line)
        {
            if (LogStarted!=null)
            {
                return false;
            }
            if (Line.Contains(KEYFINDSTARTTIME))
            {
                var textTime = Line.Substring(KEYFINDSTARTTIME.Length + 1).Trim();
                if (DateTime.TryParse(textTime, out var sTime))
                {
                    LogStarted = sTime;
                    return true;
                }
            }
            return false;
        }
    }
}
