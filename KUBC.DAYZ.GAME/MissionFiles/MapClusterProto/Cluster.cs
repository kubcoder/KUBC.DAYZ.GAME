using System.Xml.Serialization;

namespace KUBC.DAYZ.GAME.MissionFiles.MapClusterProto
{
    /// <summary>
    /// Описание кластера спавна
    /// </summary>
    public class Cluster
    {
        /// <summary>
        /// Имя кластера
        /// </summary>
        /// <remarks>
        /// Оно должно быть объявлено в секции <see cref="File.Clusters"/>
        /// На сколько я понял там импортируется геометрия, а тут добавляется некое
        /// описание
        /// </remarks>
        [XmlAttribute("name")]
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// Максимальное кол-во лута в кластере
        /// </summary>
        [XmlAttribute("lootmax")]
        public int LootMax { get; set; } = 2;
        /// <summary>
        /// Максимальное число экземпляров которые могут одновременно
        /// существовать на сервере
        /// </summary>
        [XmlAttribute("maxinstances")]
        public int MaxInstances { get; set; } = 150;
        /// <summary>
        /// Что именно и в каком кол-ве нужно 
        /// спавнить в данном кластере. 
        /// Данное имя определено в файле events.xml <see cref="DB.Events.File"/>
        /// </summary>
        [XmlElement("de")]
        public DefineEvents DE { get; set; } = new();
        /// <summary>
        /// Где именно спавнить лут внутри кластера
        /// </summary>
        [XmlElement("container")]
        public Container Container { get; set; } = new();


    }
}
