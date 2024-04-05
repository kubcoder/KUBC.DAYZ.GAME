using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUBC.DAYZ.GAME.LogFiles.RPT
{
    /// <summary>
    /// Событие подключения игрока
    /// </summary>
    public class ConnectEvent : OneLineEntity
    {
        /// <summary>
        /// Steam идентификатор игрока
        /// </summary>
        public long SteamID { get; set; } = 0;
        /// <summary>
        /// Ник с которым игрок приконектился
        /// </summary>
        public string NickName { get; set; } = string.Empty;
    }
}
