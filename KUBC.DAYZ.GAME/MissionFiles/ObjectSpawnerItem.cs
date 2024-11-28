using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace KUBC.DAYZ.GAME.MissionFiles;

/// <summary>
/// Элемент для спавна
/// </summary>
public class ObjectSpawnerItem
{
    /// <summary>
    /// Имя класса объекта
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Позиция объекта
    /// </summary>
    [JsonPropertyName("pos")]
    public Vector Position { get; set; } = new Vector();

    /// <summary>
    /// Ориентация объекта (Yaw, Pitch, Roll)
    /// </summary>
    [JsonPropertyName("ypr")]
    public Vector YPR { get; set; } = new Vector();

    /// <summary>
    /// Масштабирование объекта
    /// </summary>
    [JsonPropertyName("scale")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public float? Scale { get; set; }

    /// <summary>
    /// Размещать без постоянства. Как только игрок возьмет его 
    /// то будет включено постоянство.
    /// Чет написано совсем не вразумительное
    /// </summary>
    [JsonPropertyName("enableCEPersistency")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? EnableCEPersistency {  get; set; }
}
