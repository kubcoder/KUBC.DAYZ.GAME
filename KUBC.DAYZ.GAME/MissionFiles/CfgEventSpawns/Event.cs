using System.Xml.Serialization;

namespace KUBC.DAYZ.GAME.MissionFiles.CfgEventSpawns
{
    /// <summary>
    /// Элемент описания события
    /// </summary>
    [Serializable]
    [XmlType("event")]
    public class Event
    {
        /// <summary>
        /// Название события
        /// </summary>
        [XmlAttribute("name")]
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// Настройки зон события
        /// </summary>
        [XmlElement("zone")]
        public Zone? Zone { get; set; }
        /// <summary>
        /// Координаты события
        /// </summary>
        [XmlElement(ElementName = "pos", IsNullable = true)]
        public List<Pos>? Positions { get; set; }
    }
}
