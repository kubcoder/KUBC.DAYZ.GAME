using System.Xml.Serialization;

namespace KUBC.DAYZ.GAME.MissionFiles.CfgEnvironment
{
    /// <summary>
    /// Настройки территорий
    /// </summary>
    [Serializable]
    [XmlType("territories")]
    public class Territories
    {
        /// <summary>
        /// Пути к файлам
        /// </summary>
        [XmlElement("file")]
        public List<FilePath> Paths = new();

        /// <summary>
        /// Элементы спавна
        /// </summary>
        [XmlElement("territory")]
        public List<Territory> Items = new();
    }
}
