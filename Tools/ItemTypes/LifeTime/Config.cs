using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUBC.DAYZ.GAME.Tools.ItemTypes.LifeTime;

/// <summary>
/// Конфигурация инструмента
/// изменения времени жизни предмета
/// </summary>
public class Config:ItemTypes.Config
{
    /// <summary>
    /// Устанавливаемое время жизни предметов
    /// </summary>
    public int LifeTime { get; set; } = 3888000;
}
