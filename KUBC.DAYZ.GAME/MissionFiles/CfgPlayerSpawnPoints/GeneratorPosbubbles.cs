using System.Xml.Serialization;

namespace KUBC.DAYZ.GAME.MissionFiles.CfgPlayerSpawnPoints
{
    /// <summary>
    /// Координаты точек спавна
    /// </summary>
    public class GeneratorPosbubbles
    {
        /// <summary>
        /// Список точек доступных для спавна
        /// </summary>
        [XmlElement("pos")]
        public List<Position> Positions = new();
    }
}
