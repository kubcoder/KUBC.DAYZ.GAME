using System.Xml.Serialization;

namespace KUBC.DAYZ.GAME.MissionFiles.CfgWeather
{
    /// <summary>
    /// Настройки дождя
    /// </summary>
    public class RainComponent : WeatherComponent
    {
        /// <summary>
        /// Пороги влияния облачности на дождь.
        /// Т.е. при какой облачности должен начатся/завершится дождь.
        /// А так же время которое требуется на изменение уровня дождя при 
        /// изменении облачности.
        /// </summary>
        [XmlElement("thresholds")]
        public Thresholds Thresholds { get; set; } = new();
    }
}
