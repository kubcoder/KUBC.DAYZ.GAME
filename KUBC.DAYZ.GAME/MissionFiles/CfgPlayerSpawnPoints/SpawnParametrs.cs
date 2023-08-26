using System.Xml.Serialization;

namespace KUBC.DAYZ.GAME.MissionFiles.CfgPlayerSpawnPoints
{
    /// <summary>
    /// Настройка дистанций определяющих выбор точки спавна игрока
    /// </summary>
    public class SpawnParametrs
    {
        /// <summary>
        /// Минимальная дистанция от зараженного
        /// </summary>
        [XmlElement("min_dist_infected")]
        public float MinInfected { get; set; } = 0;
        /// <summary>
        /// Максимальная дистанция от зараженного
        /// </summary>
        [XmlElement("max_dist_infected")]
        public float MaxInfected { get; set; } = 0;
        /// <summary>
        /// Минимальная дистанция до других игроков
        /// </summary>
        [XmlElement("min_dist_player")]
        public float MinPlayer { get; set; } = 0;
        /// <summary>
        /// Максимальная дистанция до других игроков
        /// </summary>
        [XmlElement("max_dist_player")]
        public float MaxPlayer { get; set; } = 0;

        /// <summary>
        /// Минимальная дистанция до строений
        /// </summary>
        /// <remarks>
        /// Статические объекты... не понятно будут ли учитываться
        /// статические объекты которые строятся на старте сервера.
        /// </remarks>
        [XmlElement("min_dist_static")]
        public float MinStatic { get; set; } = 0;

        /// <summary>
        /// Максимальная дистанция до стат. объектов
        /// </summary>
        [XmlElement("max_dist_static")]
        public float MaxStatic { get; set; } = 0;

    }
}
