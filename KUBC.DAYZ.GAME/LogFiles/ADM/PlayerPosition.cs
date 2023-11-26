using System.Xml.Serialization;

namespace KUBC.DAYZ.GAME.LogFiles.ADM
{
    /// <summary>
    /// Строчка про игрока в журнале
    /// </summary>
    public class PlayerPosition
    {
        /// <summary>
        /// Игрок списка
        /// </summary>
        public PlayerInfo Player { get; set; } = new PlayerInfo();
        /// <summary>
        /// Положение игрока в мире
        /// </summary>
        public Vector Position { get; set; } = [];
        /// <summary>
        /// Признак что игрок мертвый
        /// </summary>
        [XmlAttribute]
        public bool IsDead { get; set; } = false;
    }
}
