using System.Text.Json.Serialization;

namespace KUBC.DAYZ.GAME.MissionFiles.CfgGamePlay
{
    /// <summary>
    /// Настройки утопления игрока
    /// </summary>
    public class DrowningDataEntity
    {
        /// <summary>
        /// Stamina depleted per second while drowning
        /// Выносливость истощается в секунду во время утопления
        /// </summary>
        [JsonPropertyName("staminaDepletionSpeed")]
        public float StaminaDepletionSpeed { get; set; } = 10;
        /// <summary>
        /// Health depleted per second while drowning
        /// Здоровье истощается в секунду при утоплении
        /// </summary>
        [JsonPropertyName("healthDepletionSpeed")]
        public float HealthDepletionSpeed { get; set; } = 10;
        /// <summary>
        /// Shock depleted per second while drowning
        /// Шок истощается в секунду во время утопления
        /// </summary>
        [JsonPropertyName("shockDepletionSpeed")]
        public float ShockDepletionSpeed { get; set; } = 10;
    }
}
