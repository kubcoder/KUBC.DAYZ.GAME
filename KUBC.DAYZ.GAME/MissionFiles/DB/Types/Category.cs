using System.Xml.Serialization;

namespace KUBC.DAYZ.GAME.MissionFiles.DB.Types
{
    /// <summary>
    /// Категория итема. Т.е. что это за итем. Элемент классификации для центральной экономики.
    /// </summary>
    /// <remarks>
    /// Возможные значения определены в файле cfglimitsdefinition.xml
    /// </remarks>
    [Serializable]
    [XmlType("category")]
    public class Category
    {
        /// <summary>
        /// Имя категории 
        /// </summary>
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; } = string.Empty;

    }
}
