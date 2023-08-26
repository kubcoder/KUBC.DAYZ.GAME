using System.Xml.Serialization;

namespace KUBC.DAYZ.GAME.MissionFiles.CfgEventSpawns
{
    /// <summary>
    /// Описание зоны
    /// </summary>
    [Serializable]
    [XmlType("zone")]
    public class Zone
    {
        /// <summary>
        /// Минимальное кол-во статических событий
        /// </summary>
        [XmlAttribute("smin")]
        public int Smin { get; set; } = 0;
        /// <summary>
        /// Максимальное кол-во статических событий
        /// </summary>
        [XmlAttribute("smax")]
        public int Smax { get; set; } = 0;
        /// <summary>
        /// Минимальное кол-во динамических событий
        /// </summary>
        [XmlAttribute("dmin")]
        public int Dmin { get; set; } = 0;
        /// <summary>
        /// Максимамальное кол-во динамических событий
        /// </summary>
        [XmlAttribute("dmax")]
        public int Dmax { get; set; } = 0;
        /// <summary>
        /// Вот хрен знает что это такое... вроде как радиус размещения или еще чего
        /// </summary>
        [XmlAttribute("r")]
        public int R { get; set; } = 1;
    }
}
