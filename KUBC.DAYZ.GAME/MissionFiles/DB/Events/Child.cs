using System.Xml.Serialization;

namespace KUBC.DAYZ.GAME.MissionFiles.DB.Events
{
    /// <summary>
    /// Дочерний элемент события
    /// </summary>
    [Serializable]
    [XmlType("child")]
    public class Child
    {
        /// <summary>
        /// Максимальное число элементов в конкретном событии
        /// </summary>
        [XmlAttribute("lootmax")]
        public int LootMax { get; set; } = 0;
        /// <summary>
        /// Минимальное число элементов в конкретном событии
        /// </summary>
        [XmlAttribute("lootmin")]
        public int LootMin { get; set; } = 0;
        /// <summary>
        /// Максимальное число элементов во всех событиях
        /// </summary>
        [XmlAttribute("max")]
        public int Max { get; set; } = 0;
        /// <summary>
        /// Минимальное число элементов во всех событиях
        /// </summary>
        [XmlAttribute("min")]
        public int Min { get; set; } = 0;
        /// <summary>
        /// Наименование элемента который спавнится
        /// </summary>
        [XmlAttribute("type")]
        public string Type { get; set; } = string.Empty;
    }
}
