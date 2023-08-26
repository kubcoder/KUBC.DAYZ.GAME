using System.Xml.Serialization;

namespace KUBC.DAYZ.GAME.MissionFiles.CfgLimitsDefinition
{
    /// <summary>
    /// Редкость итема... т.е. где он должен спавнится.
    /// </summary>
    public class Values
    {
        /// <summary>
        /// Список имен зон спавна
        /// </summary>
        [XmlElement("value")]
        public List<DB.Types.Value> Names { get; set; } = new();
    }
}
