using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUBC.DAYZ.GAME.LogFiles.ADM
{
    /// <summary>
    /// Парсер который после чтения игрока, еще пытается прочитать позицию произошедшего
    /// </summary>
    public abstract class ADMPositionParser : ADMPlayerParser
    {
        /// <summary>
        /// Где произошли сложности
        /// </summary>
        protected Vector? Position;

        protected override bool Init(string Line, CancellationToken? cancellation = null)
        {
            if (base.Init(Line, cancellation))
            {
                Position = ReadPosition(')', cancellation); 
                return Position != null;
            }
            return false;
        }
    }
}
