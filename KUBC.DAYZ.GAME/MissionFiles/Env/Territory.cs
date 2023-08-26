using System.Xml.Serialization;

namespace KUBC.DAYZ.GAME.MissionFiles.Env
{
    /// <summary>
    /// Описание территории
    /// </summary>
    public class Territory
    {
        /// <summary>
        /// Цвет зоны... судя по всему используется только в EconomyEditor
        /// Можно сказать для красоты
        /// </summary>
        [XmlAttribute("color")]
        public string? Color { get; set; }
        /// <summary>
        /// Зоны спавна
        /// </summary>
        [XmlElement("zone")]
        public List<Zone> Zones { get; set; } = new();
    }
}
