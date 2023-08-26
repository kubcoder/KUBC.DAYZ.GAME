using System.Xml.Serialization;

namespace KUBC.DAYZ.GAME.MissionFiles.CfgEventGroups
{
    /// <summary>
    /// Элемент в группе спавна
    /// </summary>
    public class Child
    {
        /// <summary>
        /// Имя класса для спавна
        /// </summary>
        [XmlAttribute("type")]
        public string Type { get; set; } = string.Empty;
        /// <summary>
        /// ХЗ, не совсем ясно, всегда в файле нулики
        /// </summary>
        [XmlAttribute("deloot")]
        public int DeLoot { get; set; } = 0;
        /// <summary>
        /// Максимальное кол-во лута в объекте
        /// </summary>
        [XmlAttribute("lootmax")]
        public int LootMax { get; set; } = 0;
        /// <summary>
        /// Минимальное кол-во лута в объекте
        /// </summary>
        [XmlAttribute("lootmin")]
        public int LootMin { get; set; } = 0;
        /// <summary>
        /// Координата Х, относительно родительского объекта
        /// </summary>
        [XmlAttribute("x")]
        public float X { get; set; } = 0;
        /// <summary>
        /// Координата Z, относительно родительского объекта
        /// </summary>
        [XmlAttribute("z")]
        public float Z { get; set; } = 0;
        /// <summary>
        /// Поворот объекта на север относительно род. объекта
        /// </summary>
        [XmlAttribute("a")]
        public float A { get; set; } = 0;
        /// <summary>
        /// Координата Y, относительно родительского объекта
        /// </summary>
        [XmlAttribute("y")]
        public float Y { get; set; } = 0;



    }
}
