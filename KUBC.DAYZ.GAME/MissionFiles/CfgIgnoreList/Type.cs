using System.Xml.Serialization;

namespace KUBC.DAYZ.GAME.MissionFiles.CfgIgnoreList
{
    /// <summary>
    /// Элемент для игнора
    /// </summary>
    public class Type
    {
        /// <summary>
        /// Игнорируемый тип
        /// </summary>
        [XmlAttribute("name")]
        public string Name { get; set; } = string.Empty;
    }
}
