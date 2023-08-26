using System.Xml.Serialization;

namespace KUBC.DAYZ.GAME.MissionFiles.CfgWeather
{
    /// <summary>
    /// Пороговые значения
    /// </summary>
    public class Thresholds : RangeFloat
    {
        /// <summary>
        /// Время в секундах через которое дождь прекратится
        /// если облачность вышла за указанный диапазон.
        /// </summary>
        [XmlAttribute("end")]
        public int End { get; set; } = 120;
    }
}
