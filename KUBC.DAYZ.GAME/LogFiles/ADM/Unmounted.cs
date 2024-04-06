using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUBC.DAYZ.GAME.LogFiles.ADM
{
    /// <summary>
    /// Демонтаж навесных элементов
    /// </summary>
    public class Unmounted : Mounted
    {
    }
    /// <summary>
    /// Парсер события <see cref="Unmounted"/>
    /// </summary>
    public class UnmountedParser : ADMPositionParser
    {
        /// <inheritdoc/>
        protected override string GetTAG()
        {
            return "Unmounted";
        }
        /// <inheritdoc/>
        public override ILogEntity? CreateEntity(string logLine, CancellationToken? cancellation = null)
        {
            if (Init(logLine, cancellation))
            {
#pragma warning disable CS8601 // Возможные null отсечены в родительском классе
                var res = new Unmounted()
                {
                    Player = Player,
                    Position = Position,
                    Time = logTime.GetValueOrDefault()
                };
#pragma warning restore CS8601
                ReadToChar(' ', true, cancellation);
                ReadToChar(' ', true, cancellation);
                ReadToChar(' ', true, cancellation);
                var w = ReadToChar(' ', true, cancellation);
                if (!string.IsNullOrEmpty(w))
                {
                    res.ItemName = w.Trim();
                    w = ReadToChar(' ', true, cancellation);
                    if (w == "from")
                    {
                        w = ReadToChar(' ', true, cancellation);
                        if (!string.IsNullOrEmpty(w))
                        {
                            res.Construction = w.Trim();
                        }
                    }
                    return res;
                }
            }
            return null;
        }
    }
}
