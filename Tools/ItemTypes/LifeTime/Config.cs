namespace KUBC.DAYZ.GAME.Tools.ItemTypes.LifeTime;

/// <summary>
/// Конфигурация инструмента
/// изменения времени жизни предмета
/// </summary>
public class Config : ItemTypes.Config
{
    /// <summary>
    /// Устанавливаемое время жизни предметов
    /// </summary>
    public int LifeTime { get; set; } = 3888000;
}
