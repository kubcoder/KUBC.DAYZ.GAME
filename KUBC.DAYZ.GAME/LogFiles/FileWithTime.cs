using KUBC.DAYZ.GAME.LogFiles.RPT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUBC.DAYZ.GAME.LogFiles
{
    /// <summary>
    /// Файл лога с временем в каждой строчке
    /// </summary>
    public abstract class FileWithTime : File
    {

        /// <summary>
        /// Дата и время начала лога
        /// </summary>
        public DateTime? LogStarted;

        /// <summary>
        /// Парсер лога
        /// </summary>
        protected abstract ILogEntityFabric LogParser { get; }

        /// <summary>
        /// Корректируем дату и время.
        /// В частности если в лог пишется только время то добавляем дату
        /// начала записи лога.
        /// </summary>
        /// <param name="sTime">Дата и время из события</param>
        /// <returns>Правильное дата и время</returns>
        protected DateTime CorrectTime(DateTime sTime)
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

        /// <summary>
        /// Попробывать найти дату и время начала лога
        /// </summary>
        /// <param name="Line">Строчка лога</param>
        /// <returns>Обработана ли строчка, если истина, значит строчка прочитана дальше её больше не трогаем</returns>
        protected abstract bool FindStartTime(string Line);

        /// <inheritdoc/>
        protected override void OnFileOpen()
        {
            LogStarted = null;
        }

        /// <inheritdoc/>
        protected override ILogEntity? ParseLine(string LogLine)
        {
            if (!FindStartTime(LogLine))
            {
                if (LogStarted!=null)
                {
                    var entity = LogParser.CreateEntity(LogLine);
                    if (entity != null)
                    {
                        entity.Time = CorrectTime(entity.Time);
                        return entity;
                    }
                }
            }
            return null;
        }
    }
}
