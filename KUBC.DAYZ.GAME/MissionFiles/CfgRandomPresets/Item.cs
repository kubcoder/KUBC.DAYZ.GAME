using System.Globalization;
using System.Xml.Serialization;

namespace KUBC.DAYZ.GAME.MissionFiles.CfgRandomPresets
{
    /// <summary>
    /// Итем для спавна
    /// </summary>
    public class Item
    {
        /// <summary>
        /// Имя класса итема
        /// </summary>
        [XmlAttribute("name")]
        public string? Name { get; set; } = string.Empty;
        /// <summary>
        /// Шанс выпадения итема
        /// </summary>
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
        /// Шанс выпадения итема
        /// </summary>
        [XmlIgnore]
        public float? Chance;


    }
}
