using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUBC.DAYZ.GAME.LogFiles.Console
{
    /// <summary>
    /// Парсер строчки лога консоли.
    /// </summary>
    /// <remarks>
    /// Добавлено что при инициализации выполняется чтение даты или времени 
    /// когда была добавлена строчка если это не получилось, разбор 
    /// парсером будет прерван.
    /// </remarks>
    public abstract class ConsoleStringParser: LineWithTimeParser
    {
        /// <inheritdoc/>
        protected override bool ReadTime(CancellationToken? cancellation = null)
        {
            var TimeString = ReadToChar(' ', false, cancellation);
            if (TimeSpan.TryParse(TimeString, out var pTime))
            {
                logTime = DateTime.MinValue.Add(pTime);
            }
            else
            {
                return false;
            }
            return true;
        }
    }
}
