using System.Xml.Serialization;

namespace KUBC.DAYZ.GAME.MissionFiles.CfgEventSpawns
{
    /// <summary>
    /// Описание координаты события
    /// </summary>
    [XmlType("pos")]
    public class Pos
    {
        private System.Globalization.CultureInfo culture
        {
            get
            {
                return System.Globalization.CultureInfo.CreateSpecificCulture("en-GB");
            }
        }


        /// <summary>
        /// Координата X события
        /// </summary>
        [XmlAttribute("x")]
        public double X { get; set; } = 0;
        /// <summary>
        /// Координата Z события
        /// </summary>
        [XmlAttribute("z")]
        public double Z { get; set; } = 0;

        /// <summary>
        /// Координата Z события
        /// </summary>
        [XmlAttribute("y")]
        public string? YATTR
        {
            get
            {
                if (Y.HasValue)
                {
                    return Y.Value.ToString(culture);
                }
                return null;
            }
            set
            {
                Y = null;
                if (value != null)
                {
                    var style = System.Globalization.NumberStyles.Number;
                    if (double.TryParse(value, style, culture, out var f))
                    {
                        Y = f;
                    }
                }
            }
        }

        /// <summary>
        /// Угол разворота объектов
        /// </summary>
        [XmlAttribute("a")]
        public string? AATTR
        {
            get
            {
                if (A.HasValue)
                {
                    return A.Value.ToString(culture);
                }
                return null;
            }
            set
            {
                A = null;
                if (value != null)
                {
                    var style = System.Globalization.NumberStyles.Number;
                    if (double.TryParse(value, style, culture, out var f))
                    {
                        A = f;
                    }
                }
            }
        }

        /// <summary>
        /// Координата Х
        /// </summary>
        [XmlIgnore]
        public double? Y;

        /// <summary>
        /// Угол разворота события
        /// </summary>
        [XmlIgnore]
        public double? A;

        /// <summary>
        /// Имя группы события
        /// </summary>
        [XmlAttribute("group")]
        public string? Group;

        /// <summary>
        /// Получить вектор из координаты события
        /// </summary>
        /// <returns>Вектор без учета высоты</returns>
        public Vector ToVector()
        {
            Vector r = new Vector()
            {
                X = X,
                Z = Z,
            };
            if (Y.HasValue) { r.Y = Y.Value; }
            return r;
        }
    }
}
