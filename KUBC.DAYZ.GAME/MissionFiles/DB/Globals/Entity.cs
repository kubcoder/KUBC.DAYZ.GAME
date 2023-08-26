using System.Xml.Serialization;

namespace KUBC.DAYZ.GAME.MissionFiles.DB.Globals
{
    /// <summary>
    /// Элемент глобальных настроек
    /// </summary>
    [Serializable]
    [XmlType("var")]
    public class Entity
    {
        /// <summary>
        /// Имя переменной
        /// </summary>
        [XmlAttribute("name")]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Имя переменной
        /// </summary>
        [XmlAttribute("type")]
        public int TypeCode { get; set; } = 0;

        /// <summary>
        /// Имя переменной
        /// </summary>
        [XmlAttribute("value")]
        public string TextValue { get; set; } = string.Empty;
    }
}
