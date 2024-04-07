using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUBC.DAYZ.GAME.LogFiles
{
    /// <summary>
    /// Парсер строки с датой и временем в начале
    /// </summary>
    public abstract class LineWithTimeParser : StringParser
    {
        /// <summary>
        /// Дата и время из строчки лога.
        /// </summary>
        protected DateTime? logTime;

        /// <inheritdoc/>
        protected override bool Init(string Line, CancellationToken? cancellation = null)
        {
            Dispose();
            logTime = null;
            if (base.Init(Line, cancellation))
                return ReadTime();
            return false;
        }
        /// <summary>
        /// Прочитать дату и время в начале строки
        /// </summary>
        /// <returns></returns>
        protected abstract bool ReadTime(CancellationToken? cancellation = null);
        
    }
}
