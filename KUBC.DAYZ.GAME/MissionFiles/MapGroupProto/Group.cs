using System.Xml.Serialization;

namespace KUBC.DAYZ.GAME.MissionFiles.MapGroupProto
{
    /// <summary>
    /// Группа спавна
    /// </summary>
    public class Group
    {
        /// <summary>
        /// Название группы спавна, используется в <see cref="MapGroupPos"/>
        /// </summary>
        [XmlAttribute("name")]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Максимальное кол-во лута
        /// </summary>
        [XmlIgnore]
        public int? LootMax { get; set; }
        /// <summary>
        /// Атрибут максимального числа лута в группе 
        /// </summary>
        [XmlAttribute("lootmax")]
        public string? LootMaxAttr
        {
            get
            {
                if (LootMax.HasValue)
                {
                    return LootMax.Value.ToString();
                }
                return null;
            }
            set
            {
                if (int.TryParse(value, out int lootmax))
                {
                    LootMax = lootmax;
                }
                else
                {
                    LootMax = null;
                }
            }
        }
        /// <summary>
        /// Тематическое использование группы спавна
        /// </summary>
        [XmlElement("usage")]
        public List<DB.Types.Usage> Usages { get; set; } = new();

        /// <summary>
        /// Признак группы по редкости спавна лута
        /// </summary>
        [XmlElement("value")]
        public List<DB.Types.Value> Values { get; set; } = new();
        /// <summary>
        /// Контейнеры для размещения лута
        /// </summary>
        [XmlElement("container")]
        public List<Сontainer> Containers { get; set; } = new();
        /// <summary>
        /// Точки приатачивания лута
        /// </summary>
        [XmlElement("dispatch")]
        public Dispatch? Dispatch { get; set; }
    }
}
