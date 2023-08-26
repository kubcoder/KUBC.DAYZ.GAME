using System.Text.Json.Serialization;

namespace KUBC.DAYZ.GAME.MissionFiles.CfgGamePlay
{
    /// <summary>
    /// Настройки поведения голограмы размещения
    /// </summary>
    public class HologramDataEntity
    {
        /// <summary>
        /// Allows placement when the hologram is colliding with objects in the world
        /// Позволяет размещение, когда голограмма сталкивается с объектами в мире
        /// </summary>
        [JsonPropertyName("disableIsCollidingBBoxCheck")]
        public bool DisableIsCollidingBBoxCheck { get; set; } = false;

        /// <summary>
        /// Allows placement when the hologram is colliding with player
        /// Позволяет размещение, когда голограмма сталкивается с игроком
        /// </summary>
        [JsonPropertyName("disableIsCollidingPlayerCheck")]
        public bool DisableIsCollidingPlayerCheck { get; set; } = false;

        /// <summary>
        /// Allows placement where placing would cause clipping with the roof
        /// Позволяет размещение там, где размещение может привести к отсечению крыши
        /// </summary>
        [JsonPropertyName("disableIsClippingRoofCheck")]
        public bool DisableIsClippingRoofCheck { get; set; } = false;

        /// <summary>
        /// Allows placement on dynamic objects and other otherwise incompatible base
        /// Позволяет размещение на динамических объектах и других несовместимых основаниях
        /// </summary>
        [JsonPropertyName("disableIsBaseViableCheck")]
        public bool DisableIsBaseViableCheck { get; set; } = false;

        /// <summary>
        /// Allows placement of garden plots despite incompatible surface type
        /// Позволяет размещать садовые участки, несмотря на несовместимый тип поверхности
        /// </summary>
        [JsonPropertyName("disableIsCollidingGPlotCheck")]
        public bool DisableIsCollidingGPlotCheck { get; set; } = false;

        /// <summary>
        /// Allows placement despite exceeding roll/pitch/yaw limits
        /// Позволяет размещение, несмотря на превышение ограничений по крену/тангажу/рысканию
        /// </summary>
        [JsonPropertyName("disableIsCollidingAngleCheck")]
        public bool DisableIsCollidingAngleCheck { get; set; } = false;

        /// <summary>
        /// Allows placement event when not permitted by rudimentary checks
        /// Разрешает событие размещения, когда это не разрешено элементарными проверками
        /// </summary>
        [JsonPropertyName("disableIsPlacementPermittedCheck")]
        public bool DisableIsPlacementPermittedCheck { get; set; } = false;

        /// <summary>
        /// Allows placement with limited height space
        /// Позволяет размещение с ограниченным пространством по высоте
        /// </summary>
        [JsonPropertyName("disableHeightPlacementCheck")]
        public bool DisableHeightPlacementCheck { get; set; } = false;

        /// <summary>
        /// Allows placement under water
        /// Позволяет размещение под водой
        /// </summary>
        [JsonPropertyName("disableIsUnderwaterCheck")]
        public bool DisableIsUnderwaterCheck { get; set; } = false;

        /// <summary>
        /// Allows placement when clipping with terrain
        /// Позволяет размещение при отсечении с ландшафтом
        /// </summary>
        [JsonPropertyName("disableIsInTerrainCheck")]
        public bool DisableIsInTerrainCheck { get; set; } = false;

    }
}
