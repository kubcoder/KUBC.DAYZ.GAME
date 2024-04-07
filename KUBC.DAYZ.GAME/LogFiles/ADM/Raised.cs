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
    public class Raised : Lowered
    {
    }
    /// <summary>
    /// Парсер события <see cref="Raised"/>
    /// </summary>
    public class RaisedParser : ADMPositionParser
    {
        /// <inheritdoc/>
        protected override string GetTAG()
        {
            return "has raised";
        }
        /// <inheritdoc/>
        public override ILogEntity? CreateEntity(string logLine, CancellationToken? cancellation = null)
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
                    Dispose();
                    return res;
                }
                Dispose();
                return res;
            }
            return null;
        }
    }
}
