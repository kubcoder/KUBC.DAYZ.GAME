using System.Xml.Serialization;

namespace KUBC.DAYZ.GAME.MissionFiles.MapGroupProto
{
    /// <summary>
    /// Судя по всему это настройка спавна всяких запчастей
    /// прямо на статических тачилах
    /// </summary>
    public class Proxy
    {
        /// <summary>
        /// Класс элемента для спанва
        /// </summary>
        [XmlAttribute("type")]
        public string Type { get; set; } = string.Empty;

        /// <summary>
        /// Позиция точки спавна
        /// </summary>
        [XmlIgnore]
        public Vector Position = new();

        /// <summary>
        /// Координата в представлении атрибута узла XML
        /// </summary>
        [XmlAttribute("pos")]
        public string PosAsString
        {
            get
            {
                return Position.ToAttribute();
            }
            set
            {
                Position = Vector.FromAttribute(value);
            }
        }

        /// <summary>
        /// Разворот итема
        /// </summary>
        [XmlIgnore]
        public Vector RPY = new();

        /// <summary>
        /// Разворот итема в представлении атрибута узла XML
        /// </summary>
        [XmlAttribute("rpy")]
        public string RPYAsString
        {
            get
            {
                return RPY.ToAttribute();
            }
            set
            {
                RPY = Vector.FromAttribute(value);
            }
        }
    }
}
