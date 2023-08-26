using System.Xml.Serialization;

namespace KUBC.DAYZ.GAME.MissionFiles.DB.Types
{
    /// <summary>
    /// В каких зонах Tier разрешен спавн данного итема
    /// </summary>
    /// <remarks>
    /// Возможные значения определены в файле cfglimitsdefinition.xml
    /// </remarks>
    [Serializable]
    [XmlType("value")]
    public class Value
    {
        /// <summary>
        /// Имя разрешенной зоны спавна
        /// </summary>
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; } = string.Empty;

    }
}
