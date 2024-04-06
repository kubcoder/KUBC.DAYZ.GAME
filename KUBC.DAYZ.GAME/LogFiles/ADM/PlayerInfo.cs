using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace KUBC.DAYZ.GAME.LogFiles.ADM
{
    /// <summary>
    /// Представление игрока в журнале админов
    /// </summary>
    public class PlayerInfo
    {
        /// <summary>
        /// Идентификатор игрока (это заветные 44 буковки, мать их так)
        /// </summary>
        [XmlAttribute]
        public string ID { get; set; } = string.Empty;
        /// <summary>
        /// Никнейм игрока
        /// </summary>
        [XmlText]
        public string NickName { get; set; } = string.Empty;
    }
}
