using System.Xml.Serialization;

namespace KUBC.DAYZ.GAME.MissionFiles.CfgWeather
{
    /// <summary>
    /// Компонент погоды.
    /// Т.е. может быть параметр непогода или туман, или дождь
    /// </summary>
    public class WeatherComponent
    {
        /// <summary>
        /// Начальные параметры погоды
        /// </summary>
        [XmlElement("current")]
        public Current Current { get; set; } = new();
        /// <summary>
        /// Каких предельных значений может достигать данный параметр погоды
        /// </summary>
        [XmlElement("limits")]
        public RangeFloat Limits { get; set; } = new();
        /// <summary>
        /// На сколько времени фиксируется текущая погода.
        /// </summary>
        [XmlElement("timelimits")]
        public TimeLimits TimeLimits { get; set; } = new();

        /// <summary>
        /// На сколько может изменится погода за раз.
        /// </summary>
        /// <remarks>
        /// На сколько я понял в момент когда нужно изменить 
        /// параметр погоды, из данного диапазона выбирается случайное число
        /// И это число добавляется(или отнимается) от текущего значения параметра.
        /// Фактически этот параметр влияет на резкость смены погоды.
        /// </remarks>
        [XmlElement("changelimits")]
        public RangeFloat ChangeLimits { get; set; } = new();
    }
}
