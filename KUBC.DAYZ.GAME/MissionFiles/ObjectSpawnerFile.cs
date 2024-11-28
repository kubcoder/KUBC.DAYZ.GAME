using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUBC.DAYZ.GAME.MissionFiles;

/// <summary>
/// Файл используемый для настройки размещения статических
/// объектов при запуске сервера.
/// </summary>
public class ObjectSpawnerFile
{
    /// <summary>
    /// Массив объектов для размещения на старте сервера
    /// </summary>
    public List<ObjectSpawnerItem> Objects { get; set; } = new();
}
