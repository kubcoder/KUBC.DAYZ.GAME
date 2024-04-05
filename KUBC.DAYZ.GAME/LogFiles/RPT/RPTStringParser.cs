using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUBC.DAYZ.GAME.LogFiles.RPT
{
    /// <summary>
    /// Парсер строчки лога RPT.
    /// </summary>
    /// <remarks>
    /// При инициализации читаем время события
    /// </remarks>
    public abstract class RPTStringParser:StringParser
    {
        /// <summary>
        /// Дата и время из строчки лога.
        /// </summary>
        protected DateTime? logTime;

        protected override bool Init(string Line, CancellationToken? cancellation = null)
        {
            base.Init(Line, cancellation);
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
                if (DateTime.TryParse(TimeString, out var pFTime))
                {
                    logTime = pFTime;
                }
                else
                {
                    return false;
                }
            }
            return true;
        }
    }
}
