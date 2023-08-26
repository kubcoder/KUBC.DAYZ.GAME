using System.Xml.Serialization;

namespace KUBC.DAYZ.GAME.MissionFiles.DB.Types
{
    /// <summary>
    /// Тэг спавна итема, т.е. где его размещать
    /// </summary>
    /// <remarks>
    /// Возможные значения определены в файле cfglimitsdefinition.xml
    /// </remarks>
    [Serializable]
    [XmlType("tag")]
    public class Tag
    {
        /// <summary>
        /// Имя тэга
        /// </summary>
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; } = string.Empty;

    }
}
