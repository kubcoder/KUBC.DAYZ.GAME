using System.Xml.Serialization;

namespace KUBC.DAYZ.GAME.MissionFiles.MapGroupProto
{
    /// <summary>
    /// Набор значений по умолчанию
    /// </summary>
    public class Defaults
    {
        /// <summary>
        /// Непосредственно список значений
        /// </summary>
        [XmlElement("default")]
        public List<Default> Values { get; set; } = new();
    }
}
