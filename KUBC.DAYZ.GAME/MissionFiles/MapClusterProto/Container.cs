using System.Xml.Serialization;

namespace KUBC.DAYZ.GAME.MissionFiles.MapClusterProto
{
    /// <summary>
    /// Точки для спавна внутри кластера.
    /// </summary>
    public class Container
    {
        /// <summary>
        /// ХЗ. какое то имя... их немного разновидностей
        /// </summary>
        [XmlAttribute("name")]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Максимальное кол-во лута в кластере
        /// </summary>
        [XmlAttribute("lootmax")]
        public int LootMax { get; set; } = 2;
        /// <summary>
        /// Точки размещения лута в кластере
        /// </summary>
        [XmlElement("point")]
        public List<Point> Points { get; set; } = new();
    }
}
