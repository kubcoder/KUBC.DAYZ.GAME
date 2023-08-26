using System.Xml.Serialization;

namespace KUBC.DAYZ.GAME.MissionFiles.CfgEconomyCore
{
    /// <summary>
    /// Элемент настройки центральной экономики
    /// </summary>
    [XmlType("default")]
    public class DefaultEntity
    {
        /// <summary>
        /// Название параметра
        /// </summary>
        [XmlAttribute("name")]
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// Значение параметра в текстовом виде
        /// </summary>
        [XmlAttribute("value")]
        public string StringValue { get; set; } = string.Empty;
    }
}
