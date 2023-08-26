using System.Text.Json.Serialization;

namespace KUBC.DAYZ.GAME.MissionFiles.CfgGamePlay
{
    /// <summary>
    /// Настройки игрока
    /// </summary>
    public class PlayerDataEntity
    {
        /// <summary>
        /// Имя свойства отключающего слабую подсветку вокруг игрока
        /// </summary>
        public const string pn_DisablePersonalLight = "disablePersonalLight";

        /// <summary>
        /// Отключить подсветку игрока
        /// </summary>
        [JsonPropertyName(pn_DisablePersonalLight)]
        public bool DisablePersonalLight { get; set; } = false;
        /// <summary>
        /// Настройки стамины игрока
        /// </summary>
        public StaminaDataEntity StaminaData { get; set; } = new();
        /// <summary>
        /// Настройки шока игрока
        /// </summary>
        public ShockHandlingDataEntity ShockHandlingData { get; set; } = new();

        /// <summary>
        /// Настройки инерции движения игрока
        /// </summary>
        public MovementDataEntity MovementData { get; set; } = new();

        /// <summary>
        /// Настройки утопления игрока
        /// </summary>
        public DrowningDataEntity DrowningData { get; set; } = new();
    }
}
