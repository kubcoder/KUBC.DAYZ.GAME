using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace KUBC.DAYZ.GAME.LogFiles.RPT
{
    /// <summary>
    /// Среднее ФПС сервера
    /// </summary>
    public class AverageFPS : OneLineEntity
    {
        /// <summary>
        /// Измеренный ФПС
        /// </summary>
        [XmlText]
        public float FPS { get; set; } = 0;

    }
}
