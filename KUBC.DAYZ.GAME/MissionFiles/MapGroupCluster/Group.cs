using System.Xml.Serialization;

namespace KUBC.DAYZ.GAME.MissionFiles.MapGroupCluster
{
    /// <summary>
    /// Размещение кластера в мире
    /// </summary>
    public class Group
    {
        /// <summary>
        /// Имя типа кластера
        /// </summary>
        /// <remarks>
        /// Данное имя должно быть определено в <see cref="MapClusterProto.File"/>
        /// </remarks>
        [XmlAttribute("name")]
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// Позиция кластера 
        /// </summary>
        [XmlIgnore]
        public Vector Position = new();

        /// <summary>
        /// Координата кластера в мире в виде строки
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
        /// Угол разворота кластера в мире
        /// </summary>
        [XmlAttribute("a")]
        public float A { get; set; } = 0;
    }
}
