using System.Xml.Serialization;

namespace KUBC.DAYZ.GAME.MissionFiles.CfgEnvironment
{
    /// <summary>
    /// Описание территории обитания
    /// </summary>
    [Serializable]
    [XmlType("territory")]
    public class Territory
    {

        /// <summary>
        /// Тип территории
        /// </summary>
        [XmlAttribute("type")]
        public string Type { get; set; } = string.Empty;

        /// <summary>
        /// Название территории
        /// </summary>
        [XmlAttribute("name")]
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// Поведение группы AI
        /// </summary>
        [XmlAttribute("behavior")]
        public string Behavior { get; set; } = string.Empty;

        /// <summary>
        /// Какой файл использовать для территорий
        /// </summary>
        [XmlElement("file")]
        public FileUsable File { get; set; } = new();


        /// <summary>
        /// Элементы стада
        /// </summary>
        [XmlElement("agent")]
        public List<Agent> Agents { get; set; } = new();


        /// <summary>
        /// Дополнительные настройки территории
        /// </summary>
        [XmlElement("item")]
        public List<Item> Settings { get; set; } = new();

    }
}
