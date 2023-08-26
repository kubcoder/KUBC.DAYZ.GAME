using System.Xml.Serialization;

namespace KUBC.DAYZ.GAME.MissionFiles.CfgSpawnableTypes
{
    /// <summary>
    /// Уровень дамажа... 
    /// </summary>
    public class Damage
    {
        /// <summary>
        /// Минимальный дамаж на спавне
        /// </summary>
        [XmlAttribute("min")]
        public float Min { get; set; } = 0;

        /// <summary>
        /// Максимальный дамаж на спавне
        /// </summary>
        [XmlAttribute("max")]
        public float Max { get; set; } = 0;
    }
}
