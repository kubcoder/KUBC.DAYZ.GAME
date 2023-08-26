using System.Xml.Serialization;

namespace KUBC.DAYZ.GAME.MissionFiles.CfgEnvironment
{
    /// <summary>
    /// Элемент настройки территории или агента
    /// </summary>
    public class Item
    {
        /// <summary>
        /// Имя параметра
        /// </summary>
        [XmlAttribute("name")]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Строковое представление значения
        /// </summary>
        [XmlAttribute("val")]
        public string Value { get; set; } = string.Empty;
    }
}
