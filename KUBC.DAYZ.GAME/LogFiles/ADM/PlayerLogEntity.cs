using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUBC.DAYZ.GAME.LogFiles.ADM
{
    /// <summary>
    /// Базовый класс событий журнала администрирования
    /// </summary>
    public abstract class PlayerLogEntity:OneLineEntity
    {
        /// <summary>
        /// Игрок который засветился в событии
        /// </summary>
        public PlayerInfo Player { get; set; } = new();
    }
}
