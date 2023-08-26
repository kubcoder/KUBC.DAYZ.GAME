using System.Xml.Serialization;

namespace KUBC.DAYZ.GAME.MissionFiles.CfgPlayerSpawnPoints
{
    /// <summary>
    /// Точка спавна
    /// </summary>
    /// <remarks>
    /// Определяет цент сетки точки спавна
    /// </remarks>
    public class Position
    {
        /// <summary>
        /// Координата запад-восток
        /// </summary>
        [XmlAttribute("x")]
        public float X { get; set; } = 0;

        /// <summary>
        /// Координата Север - Юг
        /// </summary>
        [XmlAttribute("z")]
        public float Z { get; set; } = 0;
    }
}
