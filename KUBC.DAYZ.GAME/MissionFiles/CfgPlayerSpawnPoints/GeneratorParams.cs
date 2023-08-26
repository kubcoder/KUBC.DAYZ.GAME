using System.Xml.Serialization;

namespace KUBC.DAYZ.GAME.MissionFiles.CfgPlayerSpawnPoints
{
    /// <summary>
    /// Настройки генератора точек
    /// </summary>
    /// <remarks>
    /// Spawn points are generated around positions given inside <see cref="GeneratorPosbubbles"/> element.
    /// Around every position is created a rectangle with given dimensions.
    /// This rectangle is then sampled for spawn point candidates in form of a grid.
    /// </remarks>
    public class GeneratorParams
    {
        /// <summary>
        /// Плотность сетки
        /// </summary>
        [XmlElement("grid_density")]
        public int Density { get; set; } = 8;
        /// <summary>
        /// Ширина ячейки
        /// </summary>
        [XmlElement("grid_width")]
        public float Width { get; set; } = 40;
        /// <summary>
        /// Высота ячейки
        /// </summary>
        [XmlElement("grid_height")]
        public float Height { get; set; } = 40;

        /// <summary>
        /// Минимальная дистанция до строений
        /// </summary>
        [XmlElement("min_dist_static")]
        public float MinStatic { get; set; } = 0.5f;
        /// <summary>
        /// Максимальная дистанция до стат. объектов
        /// </summary>
        [XmlElement("max_dist_static")]
        public float MaxStatic { get; set; } = 2.0f;
        /// <summary>
        /// Минимальное ограниечение склона 
        /// </summary>
        [XmlElement("min_steepness")]
        public float MinSteepness { get; set; } = -45;

        /// <summary>
        /// Максимальное ограниечение склона 
        /// </summary>
        [XmlElement("max_steepness")]
        public float MaxSteepness { get; set; } = -45;
    }
}
