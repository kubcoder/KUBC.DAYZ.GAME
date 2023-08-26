using System.Text.Json.Serialization;

namespace KUBC.DAYZ.GAME.MissionFiles.CfgGamePlay
{
    /// <summary>
    /// Настройки стамины игрока
    /// </summary>
    public class StaminaDataEntity
    {
        /// <summary>
        /// Modifies the rate at which the stamina is being consumed during erected sprint
        /// Изменяет скорость, с которой расходуется выносливость во время спринта.
        /// </summary>
        [JsonPropertyName("sprintStaminaModifierErc")]
        public float SprintStaminaModifierErc { get; set; } = 1;

        /// <summary>
        /// Modifies the rate at which the stamina is being consumed during crouched sprint
        /// Изменяет скорость, с которой расходуется выносливость во время приседания.
        /// </summary>
        [JsonPropertyName("sprintStaminaModifierCro")]
        public float SprintStaminaModifierCro { get; set; } = 1;

        /// <summary>
        /// This amount of stamina points (divided by 1000) will not count towards stamina weight deduction
        /// Это количество очков выносливости (деленное на 1000) не будет учитываться при вычете веса выносливости.
        /// </summary>
        [JsonPropertyName("staminaWeightLimitThreshold")]
        public float StaminaWeightLimitThreshold { get; set; } = 6000;

        /// <summary>
        /// Maximum stamina (setting to 0 may produce unexpected results)
        /// Максимальная выносливость (установка на 0 может привести к неожиданным результатам)
        /// </summary>
        [JsonPropertyName("staminaMax")]
        public float StaminaMax { get; set; } = 100;

        /// <summary>
        /// Multiplier used when calculating stamina points deducted from max stamina given the player load
        /// Множитель, используемый при расчете очков выносливости, вычитаемых из максимальной выносливости с учетом загрузки игрока.
        /// </summary>
        [JsonPropertyName("staminaKgToStaminaPercentPenalty")]
        public float StaminaKgToStaminaPercentPenalty { get; set; } = 1.75f;

        /// <summary>
        /// Minimum size of stamina cap (setting to 0 may produce unexpected results)
        /// Минимальный размер предела выносливости (установка на 0 может привести к неожиданным результатам)
        /// </summary>
        [JsonPropertyName("staminaMinCap")]
        public float StaminaMinCap { get; set; } = 5;

        /// <summary>
        /// Modifies the rate at which the stamina is being consumed during fast swimming
        /// Изменяет скорость, с которой выносливость потребляется во время быстрого плавания
        /// </summary>
        [JsonPropertyName("sprintSwimmingStaminaModifier")]
        public float SprintSwimmingStaminaModifier { get; set; } = 1;

        /// <summary>
        /// Modifies the rate at which the stamina is being consumed during fast ladder climbing
        /// Изменяет скорость, с которой расходуется выносливость во время быстрого подъема по лестнице.
        /// </summary>
        [JsonPropertyName("sprintLadderStaminaModifier")]
        public float SprintLadderStaminaModifier { get; set; } = 1;
        /// <summary>
        /// Modifies how much stamina is being consumed when performing heavy melee attacks and evasion
        /// Изменяет количество выносливости, расходуемой при тяжелых атаках ближнего боя и уклонении.
        /// </summary>
        [JsonPropertyName("meleeStaminaModifier")]
        public float MeleeStaminaModifier { get; set; } = 1;
        /// <summary>
        /// Modifies how much stamina is being consumed when performing jumping, climbing and vaulting
        /// Изменяет количество выносливости, расходуемой при выполнении прыжков, лазания и прыжков с трамплина.
        /// </summary>
        [JsonPropertyName("obstacleTraversalStaminaModifier")]
        public float ObstacleTraversalStaminaModifier { get; set; } = 1;
        /// <summary>
        /// Modifies the rate at which the stamina is being consumed when holding breath
        /// Изменяет скорость, с которой расходуется выносливость при задержке дыхания.
        /// </summary>
        [JsonPropertyName("holdBreathStaminaModifier")]
        public float HoldBreathStaminaModifier { get; set; } = 1;

    }
}
