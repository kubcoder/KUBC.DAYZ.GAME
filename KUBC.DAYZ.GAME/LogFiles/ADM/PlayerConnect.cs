using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUBC.DAYZ.GAME.LogFiles.ADM
{
    /// <summary>
    /// Событие подключения игрока
    /// </summary>
    public class PlayerConnect : PlayerLogEntity
    {
    }
    /// <summary>
    /// Парсер события <see cref="PlayerConnect"/>
    /// </summary>
    public class PlayerConnectParser : ADMPlayerParser
    {

        /// <inheritdoc/>
        protected override string GetTAG()
        {
            return "is connected";
        }
        /// <inheritdoc/>
        public override ILogEntity? CreateEntity(string logLine, CancellationToken? cancellation = null)
        {
            if (Init(logLine, cancellation))
            {
#pragma warning disable CS8601 // Возможные null отсечены в родительском классе
                var res = new PlayerConnect()
                {
                    Player = Player,
                    Time = logTime.GetValueOrDefault()
                };
#pragma warning restore CS8601
                return res;
            }
            return null;
        }
    }
}
