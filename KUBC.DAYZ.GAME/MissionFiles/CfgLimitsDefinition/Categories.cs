using System.Xml.Serialization;

namespace KUBC.DAYZ.GAME.MissionFiles.CfgLimitsDefinition
{
    /// <summary>
    /// Список названий категорий
    /// </summary>
    public class Categories
    {
        /// <summary>
        /// Список категорий
        /// </summary>
        [XmlElement("category")]
        public List<DB.Types.Category> Names { get; set; } = new();
    }
}
