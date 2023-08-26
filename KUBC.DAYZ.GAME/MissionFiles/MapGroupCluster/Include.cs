using System.Xml.Serialization;

namespace KUBC.DAYZ.GAME.MissionFiles.MapGroupCluster
{
    /// <summary>
    /// Поле включения дополнительных файлов
    /// </summary>
    public class Include
    {
        /// <summary>
        /// Имя включаемого файла
        /// </summary>
        [XmlAttribute("file")]
        public string File { get; set; } = string.Empty;
    }
}
