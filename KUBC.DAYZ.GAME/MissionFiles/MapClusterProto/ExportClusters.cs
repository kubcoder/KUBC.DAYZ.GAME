using System.Xml.Serialization;

namespace KUBC.DAYZ.GAME.MissionFiles.MapClusterProto
{
    /// <summary>
    /// Набор экспортируемых кластеров
    /// </summary>
    public class ExportClusters
    {
        /// <summary>
        /// Экспортируемые кластеры
        /// </summary>
        [XmlElement("export")]
        public List<Export> Exports = new();
    }
}
