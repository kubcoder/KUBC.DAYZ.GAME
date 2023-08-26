using System.Text.Json.Serialization;

namespace KUBC.DAYZ.GAME.MissionFiles.CfgGamePlay
{
    /// <summary>
    /// Настройки конструкций
    /// </summary>
    public class ConstructionDataEntity
    {
        /// <summary>
        /// Allows construction when clipping with the roof
        /// Позволяет строительство при обрезании с крышей
        /// </summary>
        [JsonPropertyName("disablePerformRoofCheck")]
        public bool DisablePerformRoofCheck { get; set; } = false;

        /// <summary>
        /// Allows construction when colliding with objects in the world
        /// Позволяет строительство при столкновении с объектами в мире
        /// </summary>
        [JsonPropertyName("disableIsCollidingCheck")]
        public bool DisableIsCollidingCheck { get; set; } = false;

        /// <summary>
        /// Prevents construction when player gets bellow specified range
        /// Предотвращает строительство, когда игрок попадает ниже указанного диапазона
        /// </summary>
        [JsonPropertyName("disableDistanceCheck")]
        public bool DisableDistanceCheck { get; set; } = false;
    }
}
