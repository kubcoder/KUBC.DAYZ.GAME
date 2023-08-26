using System.Text.Json.Serialization;

namespace KUBC.DAYZ.GAME.MissionFiles.CfgGamePlay
{
    /// <summary>
    /// Настройки интерфейса игрока
    /// </summary>
    public class UIDataEntity
    {
        /// <summary>
        /// Enables use of the 3D map only (disables the default 2d map overlay)
        /// Включает использование только 3D-карты (отключает наложение 2D-карты по умолчанию)
        /// </summary>
        [JsonPropertyName("use3DMap")]
        public bool Use3DMap { get; set; } = false;

        /// <summary>
        /// Настройки индикатора повреждений
        /// </summary>
        public HitIndicationDataEntity HitIndicationData { get; set; } = new();
    }
}
