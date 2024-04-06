using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUBC.DAYZ.GAME.LogFiles.ADM
{
    /// <summary>
    /// Событие поднятия тотема
    /// </summary>
    public class Raised:Lowered
    {
    }
    /// <summary>
    /// Парсер события <see cref="Raised"/>
    /// </summary>
    public class RaisedParser : ADMPositionParser
    {
        private const string START = "has raised";

        /// <inheritdoc/>
        public override ILogEntity? CreateEntity(string logLine, CancellationToken? cancellation = null)
        {
            if (logLine.Contains(START))
            {
                if (Init(logLine, cancellation))
                {
#pragma warning disable CS8601 // Возможные null отсечены в родительском классе
                    var res = new Raised()
                    {
                        Player = Player,
                        Position = Position,
                        Time = logTime.GetValueOrDefault()
                    };
#pragma warning restore CS8601
                    var w = ReadToChar(' ', true, cancellation);
                    w = ReadToChar(' ', true, cancellation);
                    w = ReadToChar(' ', true, cancellation);
                    if (!string.IsNullOrEmpty(w))
                    {
                        res.ItemName = w.Trim();
                        w = ReadToChar(' ', true, cancellation);
                        w = ReadToChar(' ', true, cancellation);
                        if (!string.IsNullOrEmpty(w))
                        {
                            res.Totem = w.Trim();
                        }
                        return res;
                    }
                    return res;
                }
            }
            return null;
        }
    }
}
