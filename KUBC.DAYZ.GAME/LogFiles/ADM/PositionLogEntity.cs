using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUBC.DAYZ.GAME.LogFiles.ADM
{
    /// <summary>
    /// Событие в котором кроме игрока еще есть позиция происходящего
    /// </summary>
    public class PositionLogEntity:PlayerLogEntity
    {
        /// <summary>
        /// Где данное событие произошло
        /// </summary>
        public Vector Position { get; set; } = new Vector();
    }
}
