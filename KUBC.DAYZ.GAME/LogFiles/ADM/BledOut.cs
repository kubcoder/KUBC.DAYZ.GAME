using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUBC.DAYZ.GAME.LogFiles.ADM
{
    /// <summary>
    /// Событие игрок истек кровью
    /// </summary>
    public class BledOut:PositionLogEntity
    {
    }

    /// <summary>
    /// Парсер события истекания кровью
    /// </summary>
    public class BledOutParser : ADMPositionParser
    {
        /// <inheritdoc/>
        public override ILogEntity? CreateEntity(string logLine, CancellationToken? cancellation = null)
        {
            if (Init(logLine, cancellation))
            {
                Dispose();
#pragma warning disable CS8601 // Возможные null отсечены в родительском классе
                return new BledOut()
                {
                    Player = Player,
                    Position = Position,
                    Time = logTime.GetValueOrDefault()
                };
#pragma warning restore CS8601
            }
            return null;
        }
        /// <inheritdoc/>
        protected override string GetTAG()
        {
            return "bled out";
        }
    }
}
