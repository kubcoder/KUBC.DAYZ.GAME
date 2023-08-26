using System.Xml.Serialization;

namespace KUBC.DAYZ.GAME.MissionFiles.CfgLimitsDefinitionUser
{
    /// <summary>
    /// Набор флагов определяемых юзером
    /// </summary>
    public class UserFlagCollection
    {
        /// <summary>
        /// Непосредственно сам набор флагов
        /// </summary>
        [XmlElement("user")]
        public List<UserFlag> UserFlags = new();
    }
}
