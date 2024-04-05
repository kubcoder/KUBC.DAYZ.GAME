using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUBC.DAYZ.GAME.LogFiles.RPT
{
    /// <summary>
    /// Элемент журнала RPT
    /// </summary>
    public abstract class RPTEntity : LogEntity
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
