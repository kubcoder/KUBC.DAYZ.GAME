using System.Xml.Serialization;

namespace KUBC.DAYZ.GAME.MissionFiles.CfgEnvironment
{
    /// <summary>
    /// Элемент стада
    /// </summary>
    public class Agent
    {
        /// <summary>
        /// Тип спавна
        /// </summary>
        [XmlAttribute("type")]
        public string Type { get; set; } = string.Empty;


        /// <summary>
        /// Шанс спавна данного AI стада
        /// </summary>
        [XmlAttribute("chance")]
        public string? Chance { get; set; }

        /// <summary>
        /// Итемы для спавна
        /// </summary>
        [XmlElement("spawn")]
        public List<Spawn> Spawns { get; set; } = new();

        /// <summary>
        /// Дополнительные настройки AI из стада
        /// </summary>
        [XmlElement("item")]
        public List<Item> Settings { get; set; } = new();
    }
}
