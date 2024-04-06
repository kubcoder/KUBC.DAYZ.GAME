using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUBC.DAYZ.GAME.LogFiles.ADM
{
    /// <summary>
    /// Событие "закопали барахло"
    /// </summary>
    public class DugOut : ItemLogEntity
    {
        
    }

    /// <summary>
    /// Парсер события <see cref="DugOut"/>
    /// </summary>
    public class DugOutParser : ADMPositionParser
    {
        /// <inheritdoc/>
        protected override string GetTAG()
        {
            return "Dug out";
        }
        /// <inheritdoc/>
        public override ILogEntity? CreateEntity(string logLine, CancellationToken? cancellation = null)
        {
            if (Init(logLine, cancellation))
            {
#pragma warning disable CS8601 // Возможные null отсечены в родительском классе
                var res = new DugOut()
                {
                    Player = Player,
                    Position = Position,
                    Time = logTime.GetValueOrDefault()
                };
#pragma warning restore CS8601
                var w = ReadToChar(' ', true, cancellation);
                if (!string.IsNullOrEmpty(w))
                {
                    w = ReadToChar('<', true, cancellation);
                    if (!string.IsNullOrEmpty(w))
                    {
                        res.ItemName = w;
                    }
                }
                return res;
            }
            return null;
        }
    }
}
