using System.Xml.Serialization;

namespace KUBC.DAYZ.GAME.MissionFiles.DB.Types
{
    /// <summary>
    /// В каких объектах может спавнится данный лут, например военные объекты, полиция или что то еще.
    /// </summary>
    /// <remarks>
    /// Возможные значения определены в файле cfglimitsdefinition.xml
    /// </remarks>
    [Serializable]
    [XmlType("usage")]
    public class Usage
    {
        /// <summary>
        /// Имя объекта
        /// </summary>
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; } = string.Empty;
    }
}
