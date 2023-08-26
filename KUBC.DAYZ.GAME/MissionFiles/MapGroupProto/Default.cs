using System.Xml.Serialization;

namespace KUBC.DAYZ.GAME.MissionFiles.MapGroupProto
{
    /// <summary>
    /// Значение по умолчанию
    /// </summary>
    public class Default
    {
        /// <summary>
        /// Название параметра
        /// </summary>
        [XmlAttribute("name")]
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// Значение кол-ва лута по умолчанию
        /// </summary>
        [XmlAttribute("lootmax")]
        public string? LootMaxAttr { get; set; }

        /// <summary>
        /// Атрибут включения чего-то
        /// </summary>
        [XmlAttribute("enabled")]
        public string? EnabledAttr { get; set; }
        /// <summary>
        /// Имя события по умолчанию
        /// </summary>
        [XmlAttribute("de")]
        public string? DE { get; set; }
        /// <summary>
        /// Ширина чего-то
        /// </summary>
        [XmlAttribute("width")]
        public string? WidthAttr { get; set; }
        /// <summary>
        /// Высота чего-то
        /// </summary>
        [XmlAttribute("height")]
        public string? HeightAttr { get; set; }
    }
}
