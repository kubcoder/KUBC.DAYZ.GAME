using System.Xml.Serialization;

namespace KUBC.DAYZ.GAME.MissionFiles.MapClusterProto
{
    /// <summary>
    /// Точка спавна лута
    /// </summary>
    public class Point
    {
        /// <summary>
        /// Координаты точки для размещения лута
        /// внутри кластера.
        /// </summary>
        /// <remarks>
        /// Да это текстовая переменная. и я знаю что 
        /// нужно прикрутить преобразование в вектор.
        /// </remarks>
        [XmlAttribute("pos")]
        public string PositionAsString { get; set; } = string.Empty;
        /// <summary>
        /// На сколько далеко от точки может быть размещен итем
        /// </summary>
        /// <remarks>
        /// Т.е. координата <see cref="PositionAsString"/> обозначает центр
        /// а лут размещается внутри круга, радиус которого задается этой переменной
        /// </remarks>
        [XmlAttribute("range")]
        public float Range { get; set; } = 0;
        /// <summary>
        /// Высота размещения.
        /// </summary>
        /// <remarks>
        /// Судя по всему это сдвиг в верх от нуля модельки.
        /// Переменная нихуя не понятна так, как вроде в координатах 
        /// точки задается высота.
        /// </remarks>
        [XmlAttribute("height")]
        public float Height { get; set; } = 0;
        /// <summary>
        /// Флаги для центральной экономики.
        /// </summary>
        /// <remarks>
        /// Т.е. какие то настройки, что именно означают биты
        /// неизвестно.
        /// </remarks>
        [XmlAttribute("flags")]
        public string? Flags { get; set; }
    }
}
