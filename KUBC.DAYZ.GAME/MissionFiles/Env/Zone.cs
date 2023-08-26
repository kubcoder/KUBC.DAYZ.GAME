using System.Xml.Serialization;

namespace KUBC.DAYZ.GAME.MissionFiles.Env
{
    /// <summary>
    /// Зона спавна AI
    /// </summary>
    public class Zone
    {
        /// <summary>
        /// Как используется территория для спавна
        /// </summary>
        [XmlAttribute("name")]
        public string UsageAI { get; set; } = string.Empty;
        /// <summary>
        /// Минимальное количество статических AI в зоне
        /// </summary>
        [XmlAttribute("smin")]
        public int StaticMin { get; set; } = 0;
        /// <summary>
        /// Максимальное количество статических AI в зоне
        /// </summary>
        [XmlAttribute("smax")]
        public int StaticMax { get; set; } = 0;

        /// <summary>
        /// Минимальное количество динамических AI в зоне
        /// </summary>
        [XmlAttribute("dmin")]
        public int DynamicMin { get; set; } = 0;

        /// <summary>
        /// Максимальное количество динамических AI в зоне
        /// </summary>
        [XmlAttribute("dmax")]
        public int DynamicMax { get; set; } = 0;

        /// <summary>
        /// Координата центра зоны с запада на восток
        /// </summary>
        [XmlAttribute("x")]
        public float X { get; set; } = 0;

        /// <summary>
        /// Координата центра зоны с севера на юг
        /// </summary>
        [XmlAttribute("z")]
        public float Z { get; set; } = 0;

        /// <summary>
        /// Радиус зоны
        /// </summary>
        [XmlAttribute("r")]
        public float Radius { get; set; } = 0;

        /// <summary>
        /// ХЗ что это
        /// </summary>
        [XmlAttribute("d")]
        public string? DAttr { get; set; }


    }
}
