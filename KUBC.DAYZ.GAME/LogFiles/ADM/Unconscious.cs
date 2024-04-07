using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUBC.DAYZ.GAME.LogFiles.ADM
{
    /// <summary>
    /// Описание события когда игрок потерял сознание
    /// </summary>
    public class Unconscious : PositionLogEntity
    {
    }
    /// <summary>
    /// Парсер события <see cref="Unconscious"/>
    /// </summary>
    public class UnconsciousParser : ADMPositionParser
    {
        /// <inheritdoc/>
        public override ILogEntity? CreateEntity(string logLine, CancellationToken? cancellation = null)
        {
            if (Init(logLine, cancellation))
            {
                Dispose();
#pragma warning disable CS8601 // Возможные null отсечены в родительском классе
                return new Unconscious()
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
            return "is unconscious";
        }
    }
}
