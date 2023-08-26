using System.Text.Json.Serialization;

namespace KUBC.DAYZ.GAME.MissionFiles.CfgGamePlay
{
    /// <summary>
    /// Объект для спавна на старте
    /// </summary>
    public class SpawnerDataObject
    {
        /// <summary>
        /// Имя класса объекта
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Координата где размещать объект
        /// </summary>
        [JsonPropertyName("pos")]
        public Vector Position { get; set; } = new();

        /// <summary>
        /// Разворот объекта в пространстве
        /// </summary>
        [JsonPropertyName("ypr")]
        public Vector YPR { get; set; } = new();
    }
}
