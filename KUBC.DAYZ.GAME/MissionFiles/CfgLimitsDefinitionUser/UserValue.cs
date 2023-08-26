using System.Xml.Serialization;

namespace KUBC.DAYZ.GAME.MissionFiles.CfgLimitsDefinitionUser
{
    /// <summary>
    /// Пользовательский набор редкости использования
    /// </summary>
    public class UserValue
    {
        /// <summary>
        /// Имя пользовательского названия
        /// </summary>
        [XmlAttribute("name")]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Список объема лута которые определяет пользовательский набор
        /// </summary>
        [XmlElement("value")]
        public List<DB.Types.Value> Values { get; set; } = new();
    }
}
