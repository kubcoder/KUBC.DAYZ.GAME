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

        /// <summary>
        /// Прочитать имя игрока и его идентфикатор
        /// </summary>
        /// <param name="cancellation">Токен отмены</param>
        /// <returns>Информация о игроке, или null если прочитать не удалось</returns>
        protected PlayerInfo? ReadPlayer(CancellationToken? cancellation = null)
        {
            var pi = ReadPlayerName(cancellation);
            if (pi != null)
            {
                string id;
                if (ReadChars(44, out id, cancellation))
                {
                    pi.ID = id;
                    return pi;
                }
                else
                {
                    if (id.Contains("Unknown", StringComparison.OrdinalIgnoreCase))
                    {
                        return pi;
                    }
                }
            }
            return null;
        }
        private const string startID = "(id=";
        /// <summary>
        /// Прочитать имя игрока
        /// </summary>
        /// <param name="cancellation">Токен отмены</param>
        /// <returns>Информация о игроке, или null если прочитать не удалось</returns>
        protected PlayerInfo? ReadPlayerName(CancellationToken? cancellation = null)
        {
            var name = string.Empty;
            Read();
            while (LastSymbol.HasValue)
            {
                name += LastSymbol.Value;
                if (name.Length > startID.Length)
                {
                    if (name.EndsWith(startID))
                    {
                        var pi = new PlayerInfo(name);
                        return pi;
                    }
                }
                if ((cancellation != null) && (cancellation.Value.IsCancellationRequested)) { return null; }
                Read();
            }
            return null;
        }

        
    }
}
