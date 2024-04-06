using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUBC.DAYZ.GAME.LogFiles.ADM
{
    /// <summary>
    /// Элемент лога где есть игрок, координата и какая то неведома фигня
    /// </summary>
    public abstract class ItemLogEntity : PositionLogEntity
    {
        /// <summary>
        /// Используемый итем
        /// </summary>
        public string ItemName { get; set; } = string.Empty;
    }
}
