using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUBC.DAYZ.GAME.LogFiles.ADM
{
    /// <summary>
    /// Событие спуска флага
    /// </summary>
    public class Lowered:ItemLogEntity
    {
        /// <summary>
        /// Тотем на котором это было сделано
        /// </summary>
        public string Totem { get; set; } = string.Empty;
    }

    /// <summary>
    /// Парсер события <see cref="Lowered"/>
    /// </summary>
    public class LoweredParser : ADMPositionParser
    {
        /// <inheritdoc/>
        protected override string GetTAG()
        {
            return "has lowered";
        }
        /// <inheritdoc/>
        public override ILogEntity? CreateEntity(string logLine, CancellationToken? cancellation = null)
        {
            if (Init(logLine, cancellation))
            {
#pragma warning disable CS8601 // Возможные null отсечены в родительском классе
                var res = new Lowered()
                {
                    Player = Player,
                    Position = Position,
                    Time = logTime.GetValueOrDefault()
                };
#pragma warning restore CS8601
                ReadToChar(' ', true, cancellation);
                ReadToChar(' ', true, cancellation);
                var w = ReadToChar(' ', true, cancellation);
                if (!string.IsNullOrEmpty(w))
                {
                    res.ItemName = w.Trim();
                    ReadToChar(' ', true, cancellation);
                    w = ReadToChar(' ', true, cancellation);
                    if (!string.IsNullOrEmpty(w))
                    {
                        res.Totem = w.Trim();
                    }
                    return res;
                }
                return res;
            }
            return null;
        }
    }
}
