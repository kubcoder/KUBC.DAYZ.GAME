using System.Xml.Serialization;

namespace KUBC.DAYZ.GAME.MissionFiles.MapGroupPos
{
    /// <summary>
    /// Группа спавна
    /// </summary>
    public class Group
    {
        /// <summary>
        /// Имя типа группы спавна
        /// </summary>
        /// <remarks>
        /// Данное имя должно быть определено в <see cref="MapGroupProto.File"/>
        /// </remarks>
        [XmlAttribute("name")]
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// Позиция группы в мире
        /// </summary>
        [XmlIgnore]
        public Vector Position = new();

        /// <summary>
        /// Координата группы в мире в виде строки
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
        /// Разворот группы в мире
        /// </summary>
        [XmlIgnore]
        public Vector RPY = new();

        /// <summary>
        /// Координата группы в мире в виде строки
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

        /// <summary>
        /// Угол разворота кластера в мире
        /// </summary>
        [XmlAttribute("a")]
        public float A { get; set; } = 0;
    }
}
