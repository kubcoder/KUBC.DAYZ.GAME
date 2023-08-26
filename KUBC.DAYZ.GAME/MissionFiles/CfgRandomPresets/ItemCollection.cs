using System.Globalization;
using System.Xml.Serialization;

namespace KUBC.DAYZ.GAME.MissionFiles.CfgRandomPresets
{
    /// <summary>
    /// Коллекция итемов для спавна
    /// </summary>
    public class ItemCollection
    {

        /// <summary>
        ///Шанс выпадения коллекции
        /// </summary>
        /// <remarks>
        /// Да это текстовый костыль что бы несуществующие поля не крашили загрузку данных
        /// </remarks>
        [XmlAttribute("chance")]
        public string? ChanceAttr
        {
            get
            {
                if (Chance.HasValue)
                {
                    return Chance.Value.ToString(CultureInfo.InvariantCulture);
                }
                return null;
            }
            set
            {
                if (float.TryParse(value, CultureInfo.InvariantCulture, out var chance))
                {
                    Chance = chance;
                }
                else
                {
                    Chance = null;
                }

            }
        }
        /// <summary>
        /// Шанс выпадения коллекции
        /// </summary>
        [XmlIgnore]
        public float? Chance;

        /// <summary>
        /// Имя колекции
        /// </summary>
        [XmlAttribute("name")]
        public string? Name { get; set; }
        /// <summary>
        /// Итемы колекции для спавна
        /// </summary>
        [XmlElement("item")]
        public List<Item>? Items { get; set; }

        /// <summary>
        /// Ссылка на имеющиеся настройки
        /// </summary>
        [XmlAttribute("preset")]
        public string? Preset { get; set; }

    }
}
