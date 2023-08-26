using System.Xml.Serialization;

namespace KUBC.DAYZ.GAME.MissionFiles.CfgWeather
{
    /// <summary>
    /// Настройки ветра в игре
    /// </summary>
    public class Wind
    {
        /// <summary>
        /// Максимальная скорость ветра в м/с
        /// </summary>
        [XmlElement("maxspeed")]
        public float MaxSpeed { get; set; } = 20;
        /// <summary>
        /// Параметры изменения ветра
        /// </summary>
        [XmlElement("params")]
        public WindParams Params { get; set; } = new();

    }
}
