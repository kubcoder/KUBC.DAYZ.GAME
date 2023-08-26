using System.Xml.Serialization;

namespace KUBC.DAYZ.GAME.MissionFiles.CfgLimitsDefinition
{
    /// <summary>
    /// Назначение итема, принадлежность... в общем ментовская форма в ментовках, а военная на военных объектах
    /// </summary>
    public class Flags
    {
        /// <summary>
        /// Список имен назначений
        /// </summary>
        [XmlElement("usage")]
        public List<DB.Types.Usage> Names { get; set; } = new();
    }
}
