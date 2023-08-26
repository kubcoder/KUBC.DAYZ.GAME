using System.Xml.Serialization;

namespace KUBC.DAYZ.GAME.MissionFiles.CfgWeather
{
    /// <summary>
    /// Лимиты времени состояния
    /// </summary>
    /// <remarks>
    /// На сколько я понял это опеределяет сколько времени параметр погоды остается неизменным...
    /// Т.е. из диапазона времени в секундах выбирается случайное число и на это время параметр
    /// фиксируется и остается не изменным. После чего может изменится, и снова фиксируется.
    /// </remarks>
    public class TimeLimits
    {
        /// <summary>
        /// Минимальное значение времени состояния погоды
        /// </summary>
        [XmlAttribute("min")]
        public int Min { get; set; } = 900;
        /// <summary>
        /// Максимальное значение времени состояния погоды
        /// </summary>
        [XmlAttribute("max")]
        public int Max { get; set; } = 1800;
    }
}
