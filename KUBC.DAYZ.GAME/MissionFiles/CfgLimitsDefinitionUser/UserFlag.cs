using System.Xml.Serialization;

namespace KUBC.DAYZ.GAME.MissionFiles.CfgLimitsDefinitionUser
{
    /// <summary>
    /// Описание пользовательского флага
    /// </summary>
    public class UserFlag
    {
        /// <summary>
        /// Имя пользовательского флага
        /// </summary>
        [XmlAttribute("name")]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Список флагов которые определяет пользовательский набор
        /// </summary>
        [XmlElement("usage")]
        public List<DB.Types.Usage> Usages { get; set; } = new();
    }
}
