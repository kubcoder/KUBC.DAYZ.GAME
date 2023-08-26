using System.Xml.Serialization;

namespace KUBC.DAYZ.GAME.MissionFiles.CfgPlayerSpawnPoints
{
    /// <summary>
    /// Настройки куска спавна
    /// </summary>
    public class SpawnSection
    {
        /// <summary>
        /// Настройки логики выбора точки
        /// </summary>
        [XmlElement("spawn_params")]
        public SpawnParametrs? SpawnParams { get; set; }
        /// <summary>
        /// Настройка генерации сетки спавна
        /// </summary>
        [XmlElement("generator_params")]
        public GeneratorParams? Generator { get; set; }
        /// <summary>
        /// Список точек для спавна
        /// </summary>
        [XmlElement("generator_posbubbles")]
        public GeneratorPosbubbles Posbubbles { get; set; } = new();
    }
}
