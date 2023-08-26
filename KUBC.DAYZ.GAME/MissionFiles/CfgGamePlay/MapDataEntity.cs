using System.Text.Json.Serialization;

namespace KUBC.DAYZ.GAME.MissionFiles.CfgGamePlay
{
    /// <summary>
    /// настройки навигации
    /// </summary>
    public class MapDataEntity
    {
        /// <summary>
        /// Player can open a map (and just the map) using dedicated input ("M") even without map in player's inventory.
        /// Игрок может открыть карту (и только карту) с помощью специального ввода («M») даже без карты в инвентаре игрока.
        /// </summary>
        [JsonPropertyName("ignoreMapOwnership")]
        public bool IgnoreMapOwnership { get; set; } = false;
        /// <summary>
        /// Compass and/or GPS receiver are not needed for displaying helpers on the 2D map.
        /// Компас и/или GPS-приемник не нужны для отображения помощников на 2D-карте.
        /// </summary>
        [JsonPropertyName("ignoreNavItemsOwnership")]
        public bool IgnoreNavItemsOwnership { get; set; } = false;

        /// <summary>
        /// Shows the red maker on the map, on player's position. It also display direction on the marker.
        /// Отображать положение и направление взгляда игрока на карте
        /// </summary>
        [JsonPropertyName("displayPlayerPosition")]
        public bool DisplayPlayerPosition { get; set; } = false;

        /// <summary>
        /// Hide GPS and Compass UI from the map legend completely (even when player has those items in inventory).
        /// Полностью скрыть GPS и пользовательский интерфейс компаса из легенды карты (даже если у игрока есть эти предметы в инвентаре).
        /// </summary>
        [JsonPropertyName("displayNavInfo")]
        public bool DisplayNavInfo { get; set; } = true;
    }
}
