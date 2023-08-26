using System.Xml.Serialization;

namespace KUBC.DAYZ.GAME.MissionFiles.CfgWeather
{
    /// <summary>
    /// Параметры изменения ветра
    /// </summary>
    public class WindParams : RangeFloat
    {
        /// <summary>
        /// Частота изменения ветра
        /// </summary>
        [XmlAttribute("frequency")]
        public int Frequency { get; set; } = 30;
    }
}
