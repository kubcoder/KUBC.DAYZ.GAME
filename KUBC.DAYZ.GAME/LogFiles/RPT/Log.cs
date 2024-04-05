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
        /// <summary>
        /// Парсер лога RPT
        /// </summary>
        private Parser parser;
        /// <summary>
        /// Создаем экземпляр RPT лога
        /// </summary>
        public Log()
        {
            parser = new Parser();
        }
        
        /// <inheritdoc/>
        protected override ILogEntity? ParseLine(string LogLine)
        {
            if (LogStarted == null)
            {
                FindStartTime(LogLine);
            }
            else
            {
                var entity = parser.CreateEntity(LogLine);
                if (entity != null) 
                {
                    entity.Time = CorrectTime(entity.Time);
                    return entity;
                }
            }
            return null;
        }


        /// <summary>
        /// Корректируем дату и время.
        /// В частности если в лог пишется только время то добавляем дату
        /// начала записи лога.
        /// </summary>
        /// <param name="sTime">Дата и время из события</param>
        /// <returns>Правильное дата и время</returns>
        private DateTime CorrectTime(DateTime sTime)
        {
            if (sTime.Year == 1)
            {
                if (LogStarted != null)
                {
                    if (sTime.TimeOfDay < LogStarted.Value.TimeOfDay)
                    {
                        sTime = LogStarted.Value.Date.Add(sTime.TimeOfDay);
                        return sTime.AddDays(1);
                    }
                    else
                    {
                        return LogStarted.Value.Date.Add(sTime.TimeOfDay);
                    }
                }
                else
                {
                    return DateTime.Today.Date.Add(sTime.TimeOfDay);
                }
            }
            else
            {
                return sTime;
            }

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
