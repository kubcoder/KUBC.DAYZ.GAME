using System.Xml.Serialization;

namespace KUBC.DAYZ.GAME.MissionFiles.CfgEnvironment
{
    /// <summary>
    /// Элемент спавна
    /// </summary>
    public class Spawn
    {
        /// <summary>
        /// Имя элемента для спавна
        /// </summary>
        [XmlAttribute("configName")]
        public string ConfigName { get; set; } = string.Empty;


        /// <summary>
        /// Шанс спавна данного итема
        /// </summary>
        [XmlAttribute("chance")]
        public string? Chance { get; set; }
    }
}
