using System.Xml.Serialization;

namespace KUBC.DAYZ.GAME.MissionFiles.CfgWeather
{
    /// <summary>
    /// Начальное значение погоды.
    /// Т.е. данные параметры применяются при старте сервера (если <see cref="File.Reset"/> истина)
    /// Или при первом запуске сервера после вайпа (если <see cref="File.Reset"/> ложь)
    /// </summary>
    /// <remarks>
    /// Т.е. данные параметры применяются при старте сервера (если <see cref="File.Reset"/> истина)
    /// Или при первом запуске сервера после вайпа (если <see cref="File.Reset"/> ложь)
    /// </remarks>
    public class Current
    {
        /// <summary>
        /// Значение параметра погоды
        /// </summary>
        [XmlAttribute("actual")]
        public float Actual { get; set; } = 0;
        /// <summary>
        /// Время через которое изменяется параметр погоды.
        /// </summary>
        /// <remarks>
        /// Судя по всему это время в секундах, значение 120 означает
        /// что погода не может изменятся чаще чем каждые 120 секунд (т.е. две минуты)
        /// </remarks>
        [XmlAttribute("time")]
        public int Time { get; set; } = 120;
        /// <summary>
        /// На сколько долго параметр остается стабильным.
        /// </summary>
        [XmlAttribute("duration")]
        public int Duration { get; set; } = 240;

    }
}
