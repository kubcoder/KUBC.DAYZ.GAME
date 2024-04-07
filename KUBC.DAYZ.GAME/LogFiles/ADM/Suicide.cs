using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUBC.DAYZ.GAME.LogFiles.ADM
{
    /// <summary>
    /// Событие самоубийства игрока
    /// </summary>
    public class Suicide : PositionLogEntity
    {
    }

    /// <summary>
    /// Парсер события <see cref="Suicide"/>
    /// </summary>
    public class SuicideParser : ADMPositionParser
    {
        /// <inheritdoc/>
        public override ILogEntity? CreateEntity(string logLine, CancellationToken? cancellation = null)
        {
            if (Init(logLine, cancellation))
            {
                Dispose();
#pragma warning disable CS8601 // Возможные null отсечены в родительском классе
                return new Suicide()
                {
                    Player = Player,
                    Position = Position,
                    Time = logTime.GetValueOrDefault()
                };
#pragma warning restore CS8601
            }
            else
            {
                if (Player!=null)
                {
                    Dispose();
                    return new Suicide()
                    {
                        Player = Player,
                        Time = logTime.GetValueOrDefault()
                    };
                }
            }
            return null;
        }
        /// <inheritdoc/>
        protected override string GetTAG()
        {
            return "committed suicide";
        }
    }
}
