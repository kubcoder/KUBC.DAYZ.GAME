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

        /// <summary>
        /// Имя свойства отключающего диалог респавна игрока в отключке
        /// </summary>
        public const string pn_DisableRespawnInUnconsciousness = "disableRespawnInUnconsciousness";
        /// <summary>
        /// Отключить диалог респавна игрока в бессознательном состоянии
        /// </summary>
        /// <remarks>
        /// Судя по всему если тут написать true, то когда игрок теряет сознание
        /// не появится надпись "возрождение". Т.е. пока не сдох окончательно
        /// респавн не запустится
        /// </remarks>
        [JsonPropertyName(pn_DisableRespawnInUnconsciousness)]
        public bool DisableRespawnInUnconsciousness { get; set; } = false;

    }
}
