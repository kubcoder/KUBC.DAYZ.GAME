using System.Xml.Serialization;

namespace KUBC.DAYZ.GAME.MissionFiles.CfgEnvironment
{
    /// <summary>
    /// Настройка путей файла
    /// </summary>
    public class FilePath
    {
        /// <summary>
        /// Путь к файлу в папке миссии
        /// </summary>
        [XmlAttribute("path")]
        public string Path { get; set; } = string.Empty;
    }
}
