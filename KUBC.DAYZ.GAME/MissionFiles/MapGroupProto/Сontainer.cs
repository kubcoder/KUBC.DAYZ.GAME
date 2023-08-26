using System.Xml.Serialization;

namespace KUBC.DAYZ.GAME.MissionFiles.MapGroupProto
{
    /// <summary>
    /// Описание контейнера спавна
    /// </summary>
    public class Сontainer
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
        /// Категории спавна
        /// </summary>
        [XmlElement("category")]
        public List<DB.Types.Category> Categories { get; set; } = new();

        /// <summary>
        /// Тэги для спавна
        /// </summary>
        [XmlElement("tag")]
        public List<DB.Types.Tag> Tags { get; set; } = new();

        /// <summary>
        /// Точки относительно позиции группы
        /// </summary>
        [XmlElement("point")]
        public List<MapClusterProto.Point> Points { get; set; } = new();
    }
}
