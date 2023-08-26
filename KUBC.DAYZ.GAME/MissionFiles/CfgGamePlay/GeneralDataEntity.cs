using System.Text.Json.Serialization;

namespace KUBC.DAYZ.GAME.MissionFiles.CfgGamePlay
{
    /// <summary>
    /// Секция основных данных
    /// </summary>
    public class GeneralDataEntity
    {
        /// <summary>
        /// Имя свойства отключающего разрушение построек игроков
        /// </summary>
        public const string pn_DisableBaseDamage = "disableBaseDamage";
        /// <summary>
        /// Отключить разрушение построек
        /// </summary>
        [JsonPropertyName(pn_DisableBaseDamage)]
        public bool DisableBaseDamage { get; set; } = false;

        /// <summary>
        /// Имя свойства отключающего разрушение контейнеров
        /// </summary>
        public const string pn_DisableContainerDamage = "disableContainerDamage";
        /// <summary>
        /// Отключить разрушение контейнеров
        /// </summary>
        [JsonPropertyName(pn_DisableContainerDamage)]
        public bool DisableContainerDamage { get; set; } = false;

        /// <summary>
        /// Имя свойства отключающего диалог респавна игрока
        /// </summary>
        public const string pn_DisableRespawnDialog = "disableRespawnDialog";
        /// <summary>
        /// Отключить диалог респавна игрока
        /// </summary>
        [JsonPropertyName(pn_DisableRespawnDialog)]
        public bool DisableRespawnDialog { get; set; } = false;

    }
}
