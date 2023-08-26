using System.Xml.Serialization;

namespace KUBC.DAYZ.GAME.MissionFiles.MapClusterProto
{
    /// <summary>
    /// Экспортируемые элементы. Т.е. имя задаем и какую модельку p3d использует данное имя
    /// </summary>
    public class Export
    {
        /// <summary>
        /// Имя кластера
        /// </summary>
        [XmlAttribute("name")]
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// Фигура (моделька) размещения. Т.е. грубо говоря геометрия кластера
        /// </summary>
        [XmlAttribute("shape")]
        public string Shape { get; set; } = string.Empty;
    }
}
