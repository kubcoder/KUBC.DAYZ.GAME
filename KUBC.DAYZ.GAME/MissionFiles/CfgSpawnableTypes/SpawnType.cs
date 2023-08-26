using System.Xml.Serialization;

namespace KUBC.DAYZ.GAME.MissionFiles.CfgSpawnableTypes
{
    /// <summary>
    /// Описание типа спавна
    /// </summary>
    public class SpawnType
    {
        /// <summary>
        /// Имя типа для спавна центральной экономикой
        /// </summary>
        [XmlAttribute("name")]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Признак что итем есть хранилище
        /// </summary>
        [XmlElement("hoarder")]
        public Hoarder? Hoarder { get; set; }

        /// <summary>
        /// Дамаж итему при спавне
        /// </summary>
        [XmlElement("damage")]
        public Damage? Damage { get; set; }
        /// <summary>
        /// ТЭГ элемента спавна
        /// </summary>
        [XmlElement("tag")]
        public DB.Types.Tag? Tag { get; set; }

        /// <summary>
        /// ИТемы в инвентаре
        /// </summary>
        [XmlElement("cargo")]
        public List<CfgRandomPresets.ItemCollection>? Cargo { get; set; }

        /// <summary>
        /// Итемы в Слотах
        /// </summary>
        [XmlElement("attachments")]
        public List<CfgRandomPresets.ItemCollection>? Attachments { get; set; }


    }
}
