using System.Xml.Serialization;

namespace KUBC.DAYZ.GAME.MissionFiles.MapGroupProto
{
    /// <summary>
    /// Список точек на статическом объекте
    /// в которых может спавниться лут.
    /// </summary>
    /// <remarks>
    /// В общем колеса, капоты и прочая святотень
    /// </remarks>
    public class Dispatch
    {
        /// <summary>
        /// Точки на которых спавнится лут
        /// </summary>
        [XmlElement("proxy")]
        public List<Proxy> ProxyPoints = new();
    }
}
