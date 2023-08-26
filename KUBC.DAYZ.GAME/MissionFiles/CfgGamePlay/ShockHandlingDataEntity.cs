using System.Text.Json.Serialization;

namespace KUBC.DAYZ.GAME.MissionFiles.CfgGamePlay
{
    /// <summary>
    /// Настройка шокового состояния
    /// </summary>
    public class ShockHandlingDataEntity
    {
        /// <summary>
        /// Shock value recovery while the player is conscious (per second)
        /// Восстановление значения шока, пока игрок в сознании (в секунду)
        /// </summary>
        [JsonPropertyName("shockRefillSpeedConscious")]
        public float ShockRefillSpeedConscious { get; set; } = 5;
        /// <summary>
        /// Shock value recovery while the player is unconscious (per second)
        /// Восстановление значения шока, пока игрок без сознания (в секунду)
        /// </summary>
        [JsonPropertyName("shockRefillSpeedUnconscious")]
        public float ShockRefillSpeedUnconscious { get; set; } = 1;
        /// <summary>
        /// Allow/disallow modifier of Shock value recovery based on ammo type settings (typically faster waking-up from uncon after getting shot)
        /// Разрешить/запретить модификатор восстановления значения шока в зависимости от настроек типа боеприпасов (обычно более быстрое пробуждение после выстрела)
        /// </summary>
        [JsonPropertyName("allowRefillSpeedModifier")]
        public bool AllowRefillSpeedModifier { get; set; } = true;
    }
}
