using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace KUBC.DAYZ.GAME.LogFiles.RPT
{
    /// <summary>
    /// Данные об используемой памяти
    /// </summary>
    public class UsedMemory : OneLineEntity
    {
        /// <summary>
        /// Используемая память в КБ
        /// </summary>
        [XmlText]
        public long MemoryKB { get; set; } = 0;
    }
}
