using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUBC.DAYZ.GAME.Tools.ItemTypes.LifeTime;

/// <summary>
/// Строчка репорта
/// </summary>
public class ReportLine
{
    /// <summary>
    /// Имя элемента
    /// </summary>
    public string ItemName = string.Empty;

    /// <summary>
    /// Старое значение времени жизни
    /// </summary>
    public int OldLifeTime = 0;

    /// <summary>
    /// Новое значение времени жизни
    /// </summary>
    public int NewLifeTime = 0;
}
