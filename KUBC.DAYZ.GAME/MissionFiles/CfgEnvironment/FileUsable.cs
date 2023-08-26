using System.Xml.Serialization;

namespace KUBC.DAYZ.GAME.MissionFiles.CfgEnvironment
{
    /// <summary>
    /// Описание используемого файла
    /// </summary>
    [Serializable]
    [XmlType("file")]
    public class FileUsable
    {
        /// <summary>
        /// Имя используемого файла
        /// </summary>
        [XmlAttribute("usable")]
        public string Usable { get; set; } = string.Empty;
    }
}
