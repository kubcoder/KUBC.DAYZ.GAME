using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUBC.DAYZ.GAME.LogFiles.ADM
{
    /// <summary>
    /// Парсер который после прочтения даты и времени, пытается прочитать игрока
    /// </summary>
    public abstract class ADMPlayerParser : ADMStringParser
    {
        /// <summary>
        /// Игрок который замешан в проихсодящем
        /// </summary>
        protected PlayerInfo? Player;

        /// <inheritdoc/>
        protected override bool Init(string Line, CancellationToken? cancellation = null)
        {
            if (base.Init(Line, cancellation))
            {
                Player = ReadPlayer(cancellation);
                return Player!= null;
            }
            return false;
        }
    }
}
