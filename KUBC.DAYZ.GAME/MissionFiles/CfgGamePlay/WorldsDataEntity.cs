using System.Text.Json.Serialization;

namespace KUBC.DAYZ.GAME.MissionFiles.CfgGamePlay
{
    /// <summary>
    /// Настройки игрового мира
    /// </summary>
    public class WorldsDataEntity
    {
        /// <summary>
        /// Имя параметра определяющего настройки конфигурации освещения
        /// </summary>
        public const string pn_LightingConfig = "lightingConfig";
        /// <summary>
        /// Настройка освещения в игре
        /// </summary>
        [JsonPropertyName(pn_LightingConfig)]
        public int LightingConfig { get; set; } = 1;
        /// <summary>
        /// Файлы спавнера статических объектов
        /// </summary>
        [JsonPropertyName("objectSpawnersArr")]
        public List<string> ObjectSpawnersArr { get; set; } = new();
        /// <summary>
        /// Минимальные температуры окружающей среды (ровно 12 значений)
        /// </summary>
        [JsonPropertyName("environmentMinTemps")]
        public List<int> EnvironmentMinTemps { get; set; } = new() { -3, -2, 0, 4, 9, 14, 18, 17, 12, 7, 4, 0 };
        /// <summary>
        /// Максимальные температуры окружающей среды (ровно 12 значений)
        /// </summary>
        [JsonPropertyName("environmentMaxTemps")]
        public List<int> EnvironmentMaxTemps { get; set; } = new() { 3, 5, 7, 14, 19, 24, 26, 25, 21, 16, 10, 5 };
        /// <summary>
        /// Values for item weight modification based on wetness level of the item. Values from left to right: [DRY, DAMP, WET, SOAKED, DRENCHED]
        /// Значения для изменения веса предмета в зависимости от уровня влажности предмета. Значения слева направо: [СУХОЙ, ВЛАЖНЫЙ, ВЛАЖНЫЙ, ПРОМОКОЙ, ПРОМОКОЙ]
        /// </summary>
        [JsonPropertyName("wetnessWeightModifiers")]
        public List<float> WetnessWeightModifiers { get; set; } = new() { 1.0f, 1.0f, 1.33f, 1.66f, 2.0f };
    }
}
