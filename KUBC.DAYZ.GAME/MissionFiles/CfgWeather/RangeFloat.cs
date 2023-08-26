using System.Xml.Serialization;

namespace KUBC.DAYZ.GAME.MissionFiles.CfgWeather
{
    /// <summary>
    /// Лимиты изменения параметра погоды.
    /// По умолчанию любой параметр может меняться от 0 до 1.
    /// </summary>
    public class RangeFloat
    {
        /// <summary>
        /// Минимальное значение параметра
        /// </summary>
        [XmlAttribute("min")]
        public float Min { get; set; } = 0;
        /// <summary>
        /// Максимальное значение параметра
        /// </summary>
        [XmlAttribute("max")]
        public float Max { get; set; } = 1;


    }
}
