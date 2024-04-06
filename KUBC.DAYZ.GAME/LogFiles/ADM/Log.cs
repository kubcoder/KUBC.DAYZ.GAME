using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUBC.DAYZ.GAME.LogFiles.ADM
{
    /// <summary>
    /// Класс файла лога
    /// </summary>
    public class Log : FileWithTime
    {
        /// <summary>
        /// Набор парсеров для лога ADM
        /// </summary>
        private ADMParser parser = new ADMParser();
        /// <inheritdoc/>
        protected override ILogEntityFabric LogParser => parser;

        /// <summary>
        /// Гребанная инициализация
        /// </summary>
        public Log()
        {
            this.LinesNotRead = [
                "**********************************EOF****************************************",
                "******************************************************************************"
                ];
        }

        /// <summary>
        /// Шаблон поиска строчки с указанием текущего времени
        /// </summary>
        private const string KEYFINDSTARTTIME = "AdminLog started on";
        /// <inheritdoc/>
        protected override bool FindStartTime(string Line)
        {
            if (Line.Contains(KEYFINDSTARTTIME))
            {
                var tokens = Line.Split(' ');
                if (tokens.Length > 5)
                {
                    var sTime = $"{tokens[3]} {tokens[5]}";
                    if (DateTime.TryParse(sTime, out DateTime lTime))
                    {
                        LogStarted = lTime;
                    }
                }
                return true;
            }
            return false;
        }
        
    }
}
