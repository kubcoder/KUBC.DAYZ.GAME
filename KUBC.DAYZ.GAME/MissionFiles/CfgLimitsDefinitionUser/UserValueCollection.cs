using System.Xml.Serialization;

namespace KUBC.DAYZ.GAME.MissionFiles.CfgLimitsDefinitionUser
{
    /// <summary>
    /// Пользовательские набор редкости итема
    /// </summary>
    public class UserValueCollection
    {
        /// <summary>
        /// Непосредственно сам набор объемов
        /// </summary>
        [XmlElement("user")]
        public List<UserValue> UserValues = new();
    }
}
