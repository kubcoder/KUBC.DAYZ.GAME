using System.Xml.Serialization;

namespace KUBC.DAYZ.GAME.MissionFiles.CfgEventGroups
{
    /// <summary>
    /// Группа спавна объектов в событии
    /// </summary>
    public class Group
    {
        /// <summary>
        /// Имя группы события
        /// </summary>
        [XmlAttribute("name")]
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// Объекты группы
        /// </summary>
        [XmlElement("child")]
        public List<Child> Childs { get; set; } = new List<Child>();
    }
}
