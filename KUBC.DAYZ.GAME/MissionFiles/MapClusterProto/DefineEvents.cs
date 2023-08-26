using System.Xml.Serialization;

namespace KUBC.DAYZ.GAME.MissionFiles.MapClusterProto
{
    /// <summary>
    /// Имя используемое в файле events.xml <see cref="DB.Events"/>
    /// </summary>
    public class DefineEvents
    {
        /// <summary>
        /// Имя event. Т.е. когда срабатывает данный кластер
        /// в файле event по этому имени находится что 
        /// нужно в этом кластере разместить.
        /// </summary>
        [XmlAttribute("name")]
        public string Name { get; set; } = string.Empty;
    }
}
