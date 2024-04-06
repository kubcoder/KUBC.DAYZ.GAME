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
    public class DugIn : ItemLogEntity
    {
        
    }

    /// <summary>
    /// Парсер события <see cref="DugIn"/>
    /// </summary>
    public class DugInParser : ADMPositionParser
    {
        

        /// <inheritdoc/>
        protected override string GetTAG()
        {
            return "Dug in";
        }
        /// <inheritdoc/>
        public override ILogEntity? CreateEntity(string logLine, CancellationToken? cancellation = null)
        {
            if (Init(logLine, cancellation))
            {
#pragma warning disable CS8601 // Возможные null отсечены в родительском классе
                var res = new DugIn()
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
