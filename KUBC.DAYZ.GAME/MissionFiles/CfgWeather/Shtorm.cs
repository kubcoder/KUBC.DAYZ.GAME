using System.Xml.Serialization;

namespace KUBC.DAYZ.GAME.MissionFiles.CfgWeather
{
    /// <summary>
    /// Настройки грозы в игре
    /// </summary>
    public class Shtorm
    {
        /// <summary>
        /// Плотность вспышек молнии. Я хрен знает что это означает...
        /// Lightning density - вот честно не могу понять как это адаптировать на русский
        /// </summary>
        [XmlAttribute("density")]
        public float Density { get; set; } = 1;
        /// <summary>
        /// На каком уровне облачности могут быть молнии
        /// </summary>
        [XmlAttribute("threshold")]
        public float Threshold { get; set; } = 0.7f;
        /// <summary>
        /// Время между вспышками молний... т.е. не чаще чем указано тут
        /// И да это время в секундах
        /// </summary>
        [XmlAttribute("timeout")]
        public int TimeOut { get; set; } = 25;
    }
}
