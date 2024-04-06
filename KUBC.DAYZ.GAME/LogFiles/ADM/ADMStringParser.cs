using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUBC.DAYZ.GAME.LogFiles.ADM
{
    /// <summary>
    /// Парсер строчки лога ADM.
    /// </summary>
    /// <remarks>
    /// Добавлено что при инициализации выполняется чтение даты или времени 
    /// когда была добавлена строчка если это не получилось, разбор 
    /// парсером будет прерван.
    /// </remarks>
    public abstract class ADMStringParser : LineWithTimeParser
    {
        /// <inheritdoc/>
        protected override bool ReadTime(CancellationToken? cancellation = null)
        {
            if (!SkipChar(' ', cancellation))
            {
                return false;
            }
            var TimeString = ReadToChar(' ', false, cancellation);
            if (TimeSpan.TryParse(TimeString, out var pTime))
            {
                logTime = DateTime.MinValue.Add(pTime);
            }
            else
            {
                return false;
            }
            if (!SkipChar('|', cancellation))
            {
                return false;
            }
            return true;
        }

        

        

    }
}
