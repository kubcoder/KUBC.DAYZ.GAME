using System.Text.Json.Serialization;

namespace KUBC.DAYZ.GAME.MissionFiles.CfgGamePlay
{
    /// <summary>
    /// Управление инерцией игрока
    /// </summary>
    public class MovementDataEntity
    {
        /// <summary>
        /// Time to blend strafing (diagonal movement) while jogging (min possible value 0.01)
        /// Время смешивания стрейфинга (движения по диагонали) при беге трусцой (минимальное возможное значение 0,01)
        /// </summary>
        [JsonPropertyName("timeToStrafeJog")]
        public float TimeToStrafeJog { get; set; } = 0.1f;
        /// <summary>
        /// Rotation speed of the character while jogging (min possible value 0.01)
        /// Скорость вращения персонажа при беге (минимально возможное значение 0,01)
        /// </summary>
        [JsonPropertyName("rotationSpeedJog")]
        public float RotationSpeedJog { get; set; } = 0.3f;
        /// <summary>
        /// Time to reach sprint from jog (min possible value 0.01)
        /// Время до спринта с пробежки (минимально возможное значение 0,01)
        /// </summary>
        [JsonPropertyName("timeToSprint")]
        public float TimeToSprint { get; set; } = 0.45f;
        /// <summary>
        /// Time to blend strafing (diagonal movement) while sprinting (min possible value 0.01)
        /// Время смешивания стрейфинга (движения по диагонали) во время спринта (минимально возможное значение 0,01)
        /// </summary>
        [JsonPropertyName("timeToStrafeSprint")]
        public float TimeToStrafeSprint { get; set; } = 0.3f;
        /// <summary>
        /// Rotation speed of the character while sprinting (min possible value 0.01)
        /// Скорость вращения персонажа при спринте (минимально возможное значение 0,01)
        /// </summary>
        [JsonPropertyName("rotationSpeedSprint")]
        public float RotationSpeedSprint { get; set; } = 0.15f;
        /// <summary>
        /// When enabled allows stamina value influence player's inertia
        /// При включении позволяет значению выносливости влиять на инерцию игрока.
        /// </summary>
        [JsonPropertyName("allowStaminaAffectInertia")]
        public bool AllowStaminaAffectInertia { get; set; } = true;
    }
}
