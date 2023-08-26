using System.Xml.Serialization;

namespace KUBC.DAYZ.GAME.MissionFiles.CfgLimitsDefinition
{
    /// <summary>
    /// Список доступных тэгов центральной экономики
    /// </summary>
    public class Tags
    {
        /// <summary>
        /// Список имен тэгов
        /// </summary>
        [XmlElement("tag")]
        public List<DB.Types.Tag> Names { get; set; } = new();
    }
}
