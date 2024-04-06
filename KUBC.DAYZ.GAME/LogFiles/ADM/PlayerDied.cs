using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUBC.DAYZ.GAME.LogFiles.ADM
{
    /// <summary>
    /// Событие смерти игрока
    /// </summary>
    public class PlayerDied : PositionLogEntity
    {
        /// <summary>
        /// Вода в игроке
        /// </summary>
        public float Water { get; set; } = 0;
        /// <summary>
        /// Энергия
        /// </summary>
        public float Energy { get; set; } = 0;
        /// <summary>
        /// Колличество открытых ран
        /// </summary>
        public int Bleeding { get; set; } = 0;
    }
    /// <summary>
    /// Парсер события <see cref="PlayerDied"/>
    /// </summary>
    public class PlayerDiedParser : ADMPositionParser
    {
        /// <inheritdoc/>
        protected override string GetTAG()
        {
            return "died. Stats>";
        }
        /// <inheritdoc/>
        public override ILogEntity? CreateEntity(string logLine, CancellationToken? cancellation = null)
        {
            if (Init(logLine, cancellation))
            {
#pragma warning disable CS8601 // Возможные null отсечены в родительском классе
                var res = new PlayerDied()
                {
                    Player = Player,
                    Position = Position,
                    Time = logTime.GetValueOrDefault()
                };
#pragma warning restore CS8601
                if (SkipToChar(':', cancellation))
                {
                    var water = ReadFloat(' ', true, cancellation);
                    if (water.HasValue)
                    {
                        res.Water = water.Value;
                    }
                    if (SkipToChar(':', cancellation))
                    {
                        var energy = ReadFloat(' ', true, cancellation);
                        if (energy.HasValue)
                        {
                            res.Energy = energy.Value;
                        }
                        if (SkipToChar(':', cancellation))
                        {
                            var bleeding = ReadInt(' ', true, cancellation);
                            if (bleeding.HasValue)
                            {
                                res.Bleeding = bleeding.Value;
                            }
                        }
                    }
                }
                return res;
            }
            return null;
        }
    }
}
