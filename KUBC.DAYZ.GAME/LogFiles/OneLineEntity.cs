using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUBC.DAYZ.GAME.LogFiles
{
    /// <summary>
    /// Данные лога которые формируются из одной строчки лога
    /// </summary>
    public abstract class OneLineEntity: LogEntity
    {
        /// <inheritdoc/>
        public override bool AppendLine(string Line)
        {
            return false;
        }
        /// <inheritdoc/>
        public override bool IsEndRead()
        {
            return true;
        }
    }
}
